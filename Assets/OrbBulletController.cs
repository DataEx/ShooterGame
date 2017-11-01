using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrbBulletController : BulletController {

    Quaternion previousShooterOrientation;

    bool isParenting = false;

    void Awake() {
        this.tag = "EnemyBullet";
    }

    public void EnableBulletParenting() {
        isParenting = true;
    }

    public override void Move()
    {
        this.transform.position += this.transform.forward * speed;

        if (bulletShooter != null && isParenting) {
            Quaternion currentShooterOrientation = bulletShooter.rotation;
            float angleRotated = Quaternion.Angle(currentShooterOrientation, previousShooterOrientation);
            float distToShooter = Vector3.Distance(this.transform.position, bulletShooter.position);
            this.transform.Rotate(Vector3.up, angleRotated);
            this.transform.position = bulletShooter.position + this.transform.forward * distToShooter;
            previousShooterOrientation = currentShooterOrientation;
        }

    }

    public override void SetOrientation(Vector3 position, Quaternion rotation)
    {
        this.transform.position = position;
        this.transform.rotation = rotation;
    }

    public override void SetBulletShooter(Transform shooter)
    {
        bulletShooter = shooter;
        previousShooterOrientation = bulletShooter.rotation;
    }


}
