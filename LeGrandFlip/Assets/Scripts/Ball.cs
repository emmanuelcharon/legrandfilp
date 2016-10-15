using UnityEngine;
using System.Collections;

public class Ball : MonoBehaviour {

	Rigidbody2D rb;
	float maxSpeed = 3000f;

	public TrailRenderer trailRenderer;
	float minTrailSpeed = 2200f;


	void Start () {
		rb = GetComponent<Rigidbody2D> ();
	}
	
	void Update () {
		rb.velocity = Vector2.ClampMagnitude (rb.velocity, maxSpeed);

		trailRenderer.enabled = rb.velocity.magnitude > minTrailSpeed; 

	}
}
