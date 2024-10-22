using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;

public class GameManager : MonoBehaviour
{
	[Header("Menu")]
	[SerializeField] GameObject startMenu;
	[SerializeField] GameObject gameMenu;
	[SerializeField] GameObject leaderboardMenu;
	[SerializeField] GameObject pauseMenu;
	[SerializeField] GameObject deathMenu;
	[SerializeField] GameObject winMenu;
	[SerializeField] GameObject restartMenu;


	[Header("Game bools")]
	bool isPaused = false;
	bool isDead = false;
	bool isWin = false;
	bool isGameActive = false;
	bool isLeaderboardActive = false;
	public bool IsPaused { get { return isPaused; } }
	public bool IsDead { get { return isDead; } }
	public bool IsWin { get { return isWin; } }
	public bool IsGameActive { get { return isGameActive; } }

	[Header("gameMenu")]
	[SerializeField] Slider heatSlider;
	[SerializeField] TextMeshProUGUI timeText;

	[Header("winMenu")]
	[SerializeField] TextMeshProUGUI scoreText;
	string playerName;

	[SerializeField] float currentScore = 0;

	[SerializeField] DisplayHighscores displayHighscores;
	// Update is called once per frame
	void Update()
	{
		scoreText.text = $"SCORE: {PlayerPrefs.GetFloat("highscoreF")}";

		if (Input.GetKeyDown(KeyCode.P))
		{
			PauseGame();
		}
		if (Input.GetKeyDown(KeyCode.R))
		{
			RestartGame();
		}

		if (isGameActive && !isDead && !isWin)
		{
			currentScore += Time.deltaTime;
			timeText.text = currentScore.ToString("F2");
			heatSlider.value = HeatController.Instance.Heat;
			ControlHeat();
		}
	}

	public void StartGame()
	{
		isGameActive = true;
		startMenu.SetActive(false);
		gameMenu.SetActive(true);
		leaderboardMenu.SetActive(false);

	}
	void ControlHeat()
	{
		if (!isGameActive) { return; }
		if (HeatController.Instance.Heat < 100)
		{
			HeatController.Instance.Heat += Time.deltaTime;
		}
		if (HeatController.Instance.Heat < 0)
		{
			HeatController.Instance.Heat = 0;
		}
	}

	public void PauseGame()
	{
		if (!isPaused)
		{
			pauseMenu.SetActive(true);
			restartMenu.SetActive(true);
			isPaused = true;
			Time.timeScale = 0;
		}
		else
		{
			pauseMenu.SetActive(false);
			restartMenu.SetActive(false);
			isPaused = false;
			Time.timeScale = 1;
		}

	}
	public void RestartGame()
	{
		// reset the scene
		SceneManager.LoadScene(0);
		// reset score
		currentScore = 0;
		// reset heat
		HeatController.Instance.Heat = 0;
		// reset game
		isDead = false;
		isWin = false;
		isPaused = false;
		// reset time scale if paused
		if (Time.timeScale == 0)
		{
			Time.timeScale = 1;
		}
	}

	public void WinGame()
	{
		isWin = true;
		winMenu.SetActive(true);
		leaderboardMenu.SetActive(true);
		gameMenu.SetActive(false);
		restartMenu.SetActive(true);
		scoreText.text = "SCORE: " + ((int)currentScore).ToString();
		Debug.Log(currentScore);
	}

	public void ShowLeaderboard()
	{
		if (!isLeaderboardActive)
		{
			isLeaderboardActive = true;
			leaderboardMenu.SetActive(true);
		}
		else
		{
			isLeaderboardActive = false;
			leaderboardMenu.SetActive(false);
		}

	}
	public void InputName(string value)
	{
		playerName = value;
	}
	public void SendScore()
	{
		if (PlayerPrefs.GetString("highscoreUploaded") == "false")
		{
			PlayerPrefs.SetString("highscoreUploaded", "true");

			Debug.Log("Sending Score: " + currentScore);
			PlayerPrefs.SetFloat("highscoreF", currentScore);
			HighScores.UploadScore(playerName, (int)currentScore);
		}
		else if (currentScore < displayHighscores.myHighscoreList[0].score)
		{
			Debug.Log("Sending Score: " + currentScore);
			PlayerPrefs.SetFloat("highscoreF", currentScore);
			HighScores.UploadScore(playerName, (int)currentScore);
		}
	}
	public void ResetPlayerPrefs()
	{
		PlayerPrefs.SetFloat("highscoreF", 1200);
		PlayerPrefs.SetString("highscoreUploaded", "false");
	}
}
