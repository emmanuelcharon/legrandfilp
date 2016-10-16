using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Collider2D))]
public class Brick : MonoBehaviour {

	public SpriteRenderer sr;

	void OnCollisionEnter2D(Collision2D other) {

		if (other.gameObject.CompareTag ("Player")) {
			
			this.GetComponent<Collider2D>().enabled = false;

			AudioSource audioSource = this.gameObject.AddComponent<AudioSource> ();
			audioSource.clip = GameManager.s.soundsBricks [Random.Range (0, GameManager.s.soundsBricks.Length)];
			audioSource.loop = false;
			audioSource.playOnAwake = false;
			audioSource.volume = .75f;
			audioSource.Play ();

			iTween.FadeTo(sr.gameObject, iTween.Hash ("alpha", 0f, "time", 0.3f, "delay", 0.2f));
			iTween.ShakeScale (sr.gameObject, iTween.Hash ("amount", 0.2f * sr.transform.localScale, "time", 0.6f,
				"oncompletetarget", this.gameObject, "oncomplete", "DestroyBrick"));


		}
	}

	public void DestroyBrick() {
		Destroy (this.gameObject, 4f);
	}

}
