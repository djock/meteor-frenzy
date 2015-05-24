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
	public UILabel scoreLabel;
	public UILabel gameOverScore;
	public UILabel highScoreLabel;



	[Header ("Public Variables")]
	public static int score;
	public static int highScore;

	public static GameManager Instance;

	public enum gameState
	{
		running,
		paused,
	};

	public  gameState gs;

	void Awake ()
	{
		Instance = this;
	}

	// Use this for initialization
	void Start ()
	{

		score = 0;
		highScore = PlayerPrefs.GetInt ("highscore");

		this.gs = gameState.paused;
		Debug.LogWarning ("Game state: " + gs);
	}
	
	// Update is called once per frame
	void Update ()
	{
		Quit ();
		Score ();
		HighScore ();
		highScoreLabel.text = "" + highScore;
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

	public void GenerateMeteors ()
	{
		CancelInvoke ("SpawnBigMeteor");
		CancelInvoke ("SpawnSmallMeteor");
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

	public void EnablePlayerAssets()
	{
		score = 0;
		NGUITools.SetActive (GameManager.Instance.trees,true);
		NGUITools.SetActive (GameManager.Instance.house,true);
		NGUITools.SetActive (GameManager.Instance.tractor,true);
	}
	
	void HighScore()
	{
		if (score > highScore) {
			highScore = score;
			Debug.LogError ("Score: " + highScore);

			PlayerPrefs.SetInt ("highscore", highScore);
			PlayerPrefs.Save ();

			}
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

		if (uiHolder.gameObject.activeSelf) {
			UIWindow.Hide (uiHolder);
		}
		UIWindow.Show (gameOver);

		CancelInvoke ("SpawnBigMeteor");
		CancelInvoke ("SpawnSmallMeteor");
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