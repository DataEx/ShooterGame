using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class TrackingEnemyController : EnemyController
{

    public override void FireBullet() {
        BulletController forwardBullet = Instantiate(bulletPrefab) as BulletController;
        forwardBullet.SetBulletShooter(this.transform);
        forwardBullet.SetOrientation(this.transform.position, this.transform.rotation);
        
    }

   void Update() {
        this.GetComponent<NavMeshAgent>().destination = playerCharacter.transform.position;
    }
}
