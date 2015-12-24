using UnityEngine;
using System.Collections;

public class SpecialArrowMove : MonoBehaviour {

	public int weaponHitLayer;
	public int damageToEnemy;
	public int moveSpeed = 100; 

	// For Sound
	public static bool electricSound;


	public GameObject electicity;
	public GameObject enemy1Death;
	public GameObject enemy2Death;
	public GameObject enemy3Death;

	Player player;
	
	
	void Start() {
		player = GameObject.Find("Player").GetComponent<Player>();
	}
	

	
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

				electricSound = true; 

				// kill first the enemy we hit.
				enemy.DamageEnemy(16);
				Instantiate(electicity, new Vector3(0f, 0f, 0f), Quaternion.identity);

				//TODO : kill all enemys.
				transform.position = new Vector3(400f, 50f,0f);
			

				Destroy(this.gameObject);
				

			}
		}	
	}
	

}

