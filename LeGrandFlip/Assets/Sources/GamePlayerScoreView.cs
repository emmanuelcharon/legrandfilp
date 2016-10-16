using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GamePlayerScoreView : MonoBehaviour 
{
	public Text playerNameText = null;
	public Text playerScoreText = null;
	public Text playerRankText = null;

	public void Fill (string playerName, int playerScore, uint playerRank, Color textColor)
	{
		if (playerScore > 0) {
			playerNameText.text = playerName;
			playerScoreText.text = playerScore.ToString ("00000000");
			playerRankText.text = playerRank.ToString ("000");			
		} else {
			playerNameText.text = "---";
			playerScoreText.text = "--------";
			playerRankText.text = "---";
		}

		playerNameText.color = textColor;
		playerScoreText.color = textColor;
		playerRankText.color = textColor;
	}
}
