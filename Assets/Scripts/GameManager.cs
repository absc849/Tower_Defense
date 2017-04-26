using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum GameStatus{
	next, play, gameover, win
}
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

[SerializeField]
private int totalWaves = 10;
[SerializeField]
private Text totalEscapedLbl;

[SerializeField]
private Text totalMoneyLbl;

[SerializeField]
private Text currentWaveLbl;

[SerializeField]
private Text playButtonLbl;

[SerializeField]
private Button playButton;

private int waveNumber = 0;
private int totalMoney = 10;
private int totalEscaped = 0;
private int roundEscaped = 0;
private int totalKilled = 0;
private int whichEnemiesToSpawn = 0;

private GameStatus currentState = GameStatus.play;


//who escaped in the whole game
public int TotalEscaped{
	get {
		return totalEscaped;
	}
	set {
		totalEscaped = value;
	}
}
// who escaped in this round
public int RoundEscaped{
	get {
		return roundEscaped;
	}
	set{
		roundEscaped = value;
	}
}

public int TotalKilled{
	get {
		return totalKilled;
	}
	set{
		totalKilled = value;
	}
}


public int TotalMoney {
	get{
		return totalMoney;
	}
	set{
		totalMoney = value;
		totalMoneyLbl.text = totalMoney.ToString();
	}
}




public List<Enemy> EnemyList = new List<Enemy>();
	

/// <summary>
/// Awake is called when the script instance is being loaded.
/// </summary>
	
// Use this for initialization
	void Start () {
		playButton.gameObject.SetActive(false);
		ShowMenu();
		StartCoroutine(Spawn());
	
	}
	
	 /// <summary>
	/// Update is called every frame, if the MonoBehaviour is enabled.
	/// </summary>
	void Update()
	{
		HandleEscape();
	}


	

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

	public void AddMoney(int amount){
		TotalMoney += amount;

	}

	public void SubtractMoney(int amount){
		TotalMoney -= amount;

	}

	public void IsWaveOver(){
		totalEscapedLbl.text = "Escaped" + TotalEscaped + "/10";
		if(RoundEscaped + TotalKilled == totalEnemies){

			SetCurrentGameState();
			ShowMenu();
		}
	}

	public void SetCurrentGameState(){
		if(TotalEscaped >= 10){
			currentState = GameStatus.gameover;
		}else if (waveNumber == 0 && (TotalKilled + RoundEscaped) == 0){
			currentState = GameStatus.play;
		}else if (waveNumber >= totalWaves){
			currentState = GameStatus.win;
		}else {
			currentState = GameStatus.next;
		}
		
	}

	public void ShowMenu(){
		switch(currentState){
			case GameStatus.gameover:
			playButtonLbl.text = "Play Again!";
			/* create another button / banner have it say Game Over */

			//Add GameOver Sound
			break;
			case GameStatus.next:
			playButtonLbl.text = "Next Wave";
			
			break;
			case GameStatus.play:
			playButtonLbl.text = "Play";
			break;
			case GameStatus.win:
			playButtonLbl.text = "Play";
			/* create another button / banner have it say you win */
			break;
		}
		playButton.gameObject.SetActive(true);
	}

	public void PlayButtonPressed(){
		

	}

	private void HandleEscape(){
		if(Input.GetKeyDown(KeyCode.Escape)){
			TowerManager.Instance.DisableDragSprite();
			TowerManager.Instance.towerBtnPressed = null;
		}
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

