using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {

	private int maxHealth = 100;
	bool unTouchable = false;




	[System.Serializable]  
	public class PlayerStats {
		public int health = 100;

	}


	public static PlayerStats playerStats;
	public static bool dead; 

	void Start() {
		dead = false;
		playerStats = new PlayerStats();
		unTouchable = false;
		Debug.Log("is player untouchable " + unTouchable);
	
	}

	public void DamagePlayer (int damage) {
		if (unTouchable) {
			Debug.Log("Player UnTouchable mother fucker!!!!!!! " + unTouchable);
			return;
		}
	

		playerStats.health -= damage;

		if (playerStats.health <= 0) {
			dead = true;  
			StartCoroutine(NewGame());
			GameManager.KillPlayer(this);
		}
	}

	IEnumerator NewGame() {
		yield return new WaitForSeconds(3f);
		Application.LoadLevel("EndScene");
	}

	public void RevivePlayer (int revive) {

		if (playerStats.health + revive <= maxHealth) {
			playerStats.health += revive;
		} else {
			playerStats.health = maxHealth;
		}
	}

	public void SetUnTouchable(bool a) {
		unTouchable = a;
	}

	public bool GetUnTouchable() {
		return unTouchable;
	}

}
