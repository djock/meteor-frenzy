using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour
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

    public void GenerateMeteors()
    {
        CancelInvoke("SpawnBigMeteor");
        CancelInvoke("SpawnSmallMeteor");
		Meteors.meteorSpeed = -1f;
        InvokeRepeating("SpawnBigMeteor", 0.8f, 1f);
        InvokeRepeating("SpawnSmallMeteor", 0.5f, 1f);

    }

    public void SpawnBigMeteor()
    {
        if (this.gs == gameState.running)
        {
            NGUITools.AddChild(meteorHolder, meteorBig);
        }
    }

    public void SpawnSmallMeteor()
    {
        if (this.gs == gameState.running)
        {
            var go = NGUITools.AddChild(meteorHolder, meteorSmall);
            var randY = Random.Range(-150, -200);
            var position = new Vector3(0, (float)randY, 0);

            go.transform.localPosition = position;
        }
    }

	public void DestroyMeteors()
	{
		while (meteorHolder.gameObject.transform.childCount > 0)
			NGUITools.Destroy(meteorHolder.transform.GetChild(0).gameObject);
	}

    public void MeteorExplosion(Vector3 position)
    {
        instantiate(meteorDestroyParticle, position);
    }

    private ParticleSystem instantiate(ParticleSystem prefab, Vector3 position)
    {
        ParticleSystem newParticleSystem = Instantiate(prefab, position, Quaternion.identity) as ParticleSystem;
        Destroy(newParticleSystem.gameObject, newParticleSystem.startLifetime);
        return newParticleSystem;
    }

    IEnumerator MeteorDifficulty()
    {
        while (true)
        {
            yield return new WaitForSeconds(7f);

            if (this.gs == gameState.running)
            {
                Time.timeScale += 0.075f;
            }
        }
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

	public void StopMeteors()
	{
		CancelInvoke("SpawnBigMeteor");
		CancelInvoke("SpawnSmallMeteor");
		Debug.LogError ("Stoping meteors!");
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