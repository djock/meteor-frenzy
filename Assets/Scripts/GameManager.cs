using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour
{
	[Header ("Player Assets")]
	public GameObject house;
	public GameObject trees;
	public GameObject tractor;

	[Header ("Window Holder")]
	public UIPanel menuPanel;
	public UIPanel countdownScreen;
	public UIPanel uiHolder;

	[Header ("Modal Holder")]
	public UIPanel quitGame;
	public UIPanel gameOver;

	[Header ("Meteors")]
	public GameObject meteorBig;
	public GameObject meteorSmall;

	[Header ("UI Components")]
	public GameObject meteorHolder;
	public UILabel timeLabel;
	public UILabel scoreLabel;
	public UILabel gameOverScore;



	[Header ("Public Variables")]
	public float countdownTime;
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
			//UIWindow.Hide(countdownScreen);
			NGUITools.SetActive (countdownScreen.gameObject, false);
			this.gs = gameState.running;
			Debug.LogWarning ("Game state: " + gs);
			UIWindow.Show(uiHolder);
			//NGUITools.SetActive (uiHolder.gameObject, true);
			GenerateMeteors ();
		}

		countdownTime -= Time.deltaTime;
	}

	void Quit ()
	{

		if (Input.GetKeyDown (KeyCode.Escape)) {
			this.gs = gameState.paused;
			Debug.LogWarning ("Game state: " + gs);
			if (!quitGame.gameObject.activeSelf) {
				UIWindow.Show(quitGame);
				//NGUITools.SetActive (quitGame.gameObject, true);
				MeteorsMotion.meteorSpeed = 0;
			}

		}
	}

	void GenerateMeteors ()
	{
		MeteorsMotion.meteorSpeed = -1f;
		InvokeRepeating ("SpawnBigMeteor", 1, 1f);
		InvokeRepeating ("SpawnSmallMeteor", 1, 1f);

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
		gameOverScore.text = "" + score;
	}

	public void AddPoints (int pointsToAdd)
	{
		score += pointsToAdd;
		
		Debug.Log ("Score: " + score);
	}

	void GameOver ()
	{
		this.gs = gameState.paused;
		Debug.LogWarning ("Game state: " + gs);
		if (!gameOver.gameObject.activeSelf) {
			//NGUITools.SetActive (gameOver.gameObject, true);
			UIWindow.Show (gameOver);

			if(uiHolder.gameObject.activeSelf)
				UIWindow.Hide (uiHolder);
			UIWindow.Show (gameOver);
			MeteorsMotion.meteorSpeed = 0;
		}
	}

	public void IsPlayerAlive ()
	{
		//Debug.LogError (house.activeSelf);
		//Debug.LogError (trees.activeSelf);
		//Debug.LogError (tractor.activeSelf);
		if (!house.activeSelf && !trees.activeSelf && !tractor.activeSelf) {
			GameOver ();
			Debug.LogWarning ("Player dead");
		}
	}
}