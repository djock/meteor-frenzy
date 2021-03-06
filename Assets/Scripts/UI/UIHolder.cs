﻿using UnityEngine;
using System.Collections;

public class UIHolder : MonoBehaviour {

	[Header ("UI Components")]
	public UISprite score;
	public UIWidget buttonContainer;

	public TweenColor ScoreBackgroundColor { get; set; }
	public TweenScale ScoreBackgroundSize { get; set; }
	public UISprite scoreBackground;

	public void SetForMainMenu()
	{
		NGUITools.SetActive (buttonContainer.gameObject, false);
		NGUITools.SetActive (score.gameObject, false);

	}

	public void SetForPlayMode()
	{
		NGUITools.SetActive (buttonContainer.gameObject, false);
		NGUITools.SetActive (score.gameObject, true);

		ScoreBackgroundColor = scoreBackground.GetComponent<TweenColor>();
		ScoreBackgroundSize = scoreBackground.GetComponent<TweenScale>();
	}

	public void SetForGameOver()
	{
		NGUITools.SetActive (buttonContainer.gameObject, false);
		NGUITools.SetActive (score.gameObject, false);

	}

	public void HandleClick()
	{
		var activeButton = UIButton.current.name;

		switch (activeButton) 
		{
		case "Menu":
			Application.LoadLevel("game");
			break;
		case "Options Button":
			NGUITools.SetActive(GameManager.Instance.optionsWindow.gameObject, true);
			NGUITools.SetActive(GameManager.Instance.mainMenuWindow.gameObject, false);
			break;
		case "Back Button":
			NGUITools.SetActive(GameManager.Instance.optionsWindow.gameObject, false);
			NGUITools.SetActive(GameManager.Instance.mainMenuWindow.gameObject, true);
			break;
		case "Play Button":
			NGUITools.SetActive(GameManager.Instance.countDownWindow.gameObject, true);
			NGUITools.SetActive(GameManager.Instance.mainMenuWindow.gameObject, false);
			break;
		}
	}
}
