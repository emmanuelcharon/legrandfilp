using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class Door : MonoBehaviour {

	public int SceneIndexToLoad = 0;

	void OnCollisionEnter2D(Collision2D other) {
		if (other.gameObject.CompareTag ("Player")) {

			GameManager.s.LoadFlipper (SceneIndexToLoad);


		}
	}
}
