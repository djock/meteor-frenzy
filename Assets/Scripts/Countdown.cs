using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class Countdown : MonoBehaviour {

	// Update is called once per frame
	void Update () {
		GameManager.Instance.CountDownTimer ();
	}


	
}
