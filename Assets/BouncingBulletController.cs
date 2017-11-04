using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BouncingBulletController : BulletController {

	[SerializeField]
	int numberBounces;

	public override void Move()
	{
		this.transform.position += this.transform.up * speed;
	}

	public override void SetOrientation(Vector3 position, Quaternion rotation)
	{
		this.transform.position = position;
		this.transform.rotation = rotation;
	}
		
	public virtual void OnCollisionEnter(Collision collision) {
		if (collision.collider.tag == "Stage") {
			if (numberBounces > 0) {
				SetBouncedOrientation (collision.contacts [0]);
				numberBounces--;
			}
			else
				Destroy(this.gameObject);
		}
	}

	// Calculate moving direction after bouncing off wall
	// Source : https://stackoverflow.com/questions/573084/how-to-calculate-bounce-angle
	void SetBouncedOrientation( ContactPoint contact){

		Vector3 v = this.transform.up;
		Vector3 n = contact.normal;

		Vector3 u = (Vector3.Dot (v, n) / Vector3.Dot (n, n)) * n;
		Vector3 w = v - u;

		Vector3 vPrime = w - u;

		this.transform.up = vPrime;
	}

}
