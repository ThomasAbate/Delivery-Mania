using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    public GameObject Buttons;
    public GameObject OptionsWindow;
    public GameObject ControlsWindow;

    public SaveSystem saveSystem;

	private void Start() //to make sure there is no problem on start
	{
		Buttons.SetActive(true);
        OptionsWindow.SetActive(false);
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

    public void Options() //from menu to options
    {
		Buttons.SetActive(false); //to make sure you can't click them while in the options menu
		OptionsWindow.SetActive(true); //options menu
    }
    public void Back() //from options back to the main menu
    {
        OptionsWindow.SetActive(false);
        Buttons.SetActive(true);
    }

    public void ExitGame() //close App
    {
        Application.Quit();
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
}
