using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;


public class GameManager : MonoBehaviour {

	public static bool isPause;

	public Text scoreText;
	public static int score;
	public static int numOfEnemys;
	public GameObject powerUp;
	private int givePowerUp;

	//For Sound
	public static bool deathSound;

	public static bool powerUpcomeUp;

	public static int numOfPowerUps;


	public GameObject enemyO;

	
	// Use this for initialization
	void Start () {
		numOfPowerUps = 0;
		isPause = false;
		PowerUps.haveExplodingArrowPowerUp = false;
		PowerUps.haveMachingGunPowerUp = false;
		PowerUps.haveShieldPowerUp = false;
		PowerUps.haveSpecialArrowPowerUp = false;



		// Initilizae score.
		score = 0;
		givePowerUp = 100;
		CreatePowerUp();
	}

	// Update is called once per frame
	void Update () {
		scoreText.text = "" + score;
		if (score >= givePowerUp && !LevelManager.levelOver) {
			StartCoroutine(CreatePowerUp());
			givePowerUp += 200;
		}

		if (LevelManager.level % 6 == 0) {
			PowerUps.perLevelLife = true;
			CreatePowerUp();
		}

		if (Input.GetKeyDown(KeyCode.P)) {

			if(!isPause) {
				Time.timeScale = 0;
			}else {
				Time.timeScale = 1;
			}
			isPause = !isPause;
		} 
	}

	public static void KillPlayer (Player player) {
		deathSound = true;
		Debug.Log ("Player has Been Killed");

		Debug.LogError("score is ==========> " + score);
		Debug.LogError("high score was ==========> " + PlayerPrefs.GetInt("HighScore"));
		if (score > PlayerPrefs.GetInt("HighScore")) {
			PlayerPrefs.SetInt("HighScore", score);
			Debug.LogError("new high score is ==========> " + PlayerPrefs.GetInt("HighScore"));
		}

		Application.LoadLevel("MainMenu");
		
	}
	
	public static void KillEnemy (Enemy enemy) {

		numOfEnemys--;

		Destroy (enemy.gameObject);
	}





	IEnumerator CreatePowerUp() {
		if (numOfPowerUps < 2) {
			numOfPowerUps++;
			Vector3 temp = new Vector3(Random.Range(-70, 70), Random.Range(-54, 54), 0f);
			yield return new WaitForSeconds(2f);
			powerUpcomeUp = true; 
			Instantiate(powerUp, temp, Quaternion.identity);
		}
	
	}




}
