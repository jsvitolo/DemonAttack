using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine;

public class GameController : MonoBehaviour
{
	public GameObject[] hazards;
	public Vector3 spawnValues;
	public int hazardCount;
	public float spawnWait;
	public float startWait;
	public float waveWait;

	public GUIText scoreText;
	public GUIText restartText;
	public GUIText gameOverText;
    public GUIText levelText;

	private bool gameOver;
	private bool restart;
	private int score;

    public float levelDelay = 2f;


	void Start ()
	{
		gameOver = false;
		restart  = false;
		restartText.text  = "";
		gameOverText.text = "";
        levelText.text = ""; 
		score = 0;
		UpdateScore ();
		StartCoroutine (SpawnWaves ());
	}

	void Update ()
	{
        UpdateLevel();
		if (restart)
		{
			if (Input.GetKeyDown (KeyCode.Space))
			{
				SceneManager.LoadScene (SceneManager.GetActiveScene ().buildIndex);
//				Application.LoadLevel (Application.loadedLevel); // depreciado
			}
		}
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
				restartText.text = "Press 'Space' for Restart";
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
		scoreText.text = "Score:  " + score;
	}

	public void GameOver ()
	{
		gameOverText.text = "Game Over";
		gameOver = true;
	}

    public void UpdateLevel ()
    {
        if (score == 40)
        {
            levelText.text = "Nivel 2";
            hazardCount = 4;
            waveWait = 4;
        }
        else if (score == 140)
        {
            levelText.text = "Nivel 3";
            hazardCount = 6;
            waveWait = 6;
        }
            
    }
}
