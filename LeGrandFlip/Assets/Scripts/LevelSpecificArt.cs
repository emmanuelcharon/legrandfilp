using UnityEngine;
using System.Collections;

public class LevelSpecificArt : MonoBehaviour {


	public string levelName;
	public int couleurIdx;

	void Awake() {
		if (string.IsNullOrEmpty (levelName)) {
			levelName = this.name;
		}
	}

	void Start () {
		PaintBordersAndRamps(GameManager.s.couleursRampes [couleurIdx]);

	}

	private void PaintBordersAndRamps(Color c) {
		Transform borders = transform.Find ("EnvironmentCommon/Borders");

		foreach(Transform t in borders.GetComponentsInChildren<Transform>()) {
			SpriteRenderer sr = t.GetComponent<SpriteRenderer> ();

			if (sr != null) {
				sr.color = c;
			}

		}

		Transform specificEmploi = transform.Find ("EnvironmentSpecificEmploi");

		if (specificEmploi != null) {
			foreach (Transform t in specificEmploi.GetComponentsInChildren<Transform>()) {
				SpriteRenderer sr = t.GetComponent<SpriteRenderer> ();
				FlipControl p = t.parent.GetComponent<FlipControl> ();

				if (sr != null && p == null) {
					sr.color = c;
				}
			}
		}
	}
	

}
