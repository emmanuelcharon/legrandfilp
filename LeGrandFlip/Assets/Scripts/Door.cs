using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class Door : MonoBehaviour {

	public enum TYPE {START_NEXT_AT_TOP, START_NEXT_AT_BOTTOM}

	public TYPE doorType;
	public int destinationLevel;

	void OnCollisionEnter2D(Collision2D other) {
		if (other.gameObject.CompareTag ("Player")) {
			GameManager.s.LoadFlipper (destinationLevel, doorType==TYPE.START_NEXT_AT_TOP);
		}
	}
}
