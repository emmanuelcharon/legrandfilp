using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class GameManager : MonoBehaviour {

	public static GameManager s;

	public Ball playerBall;
	[HideInInspector]
	public bool touchedInputOnce;

	public GameObject startTexts;
	public Text startLevelText;
	public List<Vector3> sceneRootPositions;

	public Transform BallStartPointInLevel;



	void Awake() {
		if (s != null) {
			Destroy (this.gameObject);
		} else {
			s = this;
			DontDestroyOnLoad (s.gameObject);
		}

		touchedInputOnce = false;
	}


	void Start () {
		PauseBeforeStartGame ();
	}


	public void PauseBeforeStartGame() {
		playerBall.rb.isKinematic = true; // will stop movement
	}

	public void StartGame() {
		playerBall.rb.isKinematic = false;
		//iTween.FadeTo(startTexts, iTween.Hash ("alpha", 0f, "time", 2f, "includechildren", true));
		startTexts.SetActive (false);
	}

	public void LoadFlipper(int index) {

		if (index < 0 || index >= sceneRootPositions.Count) {
			Debug.LogError ("No scene at index " + index);
			return;
		}

		this.transform.position = sceneRootPositions[index]; // camera
		playerBall.transform.position = BallStartPointInLevel.position;
		playerBall.rb.velocity = new Vector3 (0f, 1000f, 0f);

		if (index > 0) {
			startLevelText.text = "Level " + index;
			startLevelText.enabled = true;
			StartCoroutine(DisableTextIn (startLevelText, 3f));
		}
	}

	private IEnumerator DisableTextIn(Text text, float seconds) {
		yield return new WaitForSeconds (seconds);
		text.enabled = false;
	}



}
