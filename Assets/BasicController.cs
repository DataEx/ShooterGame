using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BasicController : MonoBehaviour {

    [SerializeField]
    protected int health;

    protected int maxHealth;

    [SerializeField]
    protected float speed = 1f;

    [SerializeField]
    protected Transform rotator;

    [SerializeField]
    protected Collider characterCollider;

    [SerializeField]
    protected float bulletsPerSecond;

    [SerializeField]
    protected BulletController bulletPrefab;

    protected Renderer[] characterMeshes;
    protected Color characterColor; 

    public virtual void FireBullet() { }

    public virtual void Awake() {
        characterMeshes = GetComponentsInChildren<Renderer>();
        characterColor = characterMeshes[0].material.color;
        maxHealth = health;
        StartCoroutine(FireBulletsContinuously());
    }

    public virtual IEnumerator FireBulletsContinuously()
    {
        while (true)
        {
            FireBullet();
            yield return new WaitForSeconds(1f / bulletsPerSecond);
        }
    }

    public virtual void SetCharacterColor(Color newColor) {
        foreach (Renderer r in characterMeshes) {
            r.material.color = newColor;
        }
    }

}
