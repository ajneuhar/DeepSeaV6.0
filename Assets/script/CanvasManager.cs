using UnityEngine;
using System.Collections;
using UnityEngine.UI;


public class CanvasManager : MonoBehaviour {
	
	public GameObject canvasOne;
	public GameObject canvasTwo;
	public GameObject canvasThree;
	public GameObject canvasFour; 

	public Text highScore;



	// Use this for initialization
	void Start () {
		// canvasOne.active = true;
	}

	public void onClickStartButton() {
		Application.LoadLevel("MainScene");
	}

	/*
	public void onClickExitButton() {
		Application.Quit();

	}*/

	public void onClickBackButton () {
		canvasOne.active = true;
		canvasTwo.active = false;
		canvasThree.active = false;
		canvasFour.active = false;
	}

	public void onClickHowToPlay() {
		canvasOne.active = false; 
		canvasFour.active = true; 
	}

	/*
	public void onClickTutorialButton()
	{
		Application.LoadLevel("Tutorial");
	}*/

	public void onClickCreditsButton () {
		canvasTwo.active = true;
		canvasOne.active = false;
	}
	public void onClickHightScoreButton () {
		canvasThree.active = true;
		canvasOne.active = false;
		highScore.text = "" + PlayerPrefs.GetInt("HighScore");
	}
}