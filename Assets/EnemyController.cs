using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EnemyController : BasicController {

    public float rotateSpeed;
    public GameObject playerCharacter;

    public override void Awake()
    {
        base.Awake();
		EnemyTracker.AddEnemy (this);
	}

    void OnTriggerEnter(Collider collider) {
        if (collider.tag == "PlayerBullet") {
            Destroy(collider.gameObject);
            health -= 1;
            SetHealthColor();
			if (health <= 0) {
				EnemyTracker.RemoveEnemy (this);
				Destroy(this.gameObject);
			}
        }
    }

    void SetHealthColor() {
        SetCharacterColor(Color.Lerp(Color.green, Color.red, 1.0f - (float)health / maxHealth));
    }

    public virtual void Move() {
    }

    public virtual void FollowPlayer() {

    }

    


}
