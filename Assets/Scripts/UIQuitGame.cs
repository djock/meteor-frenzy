using UnityEngine;
using System.Collections;

public class UIQuitGame : MonoBehaviour {

	void OnEnable()
	{
		Meteors.meteorSpeed = 0;
	}

	void OnDisable()
	{
		if (GameManager.Instance.uiHolder.gameObject.activeSelf) {
			Meteors.meteorSpeed = -1f;
			GameManager.Instance.gs = GameManager.gameState.running;
			GameManager.Instance.GenerateMeteors ();
		}
	}

	public void CancelQuitGame()
	{
		UIWindow.Hide (GameManager.Instance.quitGame);
	}

	public void QuitGame()
	{

		PlayerPrefs.DeleteAll();
		Debug.Log("Deleted all player prefs.");

//		Debug.Log("Application closed");
//		Application.Quit ();
	}
}
