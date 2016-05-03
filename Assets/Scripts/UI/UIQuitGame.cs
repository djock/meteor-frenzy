using UnityEngine;
using System.Collections;

public class UIQuitGame : MonoBehaviour {

	void OnEnable()
	{
		Meteors.meteorSpeed = 0;
	}

	void OnDisable()
	{
		if (!GameManager.Instance.mainMenuWindow.gameObject.activeSelf &&
		    !GameManager.Instance.gameOverWindow.gameObject.activeSelf &&
		    !GameManager.Instance.countDownWindow.gameObject.activeSelf) {
			GameManager.Instance.ResumeGame ();

		} else {
			NGUITools.SetActive (GameManager.Instance.uiHolderWindow.gameObject, false);
		}
	}

	public void CancelQuitGame()
	{
		UIWindow.Hide (GameManager.Instance.quitGame);
	}

	public void QuitGame()
	{
		Application.Quit();
	}
}
