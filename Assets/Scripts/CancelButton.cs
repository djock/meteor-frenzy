using UnityEngine;
using System.Collections;

public class CancelButton : MonoBehaviour {

	public UIPanel panel;

	void OnClick()
	{
		UIWindow.Hide (panel);
        GameManager.Instance.GenerateMeteors();
	}
}
