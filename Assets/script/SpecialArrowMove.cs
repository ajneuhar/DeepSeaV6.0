using UnityEngine;
using System.Collections;

public class SpecialArrowMove : MonoBehaviour {

	public int weaponHitLayer;
	public int damageToEnemy;
	public int moveSpeed = 100; 





	public GameObject electicity;
	
	
	
	
	
	// Update is called once per frame
	void Update () {
		transform.Translate (Vector3.right * Time.deltaTime * moveSpeed);
		Destroy (gameObject, 2);
	}
	
	
	
	void OnTriggerEnter2D (Collider2D other) {
		string tag = other.tag;
		Debug.Log("Spear Hit something....... " + tag);
		
		
		if (tag == "enemy1" || tag == "enemy2" || tag == "enemy3" || tag == "enemy4") {
			Enemy enemy = other.GetComponent<Enemy>();
			SpriteRenderer sprite = enemy.GetComponent<SpriteRenderer>();
			Debug.Log("layer is :      " + sprite.sortingOrder);
			Animator anim = other.gameObject.GetComponent<Animator>();
			
			if (!anim.GetBool("death")) {
				
				
				Instantiate(electicity, new Vector3(0f, 0f, 0f), Quaternion.identity);
				//TODO : kill all enemys.
				transform.position = new Vector3(400f, 50f,0f);

				GameObject[] enemy1 = GameObject.FindGameObjectsWithTag("enemy1");
				GameObject[] enemy2 = GameObject.FindGameObjectsWithTag("enemy2");
				GameObject[] enemy3 = GameObject.FindGameObjectsWithTag("enemy3");

				int numOfEnemys = enemy1.Length + enemy2.Length + enemy3.Length;




				for (int i = 0; i < enemy1.Length; i++) {
					if(enemy1[i] != null) {
						enemy1[i].GetComponent<Enemy>().DamageEnemy(16);
					}
				}
				for (int i = 0; i < enemy2.Length; i++) {
					if(enemy2[i] != null) {
						enemy2[i].GetComponent<Enemy>().DamageEnemy(16);
					}
				}
				for (int i = 0; i < enemy3.Length; i++) {
					if(enemy3[i] != null) {
						enemy3[i].GetComponent<Enemy>().DamageEnemy(16);
					}
				}
				
				Destroy(this.gameObject);
				

			}
		}	
	}
	

}

