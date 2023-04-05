using System.Collections;
using System.IO;
using UnityEngine;

public class SaveSystemBinary : MonoBehaviour
{
	//information variables : (HP, Player Lvl, Game Lvl, Player Name/Character, Inventory, etc...)
	//must read data in the same order as writen data
	public int lvl = 2;
	public string charaName = "Bob";
	
	void Update()
	{
		if(Input.GetKeyDown(KeyCode.P))
		{
			string saveFilePath = Application.persistentDataPath + "/saveGame.sav";
			print("Saving to : " + saveFilePath);

			FileStream fs = new FileStream(saveFilePath, FileMode.OpenOrCreate);
			BinaryWriter sw = new BinaryWriter(fs);

			sw.Write(lvl);
			sw.Write(charaName);

			fs.Close(); //always close stream & writer
			sw.Close();
		}

		if(Input.GetKeyDown(KeyCode.L))
		{
			string saveFilePath = Application.persistentDataPath + "/saveGame.sav";

			FileStream fs = new FileStream (saveFilePath, FileMode.Open);
			BinaryReader sr = new BinaryReader(fs);

			print("Current Level : " + sr.ReadInt32());
			print("Character Name : " + sr.ReadString());

			fs.Close(); //always close stream & reader
			sr.Close();
		}
	}
}
