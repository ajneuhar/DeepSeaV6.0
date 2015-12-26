using UnityEngine;
using System.Collections;

public class OpenCloseScene : MonoBehaviour {
	public float moveSpeed = -1;


	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
		transform.position = new Vector3(transform.position.x + moveSpeed, transform.position.y, transform.position.z);

		if (transform.position.x > 20) {
			Application.LoadLevel("MainMenu");
		}

		if(Input.GetKey(KeyCode.Space)) {
			Application.LoadLevel("MainMenu");		
		}
	}
}
