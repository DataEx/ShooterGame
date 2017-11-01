using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : EnemyController
{
    public EnemyController enemyPrefab;
    public int maxEnemiesSpawn;
    int enemiesSpawned = 0;

    public override void FireBullet()
    {
        if (enemiesSpawned < maxEnemiesSpawn)
        {
            EnemyController enemy = Instantiate(enemyPrefab) as EnemyController;
            enemy.GetComponent<EnemyController>().playerCharacter = playerCharacter;
            enemy.transform.position = this.transform.position;
            enemiesSpawned++;
        }
    }

}
