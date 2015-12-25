﻿using UnityEngine;
using System.Collections;
using UnityEngine.UI;


public class CanvasManager : MonoBehaviour {
	
	public GameObject canvasOne;
	public GameObject canvasTwo;
	public GameObject canvasThree;

	public Text highScore;



	// Use this for initialization
	void Start () {
		canvasOne.active = true;



	}

	public void onClickStartButton() {
		Application.LoadLevel("MainScene");
	}

	public void onClickExitButton() {
		Application.Quit();

	}

	public void onClickBackButton () {
		canvasOne.active = true;
		canvasTwo.active = false;
	}

	public void onClickTutorialButton()
	{
		Application.LoadLevel("Tutorial");
	}

	public void onClickCreditsButton () {
		canvasTwo.active = true;
		canvasOne.active = false;
	}
	public void onClickHightScoreButton () {
		canvasThree.active = true;
		canvasOne.active = false;
		highScore.text = "Highest Score is: " + PlayerPrefs.GetInt("HighScore");
	}
}