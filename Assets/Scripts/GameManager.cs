using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {


public static GameManager instance = null;
public GameObject spawnPoint;
public GameObject[] enemies;
public int maxOnScreenEnemies;
public int totalEnemies;
public int enemiesPerSpawn;

private int enemiesOnScreen = 0;
	

/// <summary>
/// Awake is called when the script instance is being loaded.
/// </summary>
	void Awake()
	{
		if(instance == null){
			instance = this;
		}
		else if(instance != this){
			Destroy(gameObject);
		}
		DontDestroyOnLoad(gameObject);
		
	}
// Use this for initialization
	void Start () {
		SpawnEnemy();
	}

	void SpawnEnemy(){
		if(enemiesPerSpawn > 0 && enemiesOnScreen < totalEnemies){
			for(int i = 0; i < enemiesPerSpawn; i++){
				if(enemiesOnScreen < maxOnScreenEnemies){
					GameObject newEnemy = Instantiate(enemies[0]) as GameObject;
					// instatiate creates an object, using as game object turns the object back into a game object
					newEnemy.transform.position = spawnPoint.transform.position;
					maxOnScreenEnemies += 1;
				}
			}
		}
	}

	public void RemoveEnemyFromScreen(){
		if (enemiesOnScreen > 0){
			enemiesOnScreen -= 1;
		}
	}	
}

