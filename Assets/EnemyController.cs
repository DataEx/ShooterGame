using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : BasicController {

    protected float rotateSpeed;
    

    void OnTriggerEnter(Collider collider) {
        print(collider.name);
        health -= 1;
        if (health <= 0)
            Destroy(this.gameObject);
    }

    protected void Move() {

    }

    protected void FollowPlayer() {

    }

    public virtual void FireBullet() { }


}
