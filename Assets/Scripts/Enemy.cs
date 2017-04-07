﻿// using System.Collections;
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

[SerializeField]
private int healthPoints;

private Collider2D enemyCollider;


private Transform enemy;
private float navigationTime = 0;

private bool isDead = false;

public bool IsDead{
	get{
		return isDead;
	}
}


	// Use this for initialization
	void Start () {
		enemy = GetComponent<Transform>();
		enemyCollider = GetComponent<Collider2D>();
		GameManager.Instance.RegisterEnemy(this);
		
	}
	
	// Update is called once per frame
	void Update () {
		if(wayPoints != null && !isDead){
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
		else if(other.tag == "Projectile"){
			Projectile newP = other.gameObject.GetComponent<Projectile>();
			EnemyHit(newP.AttackStrength);
			Destroy(other.gameObject);
		}
	}

	public void EnemyHit(int HitPoints){
		if(healthPoints - HitPoints > 0){
		healthPoints -= HitPoints;
		// enemy hurt animation
		} else {
			// enemy die animation
			Die();
		}
	}

	public void Die(){
		isDead = true;
		enemyCollider.enabled = false;
	}
}
