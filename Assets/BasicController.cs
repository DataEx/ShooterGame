using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicController : MonoBehaviour {

    [SerializeField]
    protected int health;

    [SerializeField]
    protected float speed = 1f;

    [SerializeField]
    protected Transform rotator;


    [SerializeField]
    protected float bulletsPerSecond;

    [SerializeField]
    protected GameObject bulletPrefab;


}
