using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUDController : MonoBehaviour {

    [SerializeField]
    Text healthText;

    public void SetPlayerHealth(int newHealth) {
        healthText.text = newHealth.ToString();
    }

}
