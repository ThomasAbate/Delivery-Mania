using Newtonsoft.Json.Linq;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class JsonUtilSave : MonoBehaviour
{
	public int _lvl;
	public int _score;
	public string _charaName;
	public int maxHP = 20; //doesn't need to be saved

	[System.Serializable] //makes the class automatically serializable
	private class DataContainer
	{
		//this nested class is a Data Container for all the variables that need to be saved
		public int _score;
		public int _lvl;
		public string _charaName;

		public DataContainer(int score, int lvl, string charaName)
		{
			_score = score;
			_lvl = lvl;
			_charaName = charaName;
		}
	}

	public void Saveju()
	{
		DataContainer data = new DataContainer(_score, _lvl, _charaName);

		string jsonString = JsonUtility.ToJson(data);

		string saveFilePath = Application.persistentDataPath + "/saveGame1.fsav";
		print("Saving to : " + saveFilePath + "\n" + jsonString);

		StreamWriter sw = new StreamWriter(saveFilePath);
		sw.WriteLine(jsonString);

		sw.Close();
	}
	public void Loadju()
	{
		string saveFilePath = Application.persistentDataPath + "/saveGame1.fsav";

		StreamReader sr = new StreamReader(saveFilePath);
		string jsonString = sr.ReadToEnd();

		sr.Close();
		
		print(jsonString);

		DataContainer data = JsonUtility.FromJson<DataContainer>(jsonString);

		_score = data._score;
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
