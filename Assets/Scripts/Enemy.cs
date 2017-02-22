// using System.Collections;
// using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {

private int target = 0;
[SerializeField]
private Transform exitPoint;

[SerializeField]
private Transform[] wayPoints;

//the smaller the number the less it will be called in update
[SerializeField]
private float navigationUpdate;


private Transform enemy;
private float navigationTime = 0;


	// Use this for initialization
	void Start () {
		enemy = GetComponent<Transform>();
		GameManager.Instance.RegisterEnemy(this);
		
	}
	
	// Update is called once per frame
	void Update () {
		if(wayPoints != null){
			navigationTime += Time.deltaTime;
			if(navigationTime > navigationUpdate){
				if(target < wayPoints.Length){
					enemy.position = Vector2.MoveTowards(enemy.position,wayPoints[target].position, navigationTime);
				}else {
					enemy.position = Vector2.MoveTowards(enemy.position,exitPoint.position, navigationTime);
				}
				navigationTime = 0;
			}
		}
	}

	
	/// <summary>
	/// Sent when another object enters a trigger collider attached to this
	/// object (2D physics only).
	/// </summary>
	/// <param name="other">The other Collider2D involved in this collision.</param>
	void OnTriggerEnter2D(Collider2D other)
	{
		if(other.tag == "CheckPoint"){
			target += 1;
		}
		else if (other.tag == "Finish"){
			GameManager.Instance.UnregisterEnemy(this);

		}
	}
}
