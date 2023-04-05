using System.Collections;
using System.IO;
using UnityEngine;

public class SaveSystem : MonoBehaviour
{
	//information variables : (HP, Player Lvl, Game Lvl, Player Name/Character, Inventory, etc...)
	//need to save variables in the same order that they are loaded  &  not doward/upward compatible (i.e. when you add/remove variables from your saveGame it becomes corrupt)
	public int HP = 10;
	public string _name = "Bob";
	
	void Update()
	{
		if(Input.GetKeyDown(KeyCode.P))
		{
			string saveFilePath = Application.persistentDataPath + "/saveGame.sav";
			print("Saving to : " + saveFilePath);

			FileStream fs = new FileStream(saveFilePath, FileMode.OpenOrCreate);
			BinaryWriter sw = new BinaryWriter(fs);

			sw.Write(HP);
			sw.Write(_name);

			fs.Close();
			sw.Close();
		}

		if(Input.GetKeyDown(KeyCode.L))
		{
			string saveFilePath = Application.persistentDataPath + "/saveGame.sav";

			FileStream fs = new FileStream (saveFilePath, FileMode.Open);
			BinaryReader sr = new BinaryReader(fs);

			print("HP : " + sr.ReadInt32());
			print("name : " + sr.ReadString());

			fs.Close();
			sr.Close();
		}
	}
}
