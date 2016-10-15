using UnityEngine;
using System.Collections;

[RequireComponent(typeof(BoxCollider2D))]
public class DeathZone : MonoBehaviour {

	public GameObject playerBallPrefab;
	public Transform restart;

	// Use this for initialization
	void OnTriggerEnter2D(Collider2D other) {
		if (other.CompareTag ("Player")) {
			Destroy (other);
			Instantiate (playerBallPrefab, restart.position, Quaternion.identity);
		}
	}
}
