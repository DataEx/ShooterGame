using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour {

    IEnumerator Start() {
        yield return new WaitForSeconds(5f);
        Destroy(this.gameObject);
    }

	void FixedUpdate () {
        this.transform.position += this.transform.up * 2f;
	}

    void OnTriggerEnter(Collider collider) {
        if(collider.tag == "Stage")
            Destroy(this.gameObject);
    }
}
