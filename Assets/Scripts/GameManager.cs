using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour
{

	public GameObject house;
	public GameObject trees;
	public GameObject tractor;

	[Header ("Window Holder")]
	[SerializeField]
	public UIPanel
		menuPanel;
	[SerializeField]
	UIPanel
		gameStart;

	[Header ("Modal Holder")]
	[SerializeField]
	UIPanel
		quitGame;
	[SerializeField]
	UIPanel
		pauseGame;
	[SerializeField]
	UIPanel
		gameOver;

	[Header ("UI Components")]
	public UILabel
		timeLabel;
	public GameObject meteorBig;
	public GameObject meteorSmall;
	public UILabel scoreLabel;
	public UIPanel uiHolder;
	public GameObject meteorHolder;

	[Header ("Public Variables")]
	public float
		countdownTime;
	public static int score;

	public static GameManager Instance;

	enum gameState
	{
		running,
		paused,
	};

	gameState gs;

	void Awake ()
	{
		Instance = this;
	}

	// Use this for initialization
	void Start ()
	{
		score = 0;

		this.gs = gameState.paused;
		Debug.LogWarning ("Game state: " + gs);
	}
	
	// Update is called once per frame
	void Update ()
	{
		Quit ();
		Score ();
	}

	public void CountDownTimer ()
	{
		// 3,2,1 - GO
		if (countdownTime > 1) {
			timeLabel.text = "" + (int)countdownTime;
			//Debug.Log("Seconds: " + timeRemaining);
		} else if (countdownTime < 1 && countdownTime > 0) {
			timeLabel.text = "GO";
		} else if (countdownTime < 0 && countdownTime > -1) {
			NGUITools.SetActive (gameStart.gameObject, false);
			this.gs = gameState.running;
			Debug.LogError ("Game state: " + gs);
			NGUITools.SetActive (uiHolder.gameObject, true);
			GenerateMeteors ();
		}

		countdownTime -= Time.deltaTime;
	}

	void Quit ()
	{

		if (Input.GetKeyDown (KeyCode.Escape)) {
			this.gs = gameState.paused;
			Debug.LogError ("Game state: " + gs);
			if (!quitGame.gameObject.activeSelf) {
				NGUITools.SetActive (quitGame.gameObject, true);
				MeteorsMotion.meteorSpeed = 0;
			}

		}
	}

	void GenerateMeteors ()
	{
		MeteorsMotion.meteorSpeed = -1f;
		InvokeRepeating ("SpawnBigMeteor", 0.13f, 1.3f);
		InvokeRepeating ("SpawnSmallMeteor", 0.13f, 1.3f);

	}

	void SpawnBigMeteor ()
	{
		if (this.gs == gameState.running) {
			NGUITools.AddChild (meteorHolder, meteorBig);
			//Instantiate (meteorBig);
		}
	}

	void SpawnSmallMeteor ()
	{
		if (this.gs == gameState.running) {
			var go = NGUITools.AddChild (meteorHolder, meteorSmall);
			var randY = Random.Range (-150, -200);
			var position = new Vector3 (0, (float)randY, 0);

			go.transform.localPosition = position;
			//Instantiate (meteorSmall);
		}
	}

	void Score ()
	{
		if (score < 0)
			score = 0;
		
		scoreLabel.text = "" + score;
	}

	public void AddPoints (int pointsToAdd)
	{
		score += pointsToAdd;
		
		Debug.Log ("Score: " + score);
	}

	void GameOver ()
	{
		this.gs = gameState.paused;
		Debug.LogError ("Game state: " + gs);
		if (!gameOver.gameObject.activeSelf) {
			//NGUITools.SetActive (gameOver.gameObject, true);
			UIWindow.Show (gameOver);
			MeteorsMotion.meteorSpeed = 0;
		}
	}

	public void IsPlayerAlive ()
	{
		Debug.LogError (house.activeSelf);
		Debug.LogError (trees.activeSelf);
		Debug.LogError (tractor.activeSelf);
		if (!house.activeSelf && !trees.activeSelf && !tractor.activeSelf) {
			GameOver ();
			Debug.LogError ("Player dead");
		}
	}
}