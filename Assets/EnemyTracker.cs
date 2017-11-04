using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTracker : MonoBehaviour {

	[SerializeField]
	static List<EnemyController> currentEnemies = new List<EnemyController>();


	// add pending enemies? ie enemies which will spawn by the level in x seconds or under y condition?

	public static void RemoveEnemy(EnemyController enemy){
		currentEnemies.Remove (enemy);
	} 

	public static void AddEnemy(EnemyController enemy){
		currentEnemies.Add (enemy);
	}

}
