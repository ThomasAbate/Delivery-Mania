using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    public GameObject Buttons;
    public GameObject SettingsWindow;

    public JsonUtilSave saveSystem;

	private void Start() //to make sure there is no problem on start
	{
		Buttons.SetActive(true);
        SettingsWindow.SetActive(false);
	}

	public void StartGame() //new game (tutorial lvl)
    {
        SceneManager.LoadScene("Loris Test");
    }

    public void Continue() //from the last save
    {
        saveSystem.Loadju();

        SceneManager.LoadScene(saveSystem._lvl);
    }

    public void Options() //from menu to options
    {
        SettingsWindow.SetActive(true); //options menu
        Buttons.SetActive(false); //to make sure you can't click them while in the options menu
    }
    public void Back() //from options back to the main menu
    {
        SettingsWindow.SetActive(false);
        Buttons.SetActive(true);
    }

    public void ExitGame() //close App
    {
        Application.Quit();
    }
}
