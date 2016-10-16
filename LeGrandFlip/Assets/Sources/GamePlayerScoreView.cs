using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GamePlayerScoreView : MonoBehaviour 
{
	public Text playerNameText = null;
	public Text playerScoreText = null;
	//public Text playerRankText = null;
	public Image selectedPlayer = null;

	public void Fill (string playerName, int playerScore, uint playerRank, Color textColor, bool selectedScore)
	{
		if (playerScore > 0) {
			playerNameText.text = playerName;
			playerScoreText.text = playerScore.ToString ("000000");
			//playerRankText.text = playerRank.ToString ("000");			
		} else {
			playerNameText.text = playerName;
			playerScoreText.text = playerScore.ToString ("000000");
		}

		selectedPlayer.enabled = selectedScore;

		//playerNameText.color = textColor;
		//playerScoreText.color = textColor;
		//playerRankText.color = textColor;
	}
}
