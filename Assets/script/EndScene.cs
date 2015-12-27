using UnityEngine;
using System.Collections;

public class EndScene : MonoBehaviour {

	// Use this for initialization
	void Start () {
		StartCoroutine(NewGame());
	}

	IEnumerator NewGame() {
		yield return new WaitForSeconds(7.5f);
		Application.LoadLevel("MainMenu");
	}

	// Update is called once per frame
	void Update () {
	
	}
}
