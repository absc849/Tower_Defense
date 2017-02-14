using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager> {

[SerializeField]
private GameObject spawnPoint;

[SerializeField]
private GameObject[] enemies;
[SerializeField]
private int maxOnScreenEnemies;
[SerializeField]
private int totalEnemies;
[SerializeField]
private int enemiesPerSpawn;
const float spawnDelay = 0.5f;

private int enemiesOnScreen = 0;
	

/// <summary>
/// Awake is called when the script instance is being loaded.
/// </summary>
	
// Use this for initialization
	void Start () {
		StartCoroutine(Spawn());
	}
	/// <summary>
	/// Update is called every frame, if the MonoBehaviour is enabled.
	/// </summary>



	public void RemoveEnemyFromScreen(){
		if (enemiesOnScreen > 0){
			enemiesOnScreen -= 1;
		}
	}

	IEnumerator Spawn(){
		if(enemiesPerSpawn > 0 && enemiesOnScreen < totalEnemies){
			for(int i = 0; i < enemiesPerSpawn; i++){
				if(enemiesOnScreen < maxOnScreenEnemies){
					GameObject newEnemy = Instantiate(enemies[1]) as GameObject;
					// instatiate creates an object, using as game object turns the object back into a game object
					newEnemy.transform.position = spawnPoint.transform.position;
					enemiesOnScreen += 1;
				}
			}
			yield return new WaitForSeconds(spawnDelay);
			StartCoroutine(Spawn());
		}

	}	
}

