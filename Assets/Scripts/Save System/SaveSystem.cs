using Newtonsoft.Json.Linq;
using UnityEngine;
using System.IO;
using UnityEngine.UI;
using System.Collections.Generic;

public class SaveSystem : MonoBehaviour
{
	public static SaveSystem instance;

    /*
    public GameObject loadingScreen;
    public Image progressBar;
    public TextMeshProUGUI progressText;
	*/

    #region Options to save
    public float music;
    public bool isFulscreen;

	public Toggle fullScreenToggle;
	public Slider musicSlider;
    #endregion

    public int _lvl;

	private void Awake()
	{
		if (instance) Destroy(this);
		else instance = this;
	}

	public void Save(int scene)
	{
		_lvl = scene;

        string saveFilePath = Application.persistentDataPath + "/saveMania.fsav";
		//print("Saving to : " + saveFilePath);

		JObject jObject = new JObject
		{
			{ "Component Name", GetType().ToString() }
		};

		JObject jDataObject = new JObject();
		jObject.Add("Data", jDataObject);

		jDataObject.Add("Level", _lvl);

		StreamWriter sw = new StreamWriter(saveFilePath);
		sw.WriteLine(jObject.ToString());

		sw.Close();
	}
	public void Load()
	{
        //StartCoroutine(LoadingScreen());

		string saveFilePath = Application.persistentDataPath + "/saveMania.fsav";

		StreamReader sr = new StreamReader(saveFilePath);
		string jsonString = sr.ReadToEnd();

		sr.Close();
		JObject jObject = JObject.Parse(jsonString);

		_lvl = (int)jObject["Data"]["Level"];

		//print(jObject.ToString());
	}

    /*private IEnumerator LoadingScreen()
    {
        loadingScreen.SetActive(true);

        AsyncOperation async = SceneManager.LoadSceneAsync("Game Scene");

		while (!async.isDone)
        {
            progressBar.fillAmount = async.progress;

			if (async.progress >= 0.95f)
			{
                progressText.text = "Press any key to continue";
            }
            yield return null;
        }
        loadingScreen.SetActive(false);
    }*/


	public void SaveOptions()
	{
		isFulscreen = fullScreenToggle.isOn;
        music = musicSlider.value;

        string saveFilePath = Application.persistentDataPath + "/optionsMania.fsav";
		//print("Saving to : " + saveFilePath);

		JObject jObject = new JObject
		{
			{ "Component Name", GetType().ToString() }
		};

		JObject jOptionsDataObject = new JObject();
        jObject.Add("options data", jOptionsDataObject);

        jOptionsDataObject.Add("Fulscreen", isFulscreen);
        jOptionsDataObject.Add("Music", music);

        StreamWriter sw = new StreamWriter(saveFilePath);
        sw.WriteLine(jObject.ToString());

        sw.Close();
    }

	public void LoadOptions()
	{
        if (!File.Exists(Application.persistentDataPath + "/optionsMania.fsav")) //check if there is a save file for the options
        {
			//sets them to default settings
			isFulscreen = true;
			Screen.fullScreen = true;
			
			music = musicSlider.maxValue;
        }
        else
        {
            string saveFilePath = Application.persistentDataPath + "/optionsMania.fsav";

            StreamReader sr = new StreamReader(saveFilePath);
            string jsonString = sr.ReadToEnd();

            sr.Close();
            JObject jObject = JObject.Parse(jsonString);

			isFulscreen = (bool)jObject["options data"]["Fulscreen"];
			music = (float)jObject["options data"]["Music"];
        }
    }
}
