﻿using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour {


	
	[System.Serializable]  
	public class EnemyStats {
		public int health = 5;
	}
	
	public EnemyStats enemyStats = new EnemyStats();

	//For Sound
	public static bool spearHitEnemy;
	public static bool enemyDeath;
	public static int killCount;

	//For Animation
    Animator anim;

	// For enemy to ignore walls.
	Collider2D leftWall;
	Collider2D rightWall;
	Collider2D upperWall;
	Collider2D lowerWall;
	Collider2D enemyCollider2D;





	void Start () {
		anim = GetComponent<Animator>();
		killCount = 0; 

		// Ignore walls.
		enemyCollider2D = GetComponent<Collider2D>();

		leftWall = GameObject.Find("LeftWall").GetComponent<Collider2D>();
		Physics2D.IgnoreCollision(leftWall, enemyCollider2D);

		rightWall = GameObject.Find("RightWall").GetComponent<Collider2D>();
		Physics2D.IgnoreCollision(rightWall, enemyCollider2D);

		upperWall = GameObject.Find("UpperWall").GetComponent<Collider2D>();
		Physics2D.IgnoreCollision(upperWall, enemyCollider2D);

		lowerWall = GameObject.Find("LowerWall").GetComponent<Collider2D>();
		Physics2D.IgnoreCollision(lowerWall, enemyCollider2D);



	}
	
	public void DamageEnemy (int damage) {

		spearHitEnemy = true; 
		enemyStats.health -= damage;


		if (enemyStats.health <= 0) {
			enemyDeath = true;
			killCount++;
			anim.SetBool("death", true);
			enemyCollider2D.isTrigger = true;
			iTween.Stop(this.gameObject);
			Rigidbody2D enemyR = GetComponent<Rigidbody2D>();
			enemyR.constraints = RigidbodyConstraints2D.FreezeAll;

			StartCoroutine(DeathAnimation());
			AddScore();
		} 

		if ((tag == "enemy3" || tag == "enemy2") && !anim.GetBool("death")) {

			StartCoroutine(WaitForHitAnimation());
		}
		

	}

	IEnumerator WaitForHitAnimation() {
		anim.SetBool("enemyHit", true);

		yield return new WaitForSeconds(2f);
		anim.SetBool("enemyHit", false);
	}


	IEnumerator DeathAnimation () {
		Debug.Log("im here");
	
		yield return new WaitForSeconds(2f);

		GameManager.KillEnemy(this);

	}

    public void EnemyTouchPlayer ()   {
        //TODO: kills the enemy when this function is called.
        Debug.Log("need to kill enemy");
		GameManager.numOfEnemys--;
        Destroy(this.gameObject);
    }


	public void AddScore() {
		string tagOfEnemy = this.tag;

		switch(tagOfEnemy) {
		case("enemy1"):
			GameManager.score += 16;
			break;
		case("enemy2"):
			GameManager.score += 23;
			break;
		case("enemy3"):
			GameManager.score += 42;
			break;
			
		}

	}

}
