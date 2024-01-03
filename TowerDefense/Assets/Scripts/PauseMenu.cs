using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{

	public GameObject ui;

	public string menuSceneName = "MainMenu";

	public SceneFader sceneFader;

	public GameObject summonAlly;
	public GameObject summonDrone;
	// public GameObject gameMaster;
	AudioManager audioManager;
	private void Awake()
	{
		audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
	}

	void Update()
	{
		if (Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.P))
		{
			Toggle();
		}
	}

	public void Toggle()
	{
		ui.SetActive(!ui.activeSelf);

		if (ui.activeSelf)
		{
			Time.timeScale = 0f;
			summonAlly.SetActive(false);
			summonDrone.SetActive(false);
		}
		else
		{
			Time.timeScale = 1f;
			summonAlly.SetActive(true);
			summonDrone.SetActive(true);
		}
	}

	public void Retry()
	{
		audioManager.PlaySFX(audioManager.click);
		gameObject.SetActive(false);

		GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");

		foreach (GameObject enemy in enemies)
		{
			Destroy(enemy);
		}
		WaveSpawner.EnemiesAlive = 0;

		Toggle();
		sceneFader.FadeTo(SceneManager.GetActiveScene().name);

	}

	public void Menu()
	{
		audioManager.PlaySFX(audioManager.click);
		gameObject.SetActive(false);

		GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");

		foreach (GameObject enemy in enemies)
		{
			Destroy(enemy);
		}
		WaveSpawner.EnemiesAlive = 0;
		Toggle();
		sceneFader.FadeTo(menuSceneName);
	}

}
