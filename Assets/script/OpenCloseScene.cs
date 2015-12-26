using UnityEngine;
using System.Collections;

public class OpenCloseScene : MonoBehaviour {
	public float moveSpeed;


	// Use this for initialization
	void Start () {
		StartCoroutine(RunMovie());
	}
	
	// Update is called once per frame
	void Update () {

		if(Input.GetKey(KeyCode.Space)) {
			Application.LoadLevel("MainMenu");	
		}
	}

	IEnumerator RunMovie() {
		yield return new WaitForSeconds(1f);
		iTween.MoveTo(this.gameObject ,iTween.Hash("path", iTweenPath.GetPath("Movie"), "time", 15f,
		                                           "easetype", iTween.EaseType.easeInOutSine,  "onComplete", "NextScene"));
	}

	void NextScene() {
		transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z);
		Application.LoadLevel("MainMenu");
	}


}
