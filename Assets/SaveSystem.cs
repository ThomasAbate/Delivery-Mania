using System.Collections;
using System.IO;
using UnityEngine;

public class SaveSystem : MonoBehaviour
{
	//information variables : (HP, Player Lvl, Game Lvl, Player Name/Character, Inventory, etc...)
	//need to save variables in the same order that they are loaded  &  not doward/upward compatible (i.e. when you add/remove variables from your saveGame it becomes corrupt)
	public int HP = 7;

	void Update()
	{
		if(Input.GetKeyDown(KeyCode.S))
		{
			string saveFilePath = Application.persistentDataPath + "/saveGame.sav";

			print("saving to : " + saveFilePath);

			StreamWriter sw = new StreamWriter(saveFilePath);
			sw.WriteLine("HP : " + HP);

			sw.Close();
		}
	}
}
