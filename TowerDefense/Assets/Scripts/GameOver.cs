using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
{
	public string menuSceneName = "MainMenu";

	public SceneFader sceneFader;
	AudioManager audioManager;
	private void Awake()
	{
		audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
	}

	public void Retry()
	{
		audioManager.PlaySFX(audioManager.click);
		sceneFader.FadeTo(SceneManager.GetActiveScene().name);
	}

	public void Menu()
	{
		audioManager.PlaySFX(audioManager.click);
		sceneFader.FadeTo(menuSceneName);
	}

}
