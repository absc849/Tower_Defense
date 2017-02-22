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



public List<Enemy> EnemyList = new List<Enemy>();
	

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



	

	public void RegisterEnemy(Enemy enemy){
		EnemyList.Add(enemy);

	}

	public void UnregisterEnemy(Enemy enemy){
		EnemyList.Remove(enemy);
		Destroy(enemy.gameObject);
	}

	public void DestroyAllEnemies()
	{
		foreach(Enemy enemy in EnemyList){
			Destroy(enemy.gameObject);
		}
		EnemyList.Clear();
	}

	IEnumerator Spawn(){
		if(enemiesPerSpawn > 0 && EnemyList.Count < totalEnemies){
			for(int i = 0; i < enemiesPerSpawn; i++){
				if(EnemyList.Count < maxOnScreenEnemies){
					GameObject newEnemy = Instantiate(enemies[1]) as GameObject;
					// instatiate creates an object, using as game object turns the object back into a game object
					newEnemy.transform.position = spawnPoint.transform.position;
				}
			}
			yield return new WaitForSeconds(spawnDelay);
			StartCoroutine(Spawn());
		}

	}	
}

