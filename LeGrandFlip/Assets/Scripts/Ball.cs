using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Collider2D))]
public class Ball : MonoBehaviour {

	public SpriteRenderer sr;
	Rigidbody2D rb;
	float maxSpeed = 3000f;

	public TrailRenderer trailRenderer;
	float minTrailSpeed = 2200f;


	void Start () {
		initialSpriteScale = sr.transform.localScale;
		rb = GetComponent<Rigidbody2D> ();
	}
	
	void Update () {
		rb.velocity = Vector2.ClampMagnitude (rb.velocity, maxSpeed);

		trailRenderer.enabled = rb.velocity.magnitude > minTrailSpeed; 

	}

	private string tweenName;
	private Vector3 initialSpriteScale; 

	void OnCollisionExit2D(Collision2D other) {

		if (!string.IsNullOrEmpty (tweenName)) {
			iTween.StopByName(tweenName);
			sr.transform.localScale = initialSpriteScale;
		}

		tweenName = name + System.Guid.NewGuid ().ToString();

		iTween.ScaleFrom (sr.gameObject, iTween.Hash ("name", tweenName, "scale", 1.5f * initialSpriteScale , "time", 0.2f));
	}
}
