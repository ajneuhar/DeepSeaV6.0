﻿using UnityEngine;
using System.Collections;
using Pathfinding;

[RequireComponent(typeof (Rigidbody2D))]
[RequireComponent(typeof (Seeker))]

public class EnemyAI : MonoBehaviour {
	// What to chase?
	public Transform target;

	// How many times each second we will update our path.
	public float updateRate = 0.5f;

	// Caching.
	private Seeker seeker;
	private Rigidbody2D rb;

	// the calculated path
	public Path path;

	// The AI's speed per second
	public float speed = 300f;
	public ForceMode2D fMode;

	[HideInInspector]
	public bool pathIsEnded = false;

	// The max distance from the AI to a waypoint for it to continue to the next waypoint.
	public float nextWaypointDistance = 3;

	// The waypoint we are currently moving towards.
	private int currentWayPoint = 0;

	private bool searchingForPlayer = false;

	// If doing a default path or pathfinder. 
	private bool startSeeking = false;


	private SpriteRenderer sprite;
	float alphaColor;
	


	void Start () {
		target = GameObject.Find("Boat").GetComponent<Transform>();
		StartCoroutine(RandomLayerUp());
		sprite = GetComponent<SpriteRenderer>();
		Path0();
	}

	IEnumerator RandomLayerUp() {
		yield return new WaitForSeconds(Random.Range(6, 11));
		iTween.ScaleTo(this.gameObject, new Vector3(25f, 25f, 25f), 2f);
		sprite.sortingOrder++;
		//iTween.ColorTo(this.gameObject, new Color(100f, 100f, 100f), 1f);
		// player and enemy collide and wont overlap.
		GetComponent<BoxCollider2D>().isTrigger = false;
		yield return new WaitForSeconds(Random.Range(4, 7));
		iTween.ScaleTo(this.gameObject, new Vector3(30f, 30f, 30f), 1f);
		sprite.sortingOrder++;
		//iTween.ColorTo(this.gameObject, new Color(100f, 100f, 100f), 1f);
	}


	void Path0() {
		iTween.MoveTo(this.gameObject ,iTween.Hash("path", iTweenPath.GetPath(LevelManager.startPath), "speed", 50,
		                                           "easetype", iTween.EaseType.easeInOutSine,  "onComplete", "SeekPlayer"));
	}

	/*
	void Path1() {
        // player and enemy collide and wont overlap.
        // GetComponent<BoxCollider2D>().isTrigger = false;
		iTween.MoveTo(this.gameObject ,iTween.Hash("path", iTweenPath.GetPath(LevelManager.secondPath), "speed", 50, 
		                                           "easetype", iTween.EaseType.easeInOutSine, "onComplete", "SeekPlayer"));

	}*/

	/*
	string ChoosePath () {
		switch (Random.Range(0, 5)) {
		case(0): 
			return "enemyPath1";
		case(1):
			return "enemyPath2";
		case(2):
			return "enemyPath3";
		case(3):
			return "enemyPath4";
		default:
			return "enemyPath5";

		}
	}*/


	public void SeekPlayer () {
		seeker = GetComponent<Seeker>();
		rb = GetComponent<Rigidbody2D>();
		
		Debug.Log("target is ----> " + target);
		if (target == null) {
			if (!searchingForPlayer) {
				searchingForPlayer = true;
				StartCoroutine(SearchForPlayer());
			}
			return;
		}
		
		StartCoroutine (UpdatePath());
	}

	// Called if no target has entered.
	IEnumerator SearchForPlayer () {
		GameObject sResult = GameObject.FindGameObjectWithTag ("Boat");
		if (sResult == null) {
			yield return new WaitForSeconds (0.5f);
			StartCoroutine(SearchForPlayer());
		} else {
			target = sResult.transform;
			searchingForPlayer = false;
			StartCoroutine(UpdatePath());

			return false;
		}
	}



	IEnumerator UpdatePath () {
		if (target == null) {
			if (!searchingForPlayer) {
				searchingForPlayer = true;
				StartCoroutine(SearchForPlayer());
			}
			return false;
		}

		// Start a new path to the target position, return the result to the OnPathComplete method.
		seeker.StartPath(transform.position, target.position, OnPathComplete);

		yield return new WaitForSeconds ( 1f/updateRate );
		StartCoroutine (UpdatePath ());
	}

	public void OnPathComplete(Path p) {
		//Debug.LogError ("We got a path, do we have an error? " + p.error);
		if (!p.error) {
			path = p;
			currentWayPoint = 0;
		}
	}


	void FixedUpdate() {
		if (target == null) {
			if (!searchingForPlayer) {
				searchingForPlayer = true;
				StartCoroutine(SearchForPlayer ());
			}
			return;
		} else {
			// Always look at player.
			Vector3 diraction = target.position - transform.position;
			float angle = Mathf.Atan2(diraction.y,diraction.x) * Mathf.Rad2Deg - 90f;
			transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
		}

		if (path == null) {
			return;
		}

		if (currentWayPoint >= path.vectorPath.Count) {
			if (pathIsEnded) {
				return;
			}

			//Debug.Log ("End of path reached");
			pathIsEnded = true;
			return;
		}

		pathIsEnded = false;

		// Diraction to the next waypoint.
		Vector3 dir = (path.vectorPath[currentWayPoint] - transform.position).normalized;

		dir *= speed * Time.fixedDeltaTime;

		// Move the AI.
		rb.AddForce(dir, fMode);

		float dist = Vector3.Distance (transform.position, path.vectorPath[currentWayPoint]);
		if (dist < nextWaypointDistance) {
			currentWayPoint++;
			return;
		}


	}

}
