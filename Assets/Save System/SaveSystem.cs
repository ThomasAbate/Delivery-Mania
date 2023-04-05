using Newtonsoft.Json.Linq;
using System.Collections;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public class SaveSystem : MonoBehaviour
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

	public void Save()
	{
		string saveFilePath = Application.persistentDataPath + "/saveGame1.fsav";
		print("Saving to : " + saveFilePath);

		JObject jObject = new JObject();
		jObject.Add("componentName", GetType().ToString());

		JObject jDataObject = new JObject();
		jObject.Add("data", jDataObject);

		jDataObject.Add("_score", _score);
		jDataObject.Add("_lvl", _lvl);
		jDataObject.Add("_charaName", _charaName);
		
		StreamWriter sw = new StreamWriter(saveFilePath);
		sw.WriteLine(jObject.ToString());

		sw.Close();

		/*
		FileStream file = new FileStream(saveFilePath, FileMode.OpenOrCreate);

		DataContainer dataContainer = new DataContainer(_score, _lvl, _charaName);

		BinaryFormatter binaryFormatter = new BinaryFormatter();
		
		binaryFormatter.Serialize(file, dataContainer); //serialize the data and write it to the file

		/*dataContainer.score = score;		  //create an instance of the nested class and sets it's values
		dataContainer.lvl = lvl;			 //
		dataContainer.charaName = charaName; //

		file.Close(); //don't forget to close
		*/
	}
	public void Load()
	{
		string saveFilePath = Application.persistentDataPath + "/saveGame1.fsav";

		StreamReader sr = new StreamReader(saveFilePath);
		string jsonString = sr.ReadToEnd();

		sr.Close();
		JObject jObject = JObject.Parse(jsonString);

		
		_score = (int)jObject["data"]["_score"];
		_lvl = (int)jObject["data"]["_lvl"];
		_charaName = (string)jObject["data"]["_charaName"];

		print(jObject.ToString());

		/*FileStream file = new FileStream(saveFilePath, FileMode.Open); 

		BinaryFormatter binaryFormatter = new BinaryFormatter();

		DataContainer dataContainer = binaryFormatter.Deserialize(file) as DataContainer;
		print("score : " + dataContainer._score);
		print("lvl : " + dataContainer._lvl);
		print("name : " + dataContainer._charaName);

		file.Close();*/
	}

	private void Update()
	{
		if(Input.GetKeyDown(KeyCode.P))
		{
			Save();
		}

		if (Input.GetKeyDown(KeyCode.L))
		{
			Load();
		}
	}
}
