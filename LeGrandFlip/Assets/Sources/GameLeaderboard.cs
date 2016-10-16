using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;
using System.Collections.Generic;

public class GameLeaderboard : MonoBehaviour {

	public InputField inputField = null;

	public GameObject enterNameScreen = null;
	public GameObject leaderboardScreen = null;

	public Transform redScoreRoot	= null;
	public Transform blueScoreRoot = null;
	public Transform greenScoreRoot = null;

	private static GameLeaderboard _instance = null;

	public static GameLeaderboard instance
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

	// Use this for initialization
	void Awake () 
	{
		instance = this;

		leaderboardScreen.SetActive (false);
		enterNameScreen.SetActive (false);
	}

	bool endGame = false;

	public void GameOver ()
	{
		endGame = true;
		leaderboardScreen.SetActive (false);
		enterNameScreen.SetActive (true);

		inputField.onValueChanged.RemoveAllListeners ();
		inputField.onValueChanged.AddListener (UpdatePlayerName);

	}


	// Update is called once per frame
	void Update () 
	{
		if (endGame == false)
			return;
		
		if (Input.GetKeyDown (KeyCode.KeypadEnter) || Input.GetKeyDown (KeyCode.Return)) 
		{
			enterNameScreen.SetActive (false);

			GameSave.AddScore (inputField.text.ToUpper(), 
				GameScore.instance.globalscore, 
				GameScore.instance.redScore, 
				GameScore.instance.blueScore,
				GameScore.instance.greenScore);

			leaderboardScreen.SetActive (true);
			ShowLeaderboards ();
		}
	}

	public void UpdatePlayerName (string value)
	{
		string temp = value.ToUpper ();

		if (StringComparer.Ordinal.Compare (temp, inputField.text) != 1) 
		{
			inputField.text = temp;
		}
	}

	public void ShowLeaderboards ()
	{
		ShowLeadarbord (GameSave.instance.redScore, redScoreRoot.GetComponentsInChildren<GamePlayerScoreView>());
		ShowLeadarbord (GameSave.instance.blueScore, blueScoreRoot.GetComponentsInChildren<GamePlayerScoreView>());
		ShowLeadarbord (GameSave.instance.greenScore, greenScoreRoot.GetComponentsInChildren<GamePlayerScoreView>());
	}

	public void ShowLeadarbord (List<PlayerScore> scores, GamePlayerScoreView[] entries)
	{
		int index = scores.FindIndex (s => s.id == GameSave.instance.uniqueScoreId);

		int startIndex = Mathf.Max (index - 3, 0);
		int lastIndex = startIndex + 7;

		int entrieIndex = 0;

		for (int i = startIndex; i < lastIndex; i++) 
		{
			if (i < scores.Count) {
				entries [entrieIndex].Fill (
					scores [i].playername, 
					scores [i].score, 
					scores [i].rank,
					((i == index) ? Color.yellow : Color.black));
			} else
				entries [entrieIndex].Fill ("---", 0, 0, default(Color));

			entrieIndex++;
		}
	}
}
