using Newtonsoft.Json.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.IO;

public class SaveSystem : MonoBehaviour
{
	public static SaveSystem instance;

    /*public GameObject loadingScreen;
    public Image progressBar;
    public TextMeshProUGUI progressText;*/

    #region Options to save
    public float music;
    public bool isFulscreen;
    #endregion

    public int _lvl;

	private void Awake()
	{
		if (instance) Destroy(this);
		else instance = this;
	}

	/*[System.Serializable] //makes the class automatically serializable
	private class DataContainer
	{
		//this nested class is a Data Container for all the variables that need to be saved
		int _lvl;

		public DataContainer(int lvl)
		{
			_lvl = lvl;
		}
	}*/

	public void Save(int scene)
	{
		_lvl = scene;

        string saveFilePath = Application.persistentDataPath + "/saveGame.fsav";
		print("Saving to : " + saveFilePath);

		JObject jObject = new JObject();
		jObject.Add("componentName", GetType().ToString());

		JObject jDataObject = new JObject();
		jObject.Add("data", jDataObject);

		jDataObject.Add("_lvl", _lvl);

		StreamWriter sw = new StreamWriter(saveFilePath);
		sw.WriteLine(jObject.ToString());

		sw.Close();
	}
	public void Load()
	{
        //StartCoroutine(LoadingScreen());

        if (!File.Exists(Application.persistentDataPath + "/saveGame.fsav")) //check if there is a save file
		{
            SceneManager.LoadScene("Intro");
        }
		else
		{
			string saveFilePath = Application.persistentDataPath + "/saveGame.fsav";

			StreamReader sr = new StreamReader(saveFilePath);
			string jsonString = sr.ReadToEnd();

			sr.Close();
			JObject jObject = JObject.Parse(jsonString);

			_lvl = (int)jObject["data"]["_lvl"];

			//print(jObject.ToString());
		}
	}

    /*private void Update() //just some testing
	{
		if(Input.GetKeyDown(KeyCode.P))
		{
			Save();
		}

		if (Input.GetKeyDown(KeyCode.L))
		{
			Load();
		}
	}*/

    /*private IEnumerator LoadingScreen()
    {
        loadingScreen.SetActive(true);

        AsyncOperation async = SceneManager.LoadSceneAsync("Game Scene");
		while (!async.isDone)
        {
            //progressBar.fillAmount = async.progress;

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
		isFulscreen = Menu.menuInstance.fullscreenToggle.isOn;
        music = Menu.menuInstance.musicSlider.value;

        string saveFilePath = Application.persistentDataPath + "/saveOptions.fsav";
        print("Saving to : " + saveFilePath);

        JObject jObject = new JObject();
        jObject.Add("componentName", GetType().ToString());

        JObject jOptionsDataObject = new JObject();
        jObject.Add("options data", jOptionsDataObject);

        jOptionsDataObject.Add("isFulscreen", isFulscreen);
        jOptionsDataObject.Add("music", music);

        StreamWriter sw = new StreamWriter(saveFilePath);
        sw.WriteLine(jObject.ToString());

        sw.Close();
    }

	public void LoadOptions()
	{
        if (!File.Exists(Application.persistentDataPath + "/saveOptions.fsav")) //check if there is a save file for the options
        {
            Menu.menuInstance.resetOptionsDefault();
        }
        else
        {
            string saveFilePath = Application.persistentDataPath + "/saveOptions.fsav";

            StreamReader sr = new StreamReader(saveFilePath);
            string jsonString = sr.ReadToEnd();

            sr.Close();
            JObject jObject = JObject.Parse(jsonString);

			isFulscreen = (bool)jObject["options data"]["isFulscreen"];
			music = (float)jObject["options data"]["music"];
        }
    }
}
