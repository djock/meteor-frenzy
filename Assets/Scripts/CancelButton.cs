using UnityEngine;
using System.Collections;

public class CancelButton : MonoBehaviour {

	public UIPanel panel;

	void OnClick()
	{
		NGUITools.SetActive (panel.gameObject, false);
	}
}
