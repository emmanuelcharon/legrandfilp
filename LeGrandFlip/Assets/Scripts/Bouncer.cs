using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Collider2D))]
public class Bouncer : MonoBehaviour {

	public SpriteRenderer sr;

	private string tweenName;
	private Vector3 initialSpriteScale; 

	void Awake() {
		initialSpriteScale = sr.transform.localScale;
	}

	void OnCollisionEnter2D(Collision2D other) {

		if (other.gameObject.CompareTag ("Player")) {

			if (!string.IsNullOrEmpty (tweenName)) {
				iTween.StopByName(tweenName);
				sr.transform.localScale = initialSpriteScale;
			}

			tweenName = name + System.Guid.NewGuid ().ToString();
			iTween.ShakeScale (sr.gameObject, iTween.Hash ("name", tweenName, "amount", 0.5f * initialSpriteScale , "time", 0.6f));
		}
	}
}
