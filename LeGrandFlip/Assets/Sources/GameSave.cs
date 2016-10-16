using System.Collections.Generic;
using System;
using UnityEngine.Serialization;
using UnityEngine;

[Serializable]
public class PlayerScore : IComparable<PlayerScore>
{
	public int score = 0;
	public string playername = string.Empty;
	public uint id	= 0;
	public uint rank = 0;

	public int CompareTo(PlayerScore obj)
	{
		return obj.score.CompareTo (this.score);
	}

	public PlayerScore(string name, int score, uint id)
	{
		this.score = score;
		this.playername = name;
		this.id = id;
	}

	public override string ToString ()
	{
		return string.Format ("{0} - {1} - {2}", rank, playername, score.ToString("00000000"));
	}

	public static string Empty ()
	{
		return string.Format ("{0} - {1} - {2}", "---", "---", "00000000");
	}
}

[Serializable]
public class GameSave
{
	public List<PlayerScore> redScore;
	public List<PlayerScore> greenScore;
	public List<PlayerScore> blueScore;
	public List<PlayerScore> allStarScore;

	public uint uniqueScoreId	=	0;

	private static GameSave _instance = null;

	public static GameSave instance
	{
		get { return _instance; }
		set 
		{
			if (_instance == null)
				_instance = ReadScore ();
		}
	}

	public static GameSave ReadScore ()
	{
		string jsonContent = string.Empty;

		jsonContent = PlayerPrefs.GetString ("leaderboard", string.Empty);

		GameSave gameSave = null;

		if (string.IsNullOrEmpty (jsonContent) == false) {
			gameSave = JsonUtility.FromJson<GameSave> (jsonContent);
		} else {
			gameSave = new GameSave ();
			gameSave.allStarScore = new List<PlayerScore> ();
			gameSave.redScore = new List<PlayerScore> ();
			gameSave.greenScore = new List<PlayerScore> ();
			gameSave.blueScore = new List<PlayerScore> ();
		}

		return gameSave;
	}

	public static void AddScore (string name, int globalScore, int redScore, int blueScore, int greenScore)
	{	
		if (_instance == null)
			_instance = ReadScore ();

		uint scoreId = ++_instance.uniqueScoreId; 

		_instance.allStarScore.Add (new PlayerScore (name, globalScore, scoreId));
		_instance.redScore.Add (new PlayerScore (name, redScore, scoreId));
		_instance.blueScore.Add (new PlayerScore (name, blueScore, scoreId));
		_instance.greenScore.Add (new PlayerScore (name, greenScore, scoreId));

		SortList(_instance.allStarScore);
		SortList (_instance.redScore);
		SortList (_instance.blueScore);
		SortList(_instance.greenScore);

		PlayerPrefs.SetString ("leaderboard", JsonUtility.ToJson (_instance));
	}

	public static void SortList (List<PlayerScore> scores)
	{
		if (scores.Count == 0)
			return;
		
		uint rank = 1;

		scores.RemoveAll (s => s.score <= 0);

		scores.Sort ();

		scores [0].rank = rank;

		for (int i = 1; i < scores.Count; i++) 
		{
			if (scores [i].score < scores [i - 1].score) 
			{
				rank++;
			}

			scores [i].rank = rank;
		}
	}
}