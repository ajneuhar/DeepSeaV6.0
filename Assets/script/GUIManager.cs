using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GUIManager : MonoBehaviour {
	
	public Image bar; 

	public Image womanFace;

	public Sprite face0;
	public Sprite face1;
	public Sprite face2;
	public Sprite face3;
	public Sprite face4;
	public Sprite face5;
	public Sprite face6;
	public Sprite face7;
	public Sprite face8;
	public Sprite face9;


	

	// Update is called once per frame
	void Update()
	{
		switch (Player.playerStats.health) {
		case(100):
			bar.fillAmount = 1f;
			womanFace.sprite = face0;
			break; 
		case(90):
			womanFace.sprite = face1;
			bar.fillAmount = 0.9f;
			break;
		case(80):
			womanFace.sprite = face2;
			bar.fillAmount = 0.8f;
			break;
		case(70):
			womanFace.sprite = face3;
			bar.fillAmount = 0.7f;
			break;
		case(60):
			womanFace.sprite = face4;
			bar.fillAmount = 0.6f;
			break;
		case(50):
			womanFace.sprite = face5;
			bar.fillAmount = 0.5f;
			break;
		case(40):
			//womanFace.sprite = face6;
			bar.fillAmount = 0.4f;
			break; 
		case(30):
			womanFace.sprite = face7;
			bar.fillAmount = 0.3f;
			break;
		case(20):
			womanFace.sprite = face8;
			bar.fillAmount = 0.2f;
			break;
		case(10):
			womanFace.sprite = face9;
			bar.fillAmount = 0.1f;
			break;
		default:
			bar.fillAmount = 0f;
			break;
		}
	}
}