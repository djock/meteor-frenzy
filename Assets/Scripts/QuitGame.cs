using UnityEngine;
using System.Collections;

public class QuitGame : MonoBehaviour {

	void OnClick()
	{
		Debug.Log("Application closed");
		Application.Quit ();
	}

}
