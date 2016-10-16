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

		playerNameText.color = new Color (playerNameText.color.r, playerNameText.color.g, playerNameText.color.b, 1f);
		playerScoreText.color = new Color (playerScoreText.color.r, playerScoreText.color.g, playerScoreText.color.b, 1f);

		//playerNameText.color = textColor;
		//playerScoreText.color = textColor;
		//playerRankText.color = textColor;
	}

	public void Hide ()
	{
		playerNameText.color = new Color (playerNameText.color.r, playerNameText.color.g, playerNameText.color.b, 0f);
		playerScoreText.color = new Color (playerScoreText.color.r, playerScoreText.color.g, playerScoreText.color.b, 0f);
		selectedPlayer.enabled = false;
	}
}
