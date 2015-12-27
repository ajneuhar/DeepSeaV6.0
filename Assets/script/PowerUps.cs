using UnityEngine;
using System.Collections;


public class PowerUps : MonoBehaviour {

	public static int powerUp;
	float powerUpTime;

	// For changing the player untouchable.
	private Player player;
	// For update the spear.
	public MoveBullet spear;
	private int playerRevive = 40;


	// For changing the boat sprite.
	private SpriteRenderer boatR;
	public Sprite regBoat;
	public Sprite untouchableBoat;

	// For changing Fire Rate.
	private SpearGun spearGun;

	//For Animation
	Animator anim;

	//For sound
	public static bool tookPowerUp;
	public static bool haveShield;
	public static bool gotLife; 
	public static bool gotExpArrows; 
	public static bool gotDepthArrows; 
	public static bool gotShield; 
	public static bool gotMachineGun;
	public static bool gotElectricArr;
	public static bool haveShieldAndGotHit;

	
	public static bool haveExplodingArrowPowerUp;
	public static bool haveMachingGunPowerUp;
	public static bool haveSpecialArrowPowerUp;
	public static bool haveShieldPowerUp;

	public static bool perLevelLife;




	// Use this for initialization
	void Start () {

		boatR = GameObject.Find("Boat").GetComponent<SpriteRenderer>();
		player = GameObject.Find("Player").GetComponent<Player>();
		spearGun = GameObject.Find("spearGun").GetComponent<SpearGun>();
		anim = GetComponent<Animator>();


	}
	

	private void RandomPowerUp() {
		powerUpTime = Random.Range(20, 31);

		if (perLevelLife) {
			powerUp = 1;
			perLevelLife = false;
		}

		if (spearGun.numOfSpecialArrow >= 4) {
			haveSpecialArrowPowerUp = true;
		} else {
			haveSpecialArrowPowerUp = false;
		}


		bool[] listOfOptions = new bool[] {false, false, haveExplodingArrowPowerUp, haveShieldPowerUp, haveMachingGunPowerUp, haveSpecialArrowPowerUp};
		powerUp = Random.Range(1, 6);

		while (listOfOptions[powerUp]) {
			powerUp = Random.Range(1, 6);
		}

	}

	IEnumerator ActivatePowerUp() {
		switch(powerUp) {
	
		case(1) :
			gotLife = true;
			Debug.Log("got life");
			player.RevivePlayer(playerRevive);
			yield return new WaitForSeconds(3f);

			Destroy(this.gameObject);
			break;
		
		case(2) :
			haveExplodingArrowPowerUp = true;
			gotExpArrows = true; 
			spear.WeaponUpdate(2);

			Debug.Log("Update Weapon To ==============>" + spear.weaponHitLayer + "  " + spear.damageToEnemy);
			Debug.Log(Time.time);
			yield return new WaitForSeconds(powerUpTime);
			spear.WeaponUpdate(1);
			Debug.Log(Time.time);
			haveExplodingArrowPowerUp = false;
			powerUp = 1; // For sound

			Destroy(this.gameObject);
			break;


		/* case(3) :

			gotDepthArrows = true;
			spear.WeaponUpdate(3);
			Debug.Log("Update Weapon To ==============>" + spear.weaponHitLayer + "  " + spear.damageToEnemy);
			yield return new WaitForSeconds(powerUpTime);
			spear.WeaponUpdate(1);
			Destroy(this.gameObject);
			break;
			*/

		case(3) : 
			haveShieldPowerUp = true;
			gotShield = true; 
			boatR.sprite = untouchableBoat;
			haveShieldAndGotHit = true;
			haveShield = true; 
			player.SetUnTouchable(true);
			Debug.Log("is player untouchable =====> " + player.GetUnTouchable());
	
			yield return new WaitForSeconds(20f);

			haveShieldAndGotHit = false;
			boatR.sprite = regBoat;
			player.SetUnTouchable(false);
			Debug.Log("is player untouchable =====> " + player.GetUnTouchable());
			haveShieldPowerUp = false;

			Destroy(this.gameObject);
			break;

		case(4) :
			haveMachingGunPowerUp = true;
			gotMachineGun = true;
			spearGun.fireRate = 4;

			yield return new WaitForSeconds(powerUpTime);
			spearGun.fireRate = 2;

			haveMachingGunPowerUp = false;

			Destroy(this.gameObject);
			break;

		case(5) :
			gotElectricArr = true;
			spearGun.numOfSpecialArrow++;

			Destroy(this.gameObject);
			//: TODO: GUI for special Arrow.
			break;

		}





	}


	//TODO: 
	IEnumerator OnCollisionStay2D (Collision2D other) {


		string tag = other.gameObject.tag;

		if (tag == "Boat" ) {
			GameManager.numOfPowerUps--;
			RandomPowerUp();

			tookPowerUp = true; 
			
			anim.SetBool("tookPowerUp", true);

			GetComponent<Collider2D>().isTrigger = true;

			StartCoroutine(ActivatePowerUp());

			yield return new WaitForSeconds(0.3f);

			GetComponent<Renderer>().sortingOrder = 2;


		}
	}





	
}
