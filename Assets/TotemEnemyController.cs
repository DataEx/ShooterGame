using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TotemEnemyController : EnemyController {

    int counter = 0;

    public override void FireBullet() {
        OrbBulletController forwardBullet = Instantiate(bulletPrefab) as OrbBulletController;
        OrbBulletController rightBullet = Instantiate(bulletPrefab) as OrbBulletController;
        OrbBulletController leftBullet = Instantiate(bulletPrefab) as OrbBulletController;
        OrbBulletController backBullet = Instantiate(bulletPrefab) as OrbBulletController;

        forwardBullet.name = "forwardBullet_" + counter;
        rightBullet.name = "rightBullet_" + counter;
        leftBullet.name = "leftBullet_" + counter;
        backBullet.name = "backBullet_" + counter;
        counter++;

        forwardBullet.SetBulletShooter(this.transform);
        rightBullet.SetBulletShooter(this.transform);
        leftBullet.SetBulletShooter(this.transform);
        backBullet.SetBulletShooter(this.transform);

        forwardBullet.EnableBulletParenting();
        rightBullet.EnableBulletParenting();
        leftBullet.EnableBulletParenting();
        backBullet.EnableBulletParenting();

        forwardBullet.SetTag("EnemyBullet");
        rightBullet.SetTag("EnemyBullet");
        leftBullet.SetTag("EnemyBullet");
        backBullet.SetTag("EnemyBullet");

        forwardBullet.SetOrientation(this.transform.position, this.transform.rotation);

        Vector3 rightRotation = this.transform.eulerAngles;
        rightRotation.y += 90f;
        rightBullet.SetOrientation(this.transform.position, Quaternion.Euler(rightRotation));

        Vector3 leftRotation = this.transform.eulerAngles;
        leftRotation.y -= 90f;
        leftBullet.SetOrientation(this.transform.position, Quaternion.Euler(leftRotation));

        Vector3 backRotation = this.transform.eulerAngles;
        backRotation.y += 180f;
        backBullet.SetOrientation(this.transform.position, Quaternion.Euler(backRotation));

    }

    public override void Awake()
    {
        base.Awake();
        StartCoroutine(Rotate());
    }

    IEnumerator Rotate() {
        while (true) {
            yield return new WaitForFixedUpdate();
            this.transform.Rotate(0, rotateSpeed, 0);
        }
    } 

}
