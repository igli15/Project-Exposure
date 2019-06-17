using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using UnityEngine;

public static class SaveLoadScript 
{
	
	
	public static void Save(object objToSave,string saveFileName)
	{
		var data = JsonUtility.ToJson(objToSave, true);
		File.WriteAllText(Application.persistentDataPath + "/"+ saveFileName + ".json", data);
		
	}
    
	public static void Load(object objToLoad,string fileNameToLoadFrom)
	{
		if (File.Exists(Application.persistentDataPath + "/"+ fileNameToLoadFrom + ".json"))
		{
			var loadedData = File.ReadAllText(Application.persistentDataPath + "/"+ fileNameToLoadFrom + ".json");
			Debug.Log(loadedData);
			JsonUtility.FromJsonOverwrite(loadedData, objToLoad);

		}
	}
	
	public static void DeleteJson(string fileNameToLoadFrom)
	{
		if (File.Exists(Application.persistentDataPath + "/"+ fileNameToLoadFrom + ".json"))
		{
			File.Delete(Application.persistentDataPath + "/"+ fileNameToLoadFrom + ".json");
		}
	}
}
