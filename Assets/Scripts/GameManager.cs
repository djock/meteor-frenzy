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
    public UISprite scoreBackground;
    public GameObject newHighScore;


	[Header ("Public Variables")]
	public static int score;
	public static int highScore;
    public TweenColor scoreBackgroundColor;
    public TweenScale scoreBackgroundSize;

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
        StartCoroutine(MeteorDifficulty());
    }

    void Start ()
	{
        //StartCoroutine(MeteorDifficulty());

        score = 0;
		highScore = PlayerPrefs.GetInt ("highscore");

		this.gs = gameState.paused;
		Debug.LogWarning ("Game state: " + gs);

        scoreBackgroundColor = scoreBackground.GetComponent<TweenColor>();
        scoreBackgroundSize = scoreBackground.GetComponent<TweenScale>();

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

    void SpawnBigMeteor()
    {
        if (this.gs == gameState.running)
        {
            NGUITools.AddChild(meteorHolder, meteorBig);
            //Instantiate (meteorBig);
        }
    }

    void SpawnSmallMeteor()
    {
        if (this.gs == gameState.running)
        {
            var go = NGUITools.AddChild(meteorHolder, meteorSmall);
            var randY = Random.Range(-150, -200);
            var position = new Vector3(0, (float)randY, 0);

            go.transform.localPosition = position;
            //Instantiate (meteorSmall);
        }
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
                //Debug.LogWarning("Timescale: " + Time.timeScale);
            }
        }
    }
    
    public void AddPoints(int pointsToAdd)
    {
        score += pointsToAdd;
        Debug.Log ("Score: " + score);
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
			Debug.LogWarning ("High Score: " + highScore);

			PlayerPrefs.SetInt ("highscore", highScore);
			PlayerPrefs.Save ();

            scoreBackgroundColor.enabled = true;
            scoreBackgroundSize.enabled = true;

            NGUITools.SetActive(GameManager.Instance.newHighScore, true);
        }
        
    }

    public void EnablePlayerAssets()
    {
        score = 0;
        NGUITools.SetActive(GameManager.Instance.trees, true);
        NGUITools.SetActive(GameManager.Instance.house, true);
        NGUITools.SetActive(GameManager.Instance.vehicle, true);
        NGUITools.SetActive(GameManager.Instance.fence, true);

        scoreBackgroundSize.enabled = false;
        scoreBackgroundColor.enabled = false;
        NGUITools.SetActive(GameManager.Instance.newHighScore, false);
    }

    void GameOver()
    {
        this.gs = gameState.paused;
        Debug.LogWarning("Game state: " + gs);

        if (uiHolder.gameObject.activeSelf)
        {
            UIWindow.Hide(uiHolder);
        }

        UIWindow.Show(gameOver);

        CancelInvoke("SpawnBigMeteor");
        CancelInvoke("SpawnSmallMeteor");
    }

    public void IsPlayerAlive()
    {
        //Debug.LogError (house.activeSelf);
        //Debug.LogError (trees.activeSelf);
        //Debug.LogError (vehicle.activeSelf);
        if (!house.activeSelf && !trees.activeSelf && !vehicle.activeSelf && !fence.activeSelf)
        {
            GameOver();
            Debug.LogWarning("Player dead");
        }
    }

    void Quit()
    {

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            this.gs = gameState.paused;
            Debug.LogWarning("Game state: " + gs);
            if (!quitGame.gameObject.activeSelf)
            {
                UIWindow.Show(quitGame);
            }

        }
    }

}