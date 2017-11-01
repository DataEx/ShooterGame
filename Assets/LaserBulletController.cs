using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserBulletController : BulletController
{

    public override void Move()
    {
        this.transform.position += this.transform.up * speed;
    }

    public override void SetOrientation(Vector3 position, Quaternion rotation)
    {
        this.transform.position = position;
        this.transform.rotation = rotation;
    }


}
