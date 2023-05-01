using Newtonsoft.Json.Linq;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SaveSystem : MonoBehaviour
{
	public static SaveSystem instance;

	public int _lvl;

	private void Awake()
	{
		if (instance) Destroy(this);
		else instance = this;
	}

	[System.Serializable] //makes the class automatically serializable
	private class DataContainer
	{
		//this nested class is a Data Container for all the variables that need to be saved
		int _lvl;

		public DataContainer(int lvl)
		{
			_lvl = lvl;
		}
	}

	public void Save(int scene)
	{
		_lvl = scene + 1;

		string saveFilePath = Application.persistentDataPath + "/saveGame.fsav";
		//print("Saving to : " + saveFilePath);

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
		string saveFilePath = Application.persistentDataPath + "/saveGame.fsav";

		StreamReader sr = new StreamReader(saveFilePath);
		string jsonString = sr.ReadToEnd();

		sr.Close();
		JObject jObject = JObject.Parse(jsonString);

		_lvl = (int)jObject["data"]["_lvl"];

		//print(jObject.ToString());
	}

	/*private void Update()
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
}
