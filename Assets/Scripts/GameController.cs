using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    
	public GameObject[] hazards;
	public Vector3 spawnValues;
	public int hazardCount;
	public float spawnWait;
	public float startWait;
	public float waveWait;

	public Text scoreText;
	public Text gameOverText;
    public GameObject restartButton;
    public GameObject menuButton;

    private Text levelText;
    private GameObject levelImage;

    private bool gameOver;
	private bool restart;
	private int score;
    private int level;

    public float levelDelay = 2f;


	void Start ()
	{
		gameOver = false;
		restart  = false;
        restartButton.SetActive(false);
        menuButton.SetActive(false);
		gameOverText.text = "";
		score = 0;
        level = 1;
		UpdateScore ();
		StartCoroutine (SpawnWaves ());

        levelImage = GameObject.Find("Level Image");
        levelText = GameObject.Find("Level Text").GetComponent<Text>();
        levelText.text = "NIVEL " + level;
        Invoke("HideLevelImage", levelDelay);
	}

    private void HideLevelImage()
    {
        levelImage.SetActive(false);
    }

	void Update ()
	{
        UpdateLevel();
	}

	IEnumerator SpawnWaves ()
	{
		yield return new WaitForSeconds (startWait);
		while (true)
		{
			for (int i = 0; i < hazardCount; i++)
			{
				GameObject hazard = hazards [Random.Range (0, hazards.Length)];
				Vector3 spawnPosition = new Vector3 (Random.Range (-spawnValues.x, spawnValues.x), spawnValues.y, spawnValues.z);
				Quaternion spawnRotation = Quaternion.identity;
				Instantiate (hazard, spawnPosition, spawnRotation);
				yield return new WaitForSeconds (spawnWait);
			}
			yield return new WaitForSeconds (waveWait);

			if (gameOver)
			{
                menuButton.SetActive(true);
                restartButton.SetActive (true);
				restart = true;
				break;
			}
		}
	}


	public void AddScore (int newScoreValue)
	{
		score += newScoreValue;
		UpdateScore ();
	}

	void UpdateScore ()
	{
        scoreText.text = "" + score;
	}

    public void UpdateLevel ()
    {
        switch (score)
        {
            case 100 :
                hazardCount = 4;
                level = 2;
                levelText.text = "NIVEL " + level;
				levelImage.SetActive(true);
				Invoke("HideLevelImage", levelDelay);
                break;

            case 200 :
                hazardCount = 5;
                level = 3;
                spawnWait = 0.4f;
				levelText.text = "NIVEL " + level;
				levelImage.SetActive(true);
				Invoke("HideLevelImage", levelDelay);
				break;

			case 300:
				hazardCount = 6;
				level = 4;
				spawnWait = 0.3f;
				levelText.text = "NIVEL " + level;
				levelImage.SetActive(true);
				Invoke("HideLevelImage", levelDelay);
				break;

			case 400:
				hazardCount = 8;
				level = 5;
				spawnWait = 0.2f;
				levelText.text = "NIVEL " + level;
				levelImage.SetActive(true);
				Invoke("HideLevelImage", levelDelay);
				break;
        }
    }

	public void GameOver ()
	{
		gameOverText.text = "GAME OVER";
		gameOver = true;
	}

    public void RestartGame ()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
