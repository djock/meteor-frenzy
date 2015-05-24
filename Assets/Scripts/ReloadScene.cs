using UnityEngine;
using System.Collections;

public class ReloadScene : MonoBehaviour {

	void OnClick()
	{
		Debug.Log("Application reloaded");
		Application.LoadLevel("game");
	}

}
