using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Collider2D))]
public class Brick : MonoBehaviour {

	public SpriteRenderer sr;

	void OnCollisionEnter2D(Collision2D other) {

		if (other.gameObject.CompareTag ("Player")) {
			
			this.GetComponent<Collider2D>().enabled = false;
			iTween.ShakeScale (sr.gameObject, iTween.Hash ("amount", 0.2f * sr.transform.localScale, "time", 0.6f,
				"oncompletetarget", this.gameObject, "oncomplete", "DestroyBrick"));
		}
	}

	public void DestroyBrick() {
		Destroy (this.gameObject);
	}

}
