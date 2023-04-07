using Newtonsoft.Json.Linq;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;

public class JsonUtilSave : MonoBehaviour
{
	public int _options;

	public string _lvl;

	public string _charaName;

	[System.Serializable] //makes the class automatically serializable
	private class DataContainer
	{
		//this nested class is a Data Container for all the variables that need to be saved
		public int _options;

		public string _lvl;

		public string _charaName;

		public DataContainer(int score, string lvl, string charaName)
		{
			_options = score;

			_lvl /*= lvl*/ = SceneManager.GetActiveScene().name;

			_charaName = charaName;
		}
	}

	public void Saveju()
	{
		DataContainer data = new DataContainer(_options, _lvl, _charaName);

		string jsonString = JsonUtility.ToJson(data);

		string saveFilePath = Application.persistentDataPath + "/save" + 1 + ".fsav";
		print("Saving : " + jsonString + "\n to : " + saveFilePath);

		StreamWriter sw = new StreamWriter(saveFilePath);
		sw.WriteLine(jsonString);

		sw.Close();
	}
	public void Loadju()
	{
		string saveFilePath = Application.persistentDataPath + "/save1.fsav";

		StreamReader sr = new StreamReader(saveFilePath);
		string jsonString = sr.ReadToEnd();

		sr.Close();
		
		print(jsonString);

		DataContainer data = JsonUtility.FromJson<DataContainer>(jsonString);

		_options = data._options;
		_lvl = data._lvl;
		_charaName = data._charaName;
	}


	void Update()
    {
		if (Input.GetKeyDown(KeyCode.P))
		{
			Saveju();
		}

		if (Input.GetKeyDown(KeyCode.L))
		{
			Loadju();
		}
	}
}
