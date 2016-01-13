using UnityEngine;
using System.Collections;

public class UIHolder : MonoBehaviour {

	[Header ("UI Components")]
	public UIWidget pauseButton;
	public UISprite score;
	public UIWidget menuButton;
	public UIWidget rateButton;
	public UIWidget achievementsButton;
	public UIWidget leaderboardsButton;
	public UIWidget soundButton;
	public UIWidget adsButton;
	public UIWidget shareButton;

	public TweenColor scoreBackgroundColor;
	public TweenScale scoreBackgroundSize;

	public UISprite scoreBackground;

	void OnEnable()
	{
		TemporaryDeactivateButtons ();
	}

	public void SetForMainMenu()
	{
		NGUITools.SetActive (pauseButton.gameObject, false);
		NGUITools.SetActive (score.gameObject, false);
		
		NGUITools.SetActive (menuButton.gameObject, false);
		NGUITools.SetActive (rateButton.gameObject, true);
		NGUITools.SetActive (achievementsButton.gameObject, true);
		NGUITools.SetActive (leaderboardsButton.gameObject, true);
		NGUITools.SetActive (soundButton.gameObject, true);
		NGUITools.SetActive (adsButton.gameObject, true);
		NGUITools.SetActive (shareButton.gameObject, false);
	}

	public void SetForPlayMode()
	{
		NGUITools.SetActive (pauseButton.gameObject, true);
		NGUITools.SetActive (score.gameObject, true);

		NGUITools.SetActive (menuButton.gameObject, false);
		NGUITools.SetActive (rateButton.gameObject, false);
		NGUITools.SetActive (achievementsButton.gameObject, false);
		NGUITools.SetActive (leaderboardsButton.gameObject, false);
		NGUITools.SetActive (soundButton.gameObject, false);
		NGUITools.SetActive (adsButton.gameObject, false);
		NGUITools.SetActive (shareButton.gameObject, false);

		scoreBackgroundColor = scoreBackground.GetComponent<TweenColor>();
		scoreBackgroundSize = scoreBackground.GetComponent<TweenScale>();
	}

	public void SetForGameOver()
	{
		NGUITools.SetActive (pauseButton.gameObject, false);
		NGUITools.SetActive (score.gameObject, false);

		NGUITools.SetActive (menuButton.gameObject, true);
		NGUITools.SetActive (rateButton.gameObject, false);
		NGUITools.SetActive (achievementsButton.gameObject, true);
		NGUITools.SetActive (leaderboardsButton.gameObject, true);
		NGUITools.SetActive (soundButton.gameObject, true);
		NGUITools.SetActive (adsButton.gameObject, false);
		NGUITools.SetActive (shareButton.gameObject, true);
	}

	void TemporaryDeactivateButtons()
	{
		rateButton.GetComponent<UIButton> ().isEnabled = false;
		achievementsButton.GetComponent<UIButton> ().isEnabled = false;
		leaderboardsButton.GetComponent<UIButton> ().isEnabled = false;
		soundButton.GetComponent<UIButton> ().isEnabled = false;
		//shareButton.GetComponent<UIButton> ().isEnabled = false;
		adsButton.GetComponent<UIButton> ().isEnabled = false;
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
