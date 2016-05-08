using UnityEngine;
using System.Collections;

public class UIHolder : MonoBehaviour {

	[Header ("UI Components")]
	// public UISprite pauseButton;
	public UISprite score;

	public TweenColor scoreBackgroundColor;
	public TweenScale scoreBackgroundSize;

	public UISprite scoreBackground;

	public void SetForMainMenu()
	{
		// NGUITools.SetActive (pauseButton.gameObject, false);
		NGUITools.SetActive (score.gameObject, false);

	}

	public void SetForPlayMode()
	{
		// NGUITools.SetActive (pauseButton.gameObject, true);
		NGUITools.SetActive (score.gameObject, true);

		scoreBackgroundColor = scoreBackground.GetComponent<TweenColor>();
		scoreBackgroundSize = scoreBackground.GetComponent<TweenScale>();
	}

	public void SetForGameOver()
	{
		// NGUITools.SetActive (pauseButton.gameObject, false);
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
		}
	}
}
