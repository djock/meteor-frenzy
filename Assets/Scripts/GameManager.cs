using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {


	[Header ("Window Holder")]
	[SerializeField] UIPanel menuPanel;
	[SerializeField] UIPanel gameStart;

	[Header ("Modal Holder")]
	[SerializeField] UIPanel quitGame;
	[SerializeField] UIPanel pauseGame;
	[SerializeField] UIPanel gameOver;

	

	// Use this for initialization
	void Start () {
		NGUITools.SetActive (menuPanel.gameObject, true);
	}
	
	// Update is called once per frame
	void Update () {
	}
	
}
