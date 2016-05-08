using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class UICountdownWindow : MonoBehaviour {

	public UILabel timeLabel;
	public UIPanel countDownPanel;

	public int countdownTime;
	int m_CountdownTime;

	 void OnEnable ()
	{
		GameManager.Instance.gs = GameManager.gameState.paused;

		m_CountdownTime = countdownTime;
		timeLabel.text = m_CountdownTime.ToString ();
		InvokeRepeating ("CountDownTime", 1f, 1f);

		NGUITools.SetActive (GameManager.Instance.playerAssets.gameObject, true);
		NGUITools.SetActive (GameManager.Instance.uiHolderWindow.gameObject, false);
	}

	void CountDownTime()
	{
		m_CountdownTime -= 1;

		if (m_CountdownTime > 0) {
			timeLabel.text = "" + m_CountdownTime;
			// gameObject.GetComponent<UIPlaySound> ().Play();
		} else if (m_CountdownTime == 0) {
			timeLabel.text = "GO";
		} else {
			StartGame ();
        }
	}

	public void StartGame()
	{
		GameManager.Instance.gs = GameManager.gameState.running;

		NGUITools.SetActive (countDownPanel.gameObject, false);
		NGUITools.SetActive (GameManager.Instance.uiHolderWindow.gameObject, true);
		GameManager.Instance.uiHolder.SetForPlayMode ();

		GameManager.Instance.GenerateMeteors ();
		CancelInvoke ("CountDownTime");
		GameManager.Instance.EnablePlayerAssets ();

		Time.timeScale = 1f;
	}
    
	void OnDisable()
	{
		CancelInvoke ("CountDownTime");
	}
    
}
