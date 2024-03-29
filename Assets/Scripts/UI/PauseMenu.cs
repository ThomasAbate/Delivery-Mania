using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{
    public static PauseMenu Instance;

    public static bool gameIsPaused;

	public AudioMixer audioMixer;
    public AudioSource audioSource;
    [Space]
	public GameObject pauseMenu; //pause menu
	public GameObject optionsButtons; //buttons of the options menu
    [Space]
    public GameObject controlsScheme; //control scheme
    [Space]
    public Toggle fullscreenToggle;
	public Slider musicSlider;

    private void Awake()
    {
        if (Instance) Destroy(this);
        else Instance = this;
    }

    private void Start()
	{
		gameIsPaused = false;
	}


    public void PauseGame()
    {
        if (!GameOver.Instance.isGameOver && !WinSystem.Instance.isGameWin)
        {
            if (gameIsPaused)
            {
                Resume();
            }
            else
            {
                Paused();

				if(LookWithMouse.Instance.playerInput.currentControlScheme != "Gamepad")
				{
					Cursor.visible = true;
					Cursor.lockState = CursorLockMode.Confined;
				}
            }
        }
    }

    void Paused()
    {
        SaveSystem.instance.LoadOptions(); //load options when entering pause menu

        fullscreenToggle.isOn = SaveSystem.instance.isFulscreen;
        Screen.fullScreen = SaveSystem.instance.isFulscreen;

        musicSlider.value = SaveSystem.instance.music;


        pauseMenu.SetActive(true);
		optionsButtons.SetActive(true);

        EventSystem.current.SetSelectedGameObject(optionsButtons.transform.GetChild(0).gameObject);

        Time.timeScale = 0;

		gameIsPaused = true;
	}

	public void Resume()
	{
		pauseMenu.SetActive(false);
        controlsScheme.SetActive(false);

        SaveSystem.instance.SaveOptions(); //save options when closing the options menu
        
        Time.timeScale = 1;

		gameIsPaused = false;

		Cursor.visible = false;
		Cursor.lockState = CursorLockMode.Locked;
	}

	public void LoadMainMenu()
	{
		Resume();

		SaveSystem.instance.Save(SceneManager.GetActiveScene().buildIndex); //saving the current scene, not the next one
        SaveSystem.instance.SaveOptions(); //save options when closing the options menu

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
		//audioMixer.SetFloat("Master", music);

        audioSource.volume = musicSlider.value;
    }

    ///Controls Window
    public void Controls()
	{
		optionsButtons.SetActive(false);
		controlsScheme.SetActive(true);

        EventSystem.current.SetSelectedGameObject(controlsScheme.transform.GetChild(0).transform.GetChild(0).gameObject);

        if (LookWithMouse.Instance.playerInput.currentControlScheme != "Gamepad")
        {
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.Confined;
        }
	}

	public void ControlsBack()//from controls to options
	{
		controlsScheme.SetActive(false);
		optionsButtons.SetActive(true);
        EventSystem.current.SetSelectedGameObject(optionsButtons.transform.GetChild(0).gameObject);

        if (LookWithMouse.Instance.playerInput.currentControlScheme != "Gamepad")
        {
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.Confined;
        }
	}
}	
