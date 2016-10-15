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
			Debug.Log (tweenName);
			iTween.ShakeScale (sr.gameObject, iTween.Hash ("name", tweenName, "amount", 1.01f * initialSpriteScale , "time", 0.4f));

			//iTween.PunchScale (this.gameObject, iTween.Hash ("amount", "x", 2f, "y", 2f, "time", 1f));
		}
	}
}
