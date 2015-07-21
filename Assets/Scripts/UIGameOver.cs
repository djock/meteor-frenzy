using UnityEngine;
using System.Collections;

public class UIGameOver : MonoBehaviour {

	public UIPanel countdownPanel;

	public void GoToMenu()
	{
		Debug.Log("Application reloaded");
		Application.LoadLevel("game");
	}

	public void RestartGame()
	{
		UIWindow.Show (countdownPanel);
	}

}
