using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;


public class SpearGun : MonoBehaviour {



	public static int damageToPlayer = 1;

	public LayerMask whatToHit;
	public float fireRate = 0;

	public Transform bulletTrail;
	float timeToSpawn = 0;
	public float spawnRate = 10;

	public GameObject spear;

	float timeTofire = 0;
	Transform firepoint;

	public static bool gunShot;

	public GameObject specialArrow;
	public int numOfSpecialArrow;
	bool isSpecialArrow;
	public Text temp;
	





	// Use this for initialization
	void Awake () {
		fireRate = 2;
		firepoint = transform.FindChild("firepoint");
		if (firepoint == null) {
			Debug.LogError("No firepoint?! What?!");
		}
		numOfSpecialArrow = 1;
		isSpecialArrow = false;
	}
	
	// Update is called once per frame
	void Update () {
		temp.text = "x  " + numOfSpecialArrow;


		if (Time.timeScale != 0) {
			if (fireRate == 0) {
				if (Input.GetKeyDown(KeyCode.Space)) {
					fire ();
				}
			}
			else {
				if (Input.GetKey (KeyCode.Space) && Time.time > timeTofire) {
					timeTofire = Time.time + 1/fireRate;
					fire ();
					} else if (Input.GetKeyDown (KeyCode.LeftShift) && Time.time > timeTofire && numOfSpecialArrow > 0) {
					timeTofire = Time.time + 1/fireRate;
					numOfSpecialArrow--;
					isSpecialArrow = true;
					fire ();

				}
			}
		}
	}

	void fire () {
		gunShot = true; 
		Vector2 mousePostion = new Vector2 (Camera.main.ScreenToWorldPoint (Input.mousePosition).x, 
		                                    Camera.main.ScreenToWorldPoint (Input.mousePosition).y);

		Vector2 firePointPosition = new Vector2 (firepoint.position.x, firepoint.position.y);
		RaycastHit2D hit = Physics2D.Raycast (firePointPosition, mousePostion-firePointPosition, 100, whatToHit);
		if (Time.time >= timeToSpawn) {
			if (isSpecialArrow) {
				effect ();
			} else {
				shootBullet();
			}



			timeToSpawn = Time.time + 1/spawnRate;
		} 
		Debug.DrawLine (firePointPosition, (mousePostion-firePointPosition) * 100, Color.black);
		if (hit.collider != null) {
			Debug.DrawLine (firePointPosition, hit.point, Color.red);


		}
	}
	public int getNum(){
		return numOfSpecialArrow;
	}


	void effect () {
		Instantiate (specialArrow, firepoint.position, firepoint.rotation);
		isSpecialArrow = false;
	}

	void shootBullet () {
		Instantiate (spear, firepoint.position, firepoint.rotation);
	}
}
