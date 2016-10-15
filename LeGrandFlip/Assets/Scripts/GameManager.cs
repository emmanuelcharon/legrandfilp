using UnityEngine;
using UnityEngine.SceneManagement;
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
	public Text levelNameText;

	public Transform BallBottomStartPointInLevel;
	public Transform BallTopStartPointInLevel;

	public List<GameObject> levelRoots;

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

	public void LoadFlipper(GameObject destinationLevelRoot, bool start_at_top) {
		
		this.transform.position = destinationLevelRoot.transform.position; // camera & UI

		if (start_at_top) {
			// ball starts at the top of a screen
			playerBall.transform.position = BallTopStartPointInLevel.position;
			playerBall.rb.velocity = new Vector3 (0f, 0, 0f);
		} else {
			// ball starts a the bottom of a screen
			playerBall.transform.position = BallBottomStartPointInLevel.position;
			playerBall.rb.velocity = new Vector3 (Random.Range(-250f, 250f), 1000f, 0f);
		}

		startLevelText.text = destinationLevelRoot.name;
		startLevelText.enabled = true;
		levelNameText.text = destinationLevelRoot.name;
		StartCoroutine(DisableTextIn (startLevelText, 2f));
	}

	private IEnumerator DisableTextIn(Text text, float seconds) {
		yield return new WaitForSeconds (seconds);
		text.enabled = false;
	}



}
