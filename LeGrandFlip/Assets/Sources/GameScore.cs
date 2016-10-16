using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;


public class GameScore : MonoBehaviour 
{
	public int largeBouncerScore	= 0;
	public int mediumBouncerScore	= 0;
	public int smallBouncherScore	= 0;

	private int _redScore			= 0;
	private int _blueScore			= 0;
	private int _greenScore			= 0;
	private int _globalScore		= 0;

	public int redScore { get { return _redScore; } }
	public int blueScore { get { return _blueScore; } }
	public int greenScore { get { return _greenScore; } }
	public int globalscore {get {return _globalScore;}}

	public	RectTransform redScoreImage		= null;
	public 	RectTransform blueScoreImage	= null;
	public  RectTransform greenScoreImage	= null;

	public	RectTransform redIcon			= null;
	public	RectTransform blueIcon			= null;
	public	RectTransform greenIcon         = null;

	public	Text 	      scoreText			= null;

	public	float		  size				= 0f;

	private Vector3 startPosition = Vector3.zero;

	/*
	public struct TypedScore
	{
		public Bouncer.TYPE scoreType = default(Bouncer.TYPE);
		public int			score	  = 0;
	}
	*/

	private static GameScore _instance = null;

	public static GameScore instance
	{
		get { return _instance; }
		set 
		{
			if (_instance == null) {
				_instance = value;
			} else {
				GameObject.Destroy (value.gameObject);
			}
		}
	}

	void Awake ()
	{
		instance = this;
		instance.startPosition = transform.localPosition;
	}

	public void HitBouncer(Bouncer bouncer)
	{
		if (bouncer.bouncerType == Bouncer.TYPE.GREY)
			return;

		int score = GetScore (bouncer.bouncerSize);
		_globalScore += score;

		switch (bouncer.bouncerType) 
		{
			case Bouncer.TYPE.BLUE:
				_blueScore += score;
				break;
			case Bouncer.TYPE.RED:
				_redScore += score;
				break;
			case Bouncer.TYPE.GREEN:
				_greenScore += score;
				break;
		}

		if (_redScore > 0)
			redIcon.gameObject.SetActive (true);
		if (_blueScore > 0)
			blueIcon.gameObject.SetActive (true);
		if (_greenScore > 0)
			greenIcon.gameObject.SetActive (true);

		scoreText.text = _globalScore.ToString ("000000");



		if (_globalScore > 0)
			DrawScore ();
	}

	private int GetScore (Bouncer.SIZE size)
	{
		switch (size) 
		{
		case Bouncer.SIZE.LARGE:
			return largeBouncerScore;
		case Bouncer.SIZE.MEDIUM:
			return mediumBouncerScore;
		case Bouncer.SIZE.SMALL:
			return smallBouncherScore;
		}

		return 0;
	}

	void DrawScore ()
	{
		float red =0f, blue =0f, green = 0f;

		if (_globalScore > 0) {
			red	= (float)_redScore / (float)_globalScore;
			blue	= (float)_blueScore / (float)_globalScore;
			green = (float)_greenScore / (float)_globalScore;
		}

		CleanPourcent (ref red, ref blue, ref green);
		CleanPourcent (ref blue, ref red, ref green);
		CleanPourcent (ref green, ref red, ref green);

		float temp = 1f;

		//Debug.LogFormat ("global {0} red {1} : {2}   blue {3} : {4}  green {5} : {6}",
		//	_globalScore, _redScore, red, _blueScore, blue, _greenScore, green);

		redScoreImage.anchorMax = new Vector2 (temp, 1f);
		temp -= red;
		redScoreImage.anchorMin = new Vector2 (temp, 0f);
		ResetAfterAnchor (redScoreImage);

		/*
		redIcon.anchorMax = new Vector2 (temp, 1f);
		redIcon.anchorMin = new Vector2 (temp, 1f);
		ResetAfterAnchor (redIcon);
		*/

		redIcon.anchoredPosition = new Vector3 (Mathf.Max(220f, size * temp), 0f, 0f);

		blueScoreImage.anchorMax = new Vector2 (temp, 1f);
		temp -= blue;
		blueScoreImage.anchorMin = new Vector2 (temp, 0f);
		ResetAfterAnchor (blueScoreImage);

		/*
		blueIcon.anchorMax = new Vector2 (temp, 1f);
		blueIcon.anchorMin = new Vector2 (temp, 1f);
		ResetAfterAnchor (blueIcon);
		*/

		blueIcon.anchoredPosition = new Vector3 (Mathf.Max(100f, size * temp), 0f, 0f);

		greenScoreImage.anchorMax = new Vector2 (temp, 1f);
		temp -= green;
		greenScoreImage.anchorMin = new Vector2 (temp, 0f);
		ResetAfterAnchor (greenScoreImage);

		/*
		greenIcon.anchorMax = new Vector2 (temp, 1f);
		greenIcon.anchorMin = new Vector2 (temp, 1f);	
		ResetAfterAnchor (greenIcon);	
		*/

		greenIcon.anchoredPosition = new Vector3 (10f, 0f, 0f);
	}

	void ResetAfterAnchor (RectTransform t)
	{
		t.position = Vector3.zero;
		t.offsetMax	= Vector2.zero;
		t.offsetMin = Vector2.zero;
	}

	// Update is called once per frame
	void Update () {
	
	}

	public void Reset ()
	{
		_globalScore = 0;
		_redScore = 0;
		_blueScore = 0;
		_greenScore = 0;

		redIcon.gameObject.SetActive (false);
		greenIcon.gameObject.SetActive (false);
		blueIcon.gameObject.SetActive (false);

		DrawScore ();

		MoveToDefault ();
	}

	void CleanPourcent (ref float v1, ref float v2, ref float v3)
	{
		if (v1 > 0f && v1 < 0.05f) 
		{
			if (v2 > 0.1f) 
			{
				float diff = 0.05f - v1;
				v1 += diff;
				v2 -= diff;
			}
			else if (v3 > 0.1f) 
			{
				float diff = 0.05f - v3;
				v1 += diff;
				v3 -= diff;
			}
		}		
	}

	public void MoveToLeaderboard ()
	{
		transform.localPosition = new Vector3 (0, 314f, 0f);
		transform.localScale = new Vector3 (2f, 2f, 2f);
	}

	public void MoveToDefault ()
	{
		transform.localPosition = startPosition;
		transform.localScale = new Vector3 (1f, 1f, 1f);
	}

}
