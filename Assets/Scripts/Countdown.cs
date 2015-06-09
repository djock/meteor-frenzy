using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class Countdown : MonoBehaviour {

	public int countdownTime;
	int m_CountdownTime;
	public UILabel timeLabel;

		// Update is called once per frame
	 void OnEnable ()
	{
		m_CountdownTime = countdownTime;
		timeLabel.text = m_CountdownTime.ToString ();
		InvokeRepeating ("CountDownTime", 1f, 1f);

	}

	void CountDownTime()
	{
		m_CountdownTime -= 1;

		//Debug.Log (m_CountdownTime);
		// 3,2,1 - GO
		if (m_CountdownTime > 0) {
			timeLabel.text = "" + m_CountdownTime;
			//Debug.LogError ("1");
			//Debug.Log("Seconds: " + timeRemaining);
		} else if (m_CountdownTime == 0) {
			timeLabel.text = "GO";
			//Debug.LogError ("2");
		} else {
			//Debug.LogError ("3");
			//UIWindow.Close();
			//NGUITools.SetActive (countdownScreen.gameObject, false);
			GameManager.Instance.gs = GameManager.gameState.running;
			Debug.LogWarning ("Game state: " + GameManager.Instance.gs);
			UIWindow.Show (GameManager.Instance.uiHolder);
            //NGUITools.SetActive (uiHolder.gameObject, true);
            GameManager.Instance.GenerateMeteors ();
			CancelInvoke ("CountDownTime");
			GameManager.Instance.EnablePlayerAssets ();

			
		}
		
	}



	void OnDisable()
	{
		CancelInvoke ("CountDownTime");

	}
	
}
