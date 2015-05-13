using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class Countdown : MonoBehaviour {

	public float timeRemaining;

	UILabel timeLabel;
	
	// Use this for initialization
	void Start () {
		timeLabel = GetComponent<UILabel> ();
		timeRemaining = 4;
	}
	
	// Update is called once per frame
	void Update () {
		CountDownTimer ();
	}

	void CountDownTimer() 
	{
		// 3,2,1 - GO
		if (timeRemaining > 1) {
			timeLabel.text = "" + (int)timeRemaining;
			//Debug.Log("Seconds: " + timeRemaining);
		} else {
			timeLabel.text = "GO";
		}
		
		timeRemaining -= Time.deltaTime;

		/*if(timeRemaining < 0)
		{
			Debug.Log ("Stop");

		}*/
	}
	
}
