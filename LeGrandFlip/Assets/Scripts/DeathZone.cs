﻿using UnityEngine;
using System.Collections;

[RequireComponent(typeof(BoxCollider2D))]
public class DeathZone : MonoBehaviour {

	public GameObject playerBallPrefab;
	public Transform restart;

	// Use this for initialization
	void OnTriggerEnter2D(Collider2D other) {
		if (other.CompareTag ("Player")) {

			other.transform.position = restart.position;
			other.GetComponent<Rigidbody2D> ().velocity = Vector2.zero;

			//Destroy (other.gameObject);
			//GameObject ball = (GameObject) Instantiate (playerBallPrefab, restart.position, Quaternion.identity);
			//GameManager.s.playerBall = ball.GetComponent<Ball> ();
		}
	}
}
