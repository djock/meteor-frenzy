using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour
{
	[Header ("Player Assets")]
	public GameObject house;
	public GameObject trees;
	public GameObject vehicle;
    public GameObject fence;

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
    public ParticleSystem meteorDestroyParticle;



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
		InvokeRepeating ("SpawnBigMeteor", 0.8f, 1f);
		InvokeRepeating ("SpawnSmallMeteor", 0.5f, 1f);

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
        {
            score = 0;
        }
		
		scoreLabel.text = "" + score;
		gameOverScore.text = "" + score;

        if(score > 100) { 
            Time.timeScale = 1.1F;
        }
        if (score > 200)
        {
            Time.timeScale = 1.2F;
        }
        if (score > 300)
        {
            Time.timeScale = 1.3F;
        }


    }

	public void EnablePlayerAssets()
	{
		score = 0;
		NGUITools.SetActive (GameManager.Instance.trees,true);
		NGUITools.SetActive (GameManager.Instance.house,true);
		NGUITools.SetActive (GameManager.Instance.vehicle,true);
        NGUITools.SetActive(GameManager.Instance.fence, true);
    }
	
	void HighScore()
	{
		if (score > highScore) {
			highScore = score;
			///Debug.LogError ("High Score: " + highScore);

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
		//Debug.LogError (vehicle.activeSelf);
		if (!house.activeSelf && !trees.activeSelf && !vehicle.activeSelf && !fence.activeSelf) {
			GameOver ();
			Debug.LogWarning ("Player dead");
		}
	}

    public void MeteorExplosion(Vector3 position) {
        instantiate(meteorDestroyParticle, position);
    }

    private ParticleSystem instantiate(ParticleSystem prefab, Vector3 position)
    {
        ParticleSystem newParticleSystem = Instantiate(prefab, position, Quaternion.identity) as ParticleSystem;
        Destroy(newParticleSystem.gameObject,newParticleSystem.startLifetime);
        return newParticleSystem;
    }
}