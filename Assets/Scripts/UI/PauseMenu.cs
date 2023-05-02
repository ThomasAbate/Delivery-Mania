using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{
	public static bool gameIsPaused;

	public GameObject pauseMenu; //pause menu
	public GameObject optionsButtons; //buttons of the options menu

	public GameObject controlsScheme; //control scheme

	public Slider musicSlider;

	private void Start()
	{
		gameIsPaused = false;
	}

	void Update()
	{
		if (Input.GetKeyDown(KeyCode.P))
		{
			if (gameIsPaused)
			{
				Resume();
			}
			else
			{
				Paused();

				Cursor.visible = true;
				Cursor.lockState = CursorLockMode.Confined;
			}
		}
	}

	void Paused()
	{
		pauseMenu.SetActive(true);
		optionsButtons.SetActive(true);

		Time.timeScale = 0;

		gameIsPaused = true;

		Cursor.visible = true;
		Cursor.lockState = CursorLockMode.Confined;
	}

	public void Resume()
	{
		pauseMenu.SetActive(false);

		Time.timeScale = 1;

		gameIsPaused = false;

		Cursor.visible = false;
		Cursor.lockState = CursorLockMode.Locked;
	}

	public void LoadMainMenu()
	{
		Resume();

		SaveSystem.instance.Save(SceneManager.GetActiveScene().buildIndex); //saving the current scene, not the next one

		Cursor.visible = true;
		Cursor.lockState = CursorLockMode.Confined;

		SceneManager.LoadScene("Menu");
	}


	/// Options Window
	public void FullScreen()
	{
		Screen.fullScreen = true;
	}

	public void VolumeSlider()
	{
		GetComponent<AudioSource>().volume = musicSlider.value;
	}

	///Controls Window
	public void Controls()
	{
		optionsButtons.SetActive(false);
		controlsScheme.SetActive(true);

		Cursor.visible = true;
		Cursor.lockState = CursorLockMode.Confined;
	}

	public void ControlsBack()//from controls to options
	{
		controlsScheme.SetActive(false);
		optionsButtons.SetActive(true);

		Cursor.visible = true;
		Cursor.lockState = CursorLockMode.Confined;
	}
}