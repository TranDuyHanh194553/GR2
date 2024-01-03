using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{

	public string levelToLoad = "MainLevel";

	public SceneFader sceneFader;
	AudioManager audioManager;
	private void Awake()
	{
		audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
	}

	public void Play()
	{
		audioManager.PlaySFX(audioManager.click);
		sceneFader.FadeTo(levelToLoad);
	}

	public void Quit()
	{
		audioManager.PlaySFX(audioManager.click);
		Debug.Log("Exciting...");
		Application.Quit();
	}

}
