using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour {
	public GameObject[] hazards;
	public Vector3 spawnValues;
	public int hazardCount;
	public float spawnWait;
	public float startWait;
	public float waveWait;

	public Text scoreText;
	public Text restartText;
	public Text gameOverText;

	private bool gameOver;
	private bool restart;
	private int score;



	void Start ()
	{
		score = 0;
		UpdateScore ();
		StartCoroutine (SpawnWaves ());

		scoreText = GameObject.Find("ScoreText").GetComponent<Text>();
		restartText = GameObject.Find("RestartText").GetComponent<Text>();
		gameOverText = GameObject.Find("Game Over Text").GetComponent<Text>();

		restartText.text = "";
		gameOverText.text = "";

		gameOver = false;
		restart = false;
	}

	void Update (){
		if (restart){
			if (Input.GetKeyDown (KeyCode.R)){
				Application.LoadLevel (Application.loadedLevel);
			}
		}
	}
	IEnumerator SpawnWaves ()
	{
		yield return new WaitForSeconds (startWait);
		while (true)
		{
			for (int i = 0; i < hazardCount; i++) {
				GameObject hazard = hazards[Random.Range (0,hazards.Length)];
				Vector3 spawnPosition = new Vector3 (Random.Range (-spawnValues.x, spawnValues.x), 0, spawnValues.z);
				Quaternion spawnRotation = Quaternion.identity;
				Instantiate (hazard, spawnPosition, spawnRotation);
				yield return new WaitForSeconds (spawnWait);
			}
			yield return new WaitForSeconds (waveWait);
			if (gameOver){
				restartText.text = "Press 'R' for Restart";
				restart = true;
				break;
			}
		}
	}

	public void AddScore (int newScoreValue) {
		score += newScoreValue;
		UpdateScore ();
	}

	void UpdateScore() {
		scoreText.text = "Score: " + score;
	}

	public void GameOver (){
		gameOverText.text = "Game Over!";
		gameOver = true;

	}
}
