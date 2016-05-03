using UnityEngine;
using System.Collections;

public class UIMainMenu : MonoBehaviour {

	void Start()
	{
		GameManager.Instance.gs = GameManager.gameState.paused;
		GameManager.Instance.uiHolder.SetForMainMenu ();
	}
}
