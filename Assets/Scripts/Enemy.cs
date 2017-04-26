// using System.Collections;
// using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {

private int target = 0;
[SerializeField]
private Transform exitPoint;

[SerializeField]
private Transform[] wayPoints;

[SerializeField]
private int rewardAmount;

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

private Animator anim;


	// Use this for initialization
	void Start () {
		enemy = GetComponent<Transform>();
		enemyCollider = GetComponent<Collider2D>();
		anim = GetComponent<Animator>();
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
			GameManager.Instance.RoundEscaped += 1;
			GameManager.Instance.TotalEscaped += 1;
			GameManager.Instance.UnregisterEnemy(this);
			GameManager.Instance.IsWaveOver();

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
		anim.Play("Hurt");
		} else {
			anim.SetTrigger("enemyDies");
			//set trigger because the death animation is supposed to interrupt 
			//any current state, look at the animator to understand it.
			Die();
		}
	}

	public void Die(){
		isDead = true;
		GameManager.Instance.TotalKilled += 1;
		enemyCollider.enabled = false;
		GameManager.Instance.AddMoney(rewardAmount);
		GameManager.Instance.IsWaveOver();
		
	}
}
