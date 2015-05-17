using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {


	[Header ("Window Holder")]
	[SerializeField] public UIPanel menuPanel;
	[SerializeField] UIPanel gameStart;

	[Header ("Modal Holder")]
	[SerializeField] UIPanel quitGame;
	[SerializeField] UIPanel pauseGame;
	[SerializeField] UIPanel gameOver;

	[Header ("UI Components")]
	public UILabel timeLabel;
	public GameObject meteorBig;
	public GameObject meteorSmall;
	public UILabel scoreLabel;

	[Header ("Public Variables")]
	public float countdownTime;
	public static int score;

	public static GameManager Instance;

	enum gameState {
		running,
		paused,
	};

	gameState gs;

	void Awake()
	{
		Instance = this;
	}

	// Use this for initialization
	void Start () 
	{
		score = 0;
		this.gs = gameState.paused;
		Debug.LogError("Game state: " + gs);
	}
	
	// Update is called once per frame
	void Update () 
	{
		Quit ();
		Score ();

	}

	public void CountDownTimer() 
	{
		// 3,2,1 - GO
		if (countdownTime > 1) {
			timeLabel.text = "" + (int)countdownTime;
			//Debug.Log("Seconds: " + timeRemaining);
		} else if (countdownTime < 1 && countdownTime > 0) {
			timeLabel.text = "GO";
		} else if (countdownTime < 0 && countdownTime > -1) {
			Debug.Log ("Stop");
			NGUITools.SetActive (gameStart.gameObject, false);
			this.gs = gameState.running;
			Debug.LogError("Game state: " + gs);
			GenerateMeteors ();

		}
	
		countdownTime -= Time.deltaTime;
	}

	void Quit()
	{

		if (Input.GetKeyDown (KeyCode.Escape)) 
		{
			this.gs = gameState.paused;
			Debug.Log("Close");
			if(!quitGame.gameObject.activeSelf)
			{
				NGUITools.SetActive (quitGame.gameObject, true);
			}

		}
	}

	void GenerateMeteors()
	{
		if (this.gs == gameState.running) {
			InvokeRepeating("SpawnBigMeteor", 0.1f, 1f);
			InvokeRepeating("SpawnSmallMeteor", 0.12f, 1.2f);
		}
	}

	void SpawnBigMeteor()
	{
		Instantiate (meteorBig);
	}
	void SpawnSmallMeteor()
	{
		Instantiate (meteorSmall);
	}

	void Score()
	{
		if (score < 0)
			score = 0;
		
		scoreLabel.text = "" + score;
	}

	public static void AddPoints(int pointsToAdd)
	{
		score += pointsToAdd;
		
		Debug.Log ("Score: " + score);
	}
}
