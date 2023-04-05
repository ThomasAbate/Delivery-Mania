using System.Collections;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public class SaveSystem : MonoBehaviour
{
	int _lvl = 3;
	int _score = 100;
	string _charaName = "John";
	int maxHP = 20; //doesn't need to be saved

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
		
		string saveFilePath = Application.persistentDataPath + "/saveGame.sav";
		FileStream file = new FileStream(saveFilePath, FileMode.OpenOrCreate);

		DataContainer dataContainer = new DataContainer(_score, _lvl, _charaName);

		BinaryFormatter binaryFormatter = new BinaryFormatter();
		
		binaryFormatter.Serialize(file, dataContainer); //serialize the data and write it to the file

		/*dataContainer.score = score;		  //create an instance of the nested class and sets it's values
		dataContainer.lvl = lvl;			 //
		dataContainer.charaName = charaName; //*/

		file.Close(); //don't forget to close
	}
	public void Load()
	{
		string saveFilePath = Application.persistentDataPath + "/saveGame.sav";
		FileStream file = new FileStream(saveFilePath, FileMode.OpenOrCreate);

		BinaryFormatter binaryFormatter = new BinaryFormatter();

		DataContainer dataContainer = binaryFormatter.Deserialize(file) as DataContainer;
		print("score : " + dataContainer._score);
		print("lvl : " + dataContainer._lvl);
		print("name : " + dataContainer._charaName);

		file.Close();
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
