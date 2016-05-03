using UnityEngine;
using System.Collections;

public class UIPauseGame : MonoBehaviour {

	void OnEnable()
	{
		GameManager.Instance.gs = GameManager.gameState.paused;
		GameManager.Instance.uiHolder.SetForGameOver ();

		Meteors.meteorSpeed = 0;
		GameManager.Instance.StopMeteors ();
	}

	public void ResumeGame()
	{
		GameManager.Instance.ResumeGame ();
	}

	public void RestartGame()
	{
		GameManager.Instance.RestartGame ();
	}
}
