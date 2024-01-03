using UnityEngine;
using UnityEngine.UI;

public class LevelSelector : MonoBehaviour
{

	public SceneFader fader;

	public Button[] levelButtons;
	AudioManager audioManager;
	private void Awake()
	{
		audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
	}

	void Start()
	{
		int levelReached = PlayerPrefs.GetInt("levelReached", 1);

		for (int i = 0; i < levelButtons.Length; i++)
		{
			if (i + 1 > levelReached)
				levelButtons[i].interactable = false;
		}
	}

	public void Select(string levelName)
	{
		audioManager.PlaySFX(audioManager.click);
		fader.FadeTo(levelName);
	}

}
