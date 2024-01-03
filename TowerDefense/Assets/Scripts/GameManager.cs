using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour
{

	public static bool GameIsOver;

	public GameObject gameOverUI;
	public GameObject completeLevelUI;
	public GameObject summonAlly;
	public GameObject summonDrone;
	// public WaveSpawner waveSpawner;
	AudioManager audioManager;
	private void Awake()
	{
		audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
	}

	void Start()
	{
		GameIsOver = false;
	}

	// Update is called once per frame
	void Update()
	{
		if (GameIsOver)
			return;

		if (PlayerStats.Lives <= 0)
		{
			EndGame();
		}
	}

	void EndGame()
	{
		audioManager.PlaySFX(audioManager.fail);
		GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");

		foreach (GameObject enemy in enemies)
		{
			Destroy(enemy);
		}
		gameObject.SetActive(false);
		WaveSpawner.EnemiesAlive = 0;

		GameIsOver = true;
		summonAlly.SetActive(false);
		summonDrone.SetActive(false);
		gameOverUI.SetActive(true);
	}

	public void WinLevel()
	{
		audioManager.PlaySFX(audioManager.clear);
		GameIsOver = true;
		completeLevelUI.SetActive(true);
		summonAlly.SetActive(false);
		summonDrone.SetActive(false);
	}

}
