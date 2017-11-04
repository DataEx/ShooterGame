using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUDController : MonoBehaviour {

    [SerializeField]
    Text healthText;

	[SerializeField]
	Text weaponText;

	[SerializeField]
	Text ammoText;

    public void SetPlayerHealth(int newHealth) {
        healthText.text = newHealth.ToString();
    }

	public void SetPlayerWeapon(string weaponName) {
		weaponText.text = weaponName;
	}

	public void SetPlayerAmmoRemaining(string ammoRemaining) {
		ammoText.text = ammoRemaining;
	}

}
