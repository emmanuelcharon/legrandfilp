using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Collider2D))]
public class Ball : MonoBehaviour {

	public SpriteRenderer sr;
	public Rigidbody2D rb;
	float maxSpeed = 2800f;

	public TrailRenderer trailRenderer;
	float minTrailSpeed = 2200f;

	//public ParticleSystem particles;


	void Start () {
		initialSpriteScale = sr.transform.localScale;
	}
	
	void Update () {
		rb.velocity = Vector2.ClampMagnitude (rb.velocity, maxSpeed);
		trailRenderer.enabled = rb.velocity.magnitude > minTrailSpeed; 
	}

	private string tweenName;
	private Vector3 initialSpriteScale; 

	void OnCollisionExit2D(Collision2D other) {

		if (!string.IsNullOrEmpty (tweenName)) 
		{
			try
			{
				iTween.StopByName(tweenName);
			}
			catch(System.Exception) 
			{
				tweenName = string.Empty;
			}

			sr.transform.localScale = initialSpriteScale;
		}

		tweenName = name + System.Guid.NewGuid ().ToString();

		iTween.ScaleFrom (sr.gameObject, iTween.Hash ("name", tweenName, "scale", 1.3f * initialSpriteScale , "time", 0.25f));

		//particles.Play ();
	}

	public void ResetTween ()
	{
		tweenName = string.Empty;
	}

	void OnDestroy ()
	{
		tweenName = string.Empty;
	}
		
}
