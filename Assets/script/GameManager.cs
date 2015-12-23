﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;


public class GameManager : MonoBehaviour {

	public Text scoreText;
	public static int score;
	public static int numOfEnemys;
	public GameObject powerUp;
	int givePowerUp;

	//For Sound
	public static bool deathSound;
	public static int killCount;
	public static bool powerUpcomeUp;


	public GameObject enemyO;

	
	// Use this for initialization
	void Start () {
		killCount = 0; 
		// Initilizae score.
		score = 0;
		givePowerUp = 100;
	}

	// Update is called once per frame
	void Update () {
		scoreText.text = "Score: " + score;
		if (score >= givePowerUp && !LevelManager.levelOver) {
			StartCoroutine(CreatePowerUp());
			givePowerUp += 200;
		}
	}

	public static void KillPlayer (Player player) {
		deathSound = true;
		Debug.Log ("Player has Been Killed");
		//Application.LoadLevel("MainMenu");
		
	}
	
	public static void KillEnemy (Enemy enemy) {
		killCount++;
		numOfEnemys--;

		Destroy (enemy.gameObject);
	}

	private void EnemyKillAnimation(Enemy enemy) {

	}


	IEnumerator CreatePowerUp() {
		Vector3 temp = new Vector3(Random.Range(-70, 70), Random.Range(-54, 54), 0f);
		yield return new WaitForSeconds(2f);
		powerUpcomeUp = true; 
		Instantiate(powerUp, temp, Quaternion.identity);
	}




}
