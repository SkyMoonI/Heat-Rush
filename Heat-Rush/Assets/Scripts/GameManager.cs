using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
	[SerializeField] TextMeshProUGUI scoreText;
	[SerializeField] TextMeshProUGUI nameText;

	[SerializeField] int currentScore = 0;

	// Update is called once per frame
	void Update()
	{
		scoreText.text = $"SCORE: {PlayerPrefs.GetInt("highscore")}";
	}

	public void SendScore()
	{
		if (currentScore > PlayerPrefs.GetInt("highscore"))
		{
			PlayerPrefs.SetInt("highscore", currentScore);
			HighScores.UploadScore(nameText.text, currentScore);
		}

	}
}
