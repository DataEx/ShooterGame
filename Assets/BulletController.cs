using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BulletController : MonoBehaviour {

    public float speed;
    protected Transform bulletShooter;

    IEnumerator Start() {
        yield return new WaitForSeconds(5f);
        Destroy(this.gameObject);
    }

	void FixedUpdate () {
        Move();
	}

    public virtual void Move() {
    }

    void OnTriggerEnter(Collider collider) {
        if (collider.tag == "Stage") {
            Destroy(this.gameObject);
        }
    }

    public void SetTag(string tag) {
        this.tag = tag;
    }

    public virtual void SetOrientation(Vector3 position, Quaternion rotation) {}


    public virtual void SetBulletShooter(Transform shooter)
    {
        bulletShooter = shooter;
    }
}
