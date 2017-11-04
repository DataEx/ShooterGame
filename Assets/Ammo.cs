using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ammo : MonoBehaviour {

	[System.Serializable]
	public class AmmoData{
		public BulletController bulletType;
		public int bulletsRemaining;
		public bool hasInfiniteBullets;
	}

}
