using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    public GameObject Buttons;
    public GameObject SettingsWindow;

	private void Start() //to make sure there is no problem on start
	{
		Buttons.SetActive(true);
        SettingsWindow.SetActive(false);
	}

	public void StartGame()
    {
        SceneManager.LoadScene("Tuto");
    }

    public void Continue()
    {

    }

    public void Options()
    {
        SettingsWindow.SetActive(true);
        Buttons.SetActive(false);
    }
    public void Back()
    {
        SettingsWindow.SetActive(false);
        Buttons.SetActive(true);

    }

    public void ExitGaem()
    {

    }
}
