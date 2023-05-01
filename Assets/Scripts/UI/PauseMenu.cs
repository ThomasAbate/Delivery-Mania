using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{
	public static bool gameIsPaused = false;

	public GameObject pauseMenuUI;

	public Slider musicSlider;

	public Slider sensibilitySlider;

	void Update()
	{
		if (Input.GetKeyDown(KeyCode.Escape))
		{
			if (gameIsPaused)
			{
				Resume();
			}
			else
			{
				Paused();
			}
		}
	}

	void Paused()
	{
		pauseMenuUI.SetActive(true);

		Time.timeScale = 0;

		gameIsPaused = true;

		Cursor.visible = true;
	}

	public void Resume()
	{
		pauseMenuUI.SetActive(false);

		Time.timeScale = 1;

		gameIsPaused = false;

		Cursor.visible = false;
	}

	public void LoadMainMenu()
	{
		Resume();

		SceneManager.LoadScene("Menu");
	}

	/// Options Window
	public void FullScreen()
	{
		Screen.fullScreen = true;
	}

	public void VolumeSlider(float Volume)
	{
		Volume = musicSlider.value;

		GetComponent<AudioSource>().volume = Volume;
	}

	public void SensibilitySlider()
	{

	}

	///Controls Window
	public void Controls()
	{
		pauseMenuUI.SetActive(false);
	}

	public void ControlsBack()//from controls to options
	{
		pauseMenuUI.SetActive(true);
	}
}