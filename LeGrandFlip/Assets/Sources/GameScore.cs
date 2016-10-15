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

	public	RectTransform redScoreImage		= null;
	public 	RectTransform blueScoreImage	= null;
	public  RectTransform greenScoreImage	= null;

	public	Text 	      scoreText			= null;

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
	}

	public void HitBouncer(Bouncer bouncer)
	{
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

		scoreText.text = _globalScore.ToString ("000000000");

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
		float red	= _redScore / _globalScore;
		float blue	= _blueScore / _globalScore;
		float green = _greenScore / _globalScore;

		float temp = 1f;

		redScoreImage.anchorMax = new Vector2 (temp, 1f);
		temp -= red;
		redScoreImage.anchorMin = new Vector2 (temp, 0f);
		redScoreImage.position	= Vector3.zero;

		blueScoreImage.anchorMax = new Vector2 (temp, 1f);
		temp -= blue;
		blueScoreImage.anchorMin = new Vector2 (temp, 0f);
		blueScoreImage.position = Vector3.zero;

		greenScoreImage.anchorMax = new Vector2 (temp, 0f);
		temp -= green;
		greenScoreImage.anchorMin = new Vector2 (temp, 0f);
		greenScoreImage.position = Vector3.zero;
	}

	// Update is called once per frame
	void Update () {
	
	}
}
