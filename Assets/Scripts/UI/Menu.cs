using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Menu : MonoBehaviour
{
    public static Menu menuInstance;

    public Toggle fullscreenToggle;

    public new AudioSource audio; //musica !!
	public Slider musicSlider;

	public Slider sensibilitySlider;

	public GameObject Buttons;
    public GameObject OptionsWindow;
    public GameObject ControlsWindow;

    public SaveSystem saveSystem;

	private void Start() //to make sure there is no problem on start
	{
        if (menuInstance) Destroy(this);
        else menuInstance = this;

        Buttons.SetActive(true);
        OptionsWindow.SetActive(false);
        ControlsWindow.SetActive(false);

        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.Confined;

        EventSystem.current.SetSelectedGameObject(Buttons.transform.GetChild(0).gameObject);
	}

	public void StartGame() //new game (tutorial lvl)
    {
        SceneManager.LoadScene("Intro");
    }

    public void Continue() //from the last save
    {
        saveSystem.Load();

        SceneManager.LoadScene(saveSystem._lvl);
    }
	public void ExitGame() //close App
	{
		Application.Quit();
	}

    /// Options Window
	public void Options() //from menu to options
    {
        saveSystem.LoadOptions(); //load options when entering options menu
        fullscreenToggle.isOn = SaveSystem.instance.isFulscreen;
        musicSlider.value = SaveSystem.instance.music;

        Buttons.SetActive(false); //to make sure you can't click them while in the options menu
		OptionsWindow.SetActive(true); //options menu
    }

	public void FullScreen()
	{
        Screen.fullScreen = true;
	}

    public void VolumeSlider()
    {
        audio.volume = musicSlider.value;
	}

    public void SensitivitySlider()
    {
        //you have to make a virtual mouse cursorto change the sensitivity
    }

	public void Back() //from options back to the main menu
    {
        saveSystem.SaveOptions(); //save options when closing the options menu

        OptionsWindow.SetActive(false);
        Buttons.SetActive(true);
    }

    ///Controls Window
    public void Controls()
    {
		OptionsWindow.SetActive(false);
		ControlsWindow.SetActive(true); //controls menu
	}

	public void ControlsBack()//from controls to options
    {
        ControlsWindow.SetActive(false);
		OptionsWindow.SetActive(true);
	}

    public void resetOptionsDefault()
    {
        fullscreenToggle.isOn = true;
        Screen.fullScreen = true;

        musicSlider.value = musicSlider.maxValue;
    }


}
