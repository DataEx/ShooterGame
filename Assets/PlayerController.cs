using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : BasicController {

    bool isInvulnerable = false;

    Vector3 localNorthSide, localSouthSide, localWestSide, localEastSide,
        localNESide, localNWSide, localSESide, localSWSide;

    public GameObject cannonLeft;
    public GameObject cannonRight;

    Coroutine turningCoroutine;

    [SerializeField]
    HUDController HUD;

	[SerializeField]
	Ammo.AmmoData [] ammoList;

	[SerializeField]
	int activeAmmoIndex = 0;

    // Use this for initialization
    public override void Awake () {
        /*
        BoxCollider boxCollider = this.GetComponent<BoxCollider>();
        Vector3 colliderCenter = boxCollider.center;
        Vector3 colliderSize = boxCollider.size;
        float scale = this.transform.localScale.x;
        localNorthSide = (colliderCenter + Vector3.forward * colliderSize.x / 2f) * scale;
        localSouthSide = (colliderCenter - Vector3.forward * colliderSize.x / 2f) * scale;
        localEastSide = (colliderCenter + Vector3.right* colliderSize.z / 2f) * scale;
        localWestSide = (colliderCenter - Vector3.right * colliderSize.z / 2f) * scale;

        Vector3 NEVector = (new Vector3(1, 0, 1)).normalized;
        Vector3 NWVector = (new Vector3(-1, 0, 1)).normalized;
        Vector3 SEVector = (new Vector3(1, 0, -1)).normalized;
        Vector3 SWVector = (new Vector3(-1, 0, -1)).normalized;

        localNESide = (colliderCenter + NEVector * colliderSize.x / 2f) * scale;
        localNWSide = (colliderCenter + NWVector * colliderSize.x / 2f) * scale;
        localSESide = (colliderCenter + SEVector * colliderSize.x / 2f) * scale;
        localSWSide = (colliderCenter + SWVector * colliderSize.x / 2f) * scale;
        */
		SwitchAmmo (activeAmmoIndex);

        base.Awake();
    }

    void FixedUpdate () {
        HandleLeftJoystickInput();
        HandleRightJoystickInput();

    }

    void OnCollisionEnter(Collision collision)
    {
        Collider collider = collision.collider;
        if (collider.tag == "EnemyBullet" && !isInvulnerable)
        {
            health -= 1;
            if (health <= 0) {
                print("Game Over!");
                health = 0;
            }
            else {
                StartCoroutine(EnableInvulnerability());
            }
            HUD.SetPlayerHealth(health);
        }
    }


    IEnumerator EnableInvulnerability() {
        float timeInvulnerable = 1f;
        float currentTime = 0f;
        float blinkingInterval = 0.1f;
        int colorInt = 0;

        isInvulnerable = true;
        while (currentTime < timeInvulnerable) {
            yield return new WaitForSecondsRealtime(blinkingInterval);
            if (colorInt == 0)
                SetCharacterColor(Color.red);
            else
                SetCharacterColor(characterColor);
            colorInt = (colorInt + 1) % 2;
            currentTime += blinkingInterval;
        }

        SetCharacterColor(characterColor);
        isInvulnerable = false;
    }

    public override void FireBullet() {
		if (ammoList [activeAmmoIndex].hasInfiniteBullets || ammoList [activeAmmoIndex].bulletsRemaining > 0) {
			BulletController leftBullet = Instantiate(bulletPrefab) as BulletController;
			BulletController rightBullet = Instantiate(bulletPrefab) as BulletController;
			leftBullet.SetTag("PlayerBullet");
			rightBullet.SetTag("PlayerBullet");
			leftBullet.SetOrientation(cannonLeft.transform.position, cannonLeft.transform.rotation);
			rightBullet.SetOrientation(cannonRight.transform.position, cannonRight.transform.rotation);	
			if (!ammoList [activeAmmoIndex].hasInfiniteBullets) {
				ammoList [activeAmmoIndex].bulletsRemaining--;
				UpdateHUDAmmo ();
			}
		}
 

    }

    // Controls player rotation
    void HandleRightJoystickInput()
    {
        float horizontalTurnInput = Input.GetAxis("HorizontalTurn");
        float verticalTurnInput = Input.GetAxis("VerticalTurn");

        if (horizontalTurnInput == 0 && verticalTurnInput == 0)
            return;

        Vector2 direction = new Vector2(horizontalTurnInput, verticalTurnInput);
        if (direction.magnitude < 0.4f)
            return;

        float angle = Vector2.Angle(Vector2.up, direction);
        if (horizontalTurnInput < 0)
            angle = 360 - angle;

        if (turningCoroutine != null)
            StopCoroutine(turningCoroutine);
        turningCoroutine = StartCoroutine(TurnTo(angle));
    }

    IEnumerator TurnTo(float desiredAngle) {
        float turnSpeed = 20f;
        float currentAngle = rotator.transform.localEulerAngles.y;
        int direction = 1;
        while (currentAngle != desiredAngle) {
            float diffAngle = desiredAngle - currentAngle;
            if (diffAngle > 180)
                direction = -1;
            else if (diffAngle < -180)
                direction = 1;
            else if(diffAngle < 0)
                direction = -1;
            if (Mathf.Abs(diffAngle) <= turnSpeed)
            {
                rotator.transform.localRotation = Quaternion.Euler(0, desiredAngle, 0);
            }
            else {
                rotator.transform.localRotation = Quaternion.Euler(0, currentAngle + direction * turnSpeed, 0);
            }
            yield return null;
            currentAngle = rotator.transform.localEulerAngles.y;
        }
        turningCoroutine = null;
    }

    // Controls player movement
    void HandleLeftJoystickInput() {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        if (horizontalInput == 0 && verticalInput == 0) {
            //this.GetComponent<Rigidbody>().velocity = Vector3.zero;
            return;
        }

        /*
        if (horizontalInput < 0)
            horizontalInput = -1;
        else if (horizontalInput > 0)
            horizontalInput = 1;

        if (verticalInput < 0)
            verticalInput = -1;
        else if (verticalInput > 0)
            verticalInput = 1;
            */

        Vector3 movementVector = (Vector3.forward * verticalInput
            + Vector3.right * horizontalInput) * speed;

        // Adjust movement vector against wall collisions
       // this.GetComponent<Rigidbody>().velocity = movementVector;
        this.transform.position += CheckMovement(movementVector);
    }


    // Prevent 
    Vector3 CheckMovement(Vector3 movementVector) {
        RaycastHit hit;

        float horiztonalMovement = movementVector.x;
        float verticalMovement = movementVector.z;

        SphereCollider collider = GetComponent<SphereCollider>();
        Bounds bounds = collider.bounds;
        Vector3 colliderCenter = bounds.center;
        float colliderRadius = collider.radius;
        float scale = this.transform.localScale.x ;
        Vector3 direction = new Vector3(horiztonalMovement, 0, verticalMovement);
        Vector3 movementDirection = direction.normalized * scale  * colliderRadius;
        Vector3 scaledDirection = direction.normalized * 100 + Vector3.up * colliderCenter.y;

        Debug.DrawRay(colliderCenter, scaledDirection);
        if (Physics.Raycast(colliderCenter, scaledDirection, out hit))
        {
            Vector3 point = hit.point;
            Vector3 origin = colliderCenter;
            Vector3 vector = (point - origin);
            
            if (horiztonalMovement > 0 && vector.x < horiztonalMovement + scale * colliderRadius)
            {
                horiztonalMovement = 0;
            }
            else if (horiztonalMovement < 0 && vector.x > horiztonalMovement - scale * colliderRadius)
            {
                horiztonalMovement = 0;
            }


            if (verticalMovement > 0 && vector.z < verticalMovement + scale * colliderRadius)
            {
                verticalMovement = 0;
            }
            else if (verticalMovement < 0 && vector.z > verticalMovement - scale * colliderRadius)
            {
                verticalMovement = 0;
            }
           
          //  if (hit.distance <= movementDirection.magnitude)
           //    return -movementDirection.normalized *(movementDirection.magnitude - hit.distance);
        }
        
        /*
        // need to split it into x and z compoonent
        Vector3 horizontalDirection = new Vector3(horiztonalMovement, 0, 0);
        Vector3 scaledHorizontalDirection = horizontalDirection.normalized * scale * colliderRadius;

        if (Physics.Raycast(transform.position + scaledHorizontalDirection, horizontalDirection, out hit))
        {
            if (hit.distance <= Mathf.Abs(horiztonalMovement))
            {
                horiztonalMovement = 0;
            }
        }

        Vector3 verticalDirection = new Vector3(0, 0, verticalMovement);
        Vector3 scaledVerticalDirection = verticalDirection.normalized * scale * colliderRadius;

        if (Physics.Raycast(transform.position + scaledVerticalDirection, verticalDirection, out hit))
        {
//            print(hit.collider.name);
            print("D: " + hit.distance + "   V: " + verticalMovement);
            if (hit.distance <= Mathf.Abs(verticalMovement))
            {
                verticalMovement = 0;
            }
            else {
                print("no collisoin");
            }
        }
        */


        /*
        if (verticalMovement > 0)
        {
            if (Physics.Raycast(transform.position + localNorthSide, Vector3.forward, out hit))
            {
                if (hit.distance <= verticalMovement)
                    verticalMovement = hit.distance - 0.01f;
            }
        }
        else if (verticalMovement < 0)
        {
            if (Physics.Raycast(transform.position + localSouthSide, Vector3.back, out hit))
            {
                if (hit.distance <= -verticalMovement)
                    verticalMovement = -hit.distance + 0.01f;
            }
        }
        if (horiztonalMovement > 0)
        {
            if (Physics.Raycast(transform.position + localEastSide, Vector3.right, out hit))
            {
                if (hit.distance <= horiztonalMovement)
                    horiztonalMovement = hit.distance - 0.01f;
            }
        }
        else if (horiztonalMovement < 0)
        {
            if (Physics.Raycast(transform.position + localWestSide, Vector3.left, out hit))
            {
                if (hit.distance <= -horiztonalMovement)
                    horiztonalMovement = -hit.distance + 0.01f;
            }
        }
        */
        Vector3 updatedMovementVector = new Vector3(horiztonalMovement, 0, verticalMovement);
        return updatedMovementVector;
    }

	void SwitchAmmo(int index){
		activeAmmoIndex = index;
		bulletPrefab = ammoList [activeAmmoIndex].bulletType;
		UpdateHUDWeapon ();
		UpdateHUDAmmo ();

	}

	void UpdateHUDAmmo(){
		if (ammoList [activeAmmoIndex].hasInfiniteBullets) {
			HUD.SetPlayerAmmoRemaining ("∞");
		}
		else
			HUD.SetPlayerAmmoRemaining (ammoList [activeAmmoIndex].bulletsRemaining.ToString());
	}

	void UpdateHUDWeapon(){
		HUD.SetPlayerWeapon (ammoList [activeAmmoIndex].bulletType.GetBulletName());
	}

    static void PrintVector3(Vector3 v) {
        print(v.x + ", " + v.y + ", " + v.z);	
    }


}
