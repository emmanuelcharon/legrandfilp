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
	public Text levelNameText;

	public Transform BallBottomStartPointInLevel;
	public Transform BallTopStartPointInLevel;

	public string overrideStartLevel;

	public AudioSource backgroundMusic;

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

		if (!string.IsNullOrEmpty (overrideStartLevel)) { 
			GameObject root = GameObject.Find (overrideStartLevel);
			if (root != null) {
				LoadFlipper (root, false);
			}
		} 

		GameTimer.instance.StartTimer ();
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

		LevelSpecificArt specific = destinationLevelRoot.GetComponent<LevelSpecificArt> ();

		if (specific.name == "Primaire" && !backgroundMusic.isPlaying) {
			backgroundMusic.Play ();
		}

		GameObject startLevelTextGO = (GameObject)Instantiate (startlevelTextPrefab, textPopupContainer.transform.position, Quaternion.identity);
		startLevelTextGO.transform.SetParent (textPopupContainer.transform, true);
		startLevelTextGO.transform.localScale = Vector3.one;
		Text startLevelText = startLevelTextGO.GetComponent<Text> ();
		startLevelText.text = levelNameText.text = specific.levelName;

		iTween.ScaleFrom (startLevelTextGO, iTween.Hash ("scale", Vector3.zero, "time", 0.5f, "easetype", iTween.EaseType.easeOutCubic));

		StartCoroutine(DisableTextIn (startLevelText, 2.5f));
	}

	public GameObject startlevelTextPrefab;

	private IEnumerator DisableTextIn(Text text, float seconds) {
		iTween.ScaleTo (text.gameObject, iTween.Hash ("scale", Vector3.zero, "time", 0.5f, "delay", 1f, "easetype", iTween.EaseType.easeOutCubic));
		yield return new WaitForSeconds (seconds);
		Destroy (text.gameObject);
	}

	public Color[] couleursRampes;

	public AudioClip[] soundsBricks;
	public AudioClip soundBouncer;

	public AudioSource leftFlipperSoundOn;
	public AudioSource leftFlipperSoundOff;

	public AudioSource rightFlipperSoundOn;
	public AudioSource rightFlipperSoundOff;

	public GameObject textPopupPrefab;
	public GameObject textPopupContainer;

	private List<string> GreyTexts = new List<string>(){"Lève toi!", "Bosse!", "Travaille!", "Débout!"};

	public Color textColorGrey;
	public Color textColorBlue;
	public Color textColorGreen;
	public Color textColorRed;

	public void CreateScoreTextPopup(Vector3 position, int score, Color textColor, Bouncer.SIZE bouncerSize) {

		string t = string.Format ("{0}!", score);
		Vector3 initialScale = Vector3.one;

		if (score == 0) {
			t = "";//GreyTexts[Random.Range(0, GreyTexts.Count)];
			initialScale = 0.8f * Vector3.one;
		}

		GameObject textPopupGO = (GameObject)Instantiate (textPopupPrefab, position, Quaternion.identity);
		textPopupGO.transform.SetParent (textPopupContainer.transform, true);
		textPopupGO.transform.localScale = initialScale;


		float textAngle = Random.Range (-35f, 35f);

		textPopupGO.transform.Rotate (new Vector3(0f, 0f, textAngle));

		Text textPopup = textPopupGO.GetComponent<Text> ();
		textPopup.text = t;
		textPopup.color = textColor;


		//iTween.FadeTo(textPopupGO, iTween.Hash ("alpha", 0f, "time", 0.5f, "delay", 0.5f));
		float tweenDist = tweenDistFromBouncerSize(bouncerSize);

		iTween.ScaleTo (textPopupGO, iTween.Hash ("scale", 2f * textPopupGO.transform.localScale, "time", 0.7f,
			"easetype", iTween.EaseType.easeOutCubic));
		iTween.MoveTo (textPopupGO, iTween.Hash (
			"x", textPopupGO.transform.position.x - tweenDist * Mathf.Sin(textAngle*Mathf.Deg2Rad),
			"y", textPopupGO.transform.position.y + tweenDist * Mathf.Cos(textAngle*Mathf.Deg2Rad),
			"time", 1f,
			"easetype", iTween.EaseType.easeOutCubic));


		Destroy (textPopupGO, 1.1f);

	}

	public Color colorFromBouncerType(Bouncer.TYPE bouncerType) {
		switch (bouncerType) {
		case Bouncer.TYPE.GREY:
			return textColorGrey;
		case Bouncer.TYPE.RED:
			return textColorRed;
		case Bouncer.TYPE.BLUE:
			return textColorBlue;
		case Bouncer.TYPE.GREEN:
			return textColorGreen;
		default:
			return Color.white;
		}
	}

	public float tweenDistFromBouncerSize(Bouncer.SIZE bouncerSize) {
		switch (bouncerSize) {
		case Bouncer.SIZE.SMALL:
			return 100f;
		case Bouncer.SIZE.MEDIUM:
			return 180f;
		case Bouncer.SIZE.LARGE:
			return 250f;
		default:
			return 100f;
		}
	}


}
