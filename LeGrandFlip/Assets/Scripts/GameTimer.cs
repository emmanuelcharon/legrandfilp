using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GameTimer : MonoBehaviour {

	public	int		gameDurationInSeconds	=	0;

	private int 	second					= 	0;

	private float	_startTime				= 	0f;
	private float   _endTime				=	0f;
	private bool	_gameStarted			=	false;

	public	Image 	fill					=	null;

	public Text 	timerText				= null;

	private static GameTimer _instance = null;


	public static GameTimer instance
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

	public void StartTimer ()
	{

		_startTime = Time.time;
		_endTime = Time.time + gameDurationInSeconds;
		_gameStarted = true;

		StartCoroutine ("RunTimer");
	}
		

	public void Reset ()
	{
		_gameStarted = false;
		_startTime = 0f;
		fill.fillAmount = 0f;
	}
	
	// Update is called once per frame
	void Update () 
	{
		if (_gameStarted && Time.time < _endTime) {
			fill.fillAmount = (Time.time - _startTime) / _endTime;
		} else if (_gameStarted == true) {
			_gameStarted = false;
			fill.fillAmount = 1f;
			GameLeaderboard.instance.GameOver ();
		}
	}

	IEnumerator RunTimer ()
	{
		timerText.text = (++second) + "s";

		yield return new WaitForSeconds (1f);

		if (second < gameDurationInSeconds)
			StartCoroutine ("RunTimer");
	}
}
