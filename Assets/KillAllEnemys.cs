using UnityEngine;
using System.Collections;

public class KillAllEnemys : MonoBehaviour {

	Player player;


	// Use this for initialization
	IEnumerator Start () {
		player = FindObjectOfType(typeof(Player)) as Player;
		player.SetUnTouchable(true);

		for (int i = 0; i < 4; i++) {
			KillEnemys();
			yield return new WaitForSeconds(0.5f);
		}
		
		player.SetUnTouchable(false);
		Destroy(this);
	}


	void KillEnemys () {
		Enemy[] enemys = FindObjectsOfType(typeof(Enemy)) as Enemy[];
		Debug.Log("Enemys[] =======> " + enemys.Length);
		
		foreach(Enemy enemytemp in enemys) {
			enemytemp.DamageEnemy(16);
		}
	}

}
