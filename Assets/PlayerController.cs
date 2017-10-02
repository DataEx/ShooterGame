using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    [SerializeField]
    private float speed = 1f;
    public Transform rotator;
    Vector3 localFrontSide, localBackSide, localLeftSide, localRightSide;

    // Use this for initialization
    void Awake () {
        BoxCollider boxCollider = this.GetComponent<BoxCollider>();
        Vector3 colliderCenter = boxCollider.center;
        Vector3 colliderSize = boxCollider.size;
        PrintVector3(colliderCenter);
        PrintVector3(colliderSize);
        float scale = this.transform.localScale.x;
        localFrontSide = (colliderCenter + Vector3.forward * colliderSize.x / 2f) * scale;
        PrintVector3(localFrontSide);
        localBackSide = (colliderCenter - Vector3.forward * colliderSize.x / 2f) * scale;
        localRightSide = (colliderCenter + Vector3.right* colliderSize.z / 2f) * scale;
        localLeftSide = (colliderCenter - Vector3.right * colliderSize.z / 2f) * scale;
    }

    // Update is called once per frame
    void FixedUpdate () {
        HandleLeftJoystickInput();
        HandleRightJoystickInput();   
    }

    // Controls player rotation
    void HandleRightJoystickInput()
    {
        float horizontalTurnInput = Input.GetAxis("HorizontalTurn");
        float verticalTurnInput = Input.GetAxis("VerticalTurn");

        if (horizontalTurnInput == 0 && verticalTurnInput == 0)
            return;

        Vector2 direction = new Vector2(horizontalTurnInput, verticalTurnInput);
        float angle = Vector2.Angle(Vector2.up, direction);
        if (horizontalTurnInput < 0)
            angle = 360 - angle;
        rotator.transform.localRotation = Quaternion.Euler(0, angle, 0);

    }

    // Controls player movement
    void HandleLeftJoystickInput() {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        if (horizontalInput == 0 && verticalInput == 0)
            return;

        Vector3 movementVector = (Vector3.forward * verticalInput
            + Vector3.right * horizontalInput) * speed;

        // Adjust movement vector against wall collisions
        this.transform.position += CheckMovement(movementVector);
    }


    // Prevent 
    Vector3 CheckMovement(Vector3 movementVector) {
        RaycastHit hit;

        float horiztonalMovement = movementVector.x;
        float verticalMovement = movementVector.z;


        if (verticalMovement > 0)
        {
            if (Physics.Raycast(transform.position + localFrontSide, Vector3.forward, out hit))
            {
                if (hit.distance <= verticalMovement)
                    verticalMovement = hit.distance - 0.01f;
            }
        }
        else if (verticalMovement < 0)
        {
            if (Physics.Raycast(transform.position + localBackSide, Vector3.back, out hit))
            {
                if (hit.distance <= -verticalMovement)
                    verticalMovement = -hit.distance + 0.01f;
            }
        }
        if (horiztonalMovement > 0)
        {
            if (Physics.Raycast(transform.position + localRightSide, Vector3.right, out hit))
            {
                if (hit.distance <= horiztonalMovement)
                    horiztonalMovement = hit.distance - 0.01f;
            }
        }
        else if (horiztonalMovement < 0)
        {
            if (Physics.Raycast(transform.position + localLeftSide, Vector3.left, out hit))
            {
                if (hit.distance <= -horiztonalMovement)
                    horiztonalMovement = -hit.distance + 0.01f;
            }
        }

        Vector3 updatedMovementVector = new Vector3(horiztonalMovement, 0, verticalMovement);
        return updatedMovementVector;
    }

    static void PrintVector3(Vector3 v) {
        print(v.x + ", " + v.y + ", " + v.z);
    }


}
