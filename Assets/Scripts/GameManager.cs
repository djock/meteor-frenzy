using UnityEngine;
using System.Collections;

public partial class GameManager : MonoBehaviour
{
	[Header ("Player Assets")]
	public UIWidget playerAssets;
	public GameObject house;
	public GameObject trees;
	public GameObject vehicle;

    [Header ("Window Holder")]
	public UIPanel mainMenuWindow;
	public UIPanel uiHolderWindow;
	public UIPanel countDownWindow;
	public UIPanel gameOverWindow;

	[Header ("Modal Holder")]
	public UIPanel quitGame;

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


	[Header ("Scripts")]
	public UIHolder uiHolder;

    public static GameManager Instance;

	[HideInInspector]
	public enum gameState
	{
		running,
		paused,
	};

	public  gameState gs;

	void Awake ()
	{
		Instance = this;
        StartCoroutine(MeteorDifficulty());
    }

    void Start ()
	{
        score = 0;
		highScore = PlayerPrefs.GetInt ("highscore");
		this.gs = gameState.paused;
    }
	
	void Update ()
	{
		Quit ();
		Score ();
		HighScore ();
		highScoreLabel.text = "" + highScore;
    }
    
    public void AddPoints(int pointsToAdd)
    {
        score += pointsToAdd;
    }

    void Score ()
	{
        if (score < 0)
        {
            score = 0;
        }
		
		scoreLabel.text = "" + score;
		gameOverScore.text = "" + score;
    }

	void HighScore()
	{
		if (score > highScore) {

			highScore = score;

			PlayerPrefs.SetInt ("highscore", highScore);
			PlayerPrefs.Save ();

			uiHolder.scoreBackgroundColor.enabled = true;
			uiHolder.scoreBackgroundSize.enabled = true;
        }
    }

    public void EnablePlayerAssets()
    {
        score = 0;
        NGUITools.SetActive(GameManager.Instance.trees, true);
        NGUITools.SetActive(GameManager.Instance.house, true);
        NGUITools.SetActive(GameManager.Instance.vehicle, true);
    }

    void GameOver()
    {
        this.gs = gameState.paused;

		uiHolder.SetForGameOver();

        UIWindow.Show(gameOverWindow);
		DestroyMeteors ();
		StopMeteors ();
    }

    public void IsPlayerAlive()
    {
        if (!house.activeSelf && !trees.activeSelf && !vehicle.activeSelf)
        {
            GameOver();
        }
    }

    void Quit()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            this.gs = gameState.paused;
            if (!quitGame.gameObject.activeSelf)
            {
				UIWindow.Show(quitGame);
            }
        }
    }

	public void ResumeGame()
	{
		uiHolder.SetForPlayMode ();
		Meteors.meteorSpeed = -1f;
		GameManager.Instance.gs = GameManager.gameState.running;
		GameManager.Instance.GenerateMeteors ();
	}

	public void RestartGame()
	{
		DestroyMeteors ();
		Meteors.meteorSpeed = 0;
		StopMeteors ();
	}
}