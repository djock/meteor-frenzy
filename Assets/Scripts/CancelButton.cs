using UnityEngine;
using System.Collections;

public class CancelButton : MonoBehaviour {

	public UIPanel panel;

	void OnClick()
	{
		UIWindow.Hide (panel);
		MeteorsMotion.meteorSpeed = -1;
	}
}
