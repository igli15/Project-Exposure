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
		File.WriteAllText(Application.persistentDataPath + "/"+ saveFileName + ".json", Encryption.Encrypt(data));
		
	}
    
	public static void Load(object objToLoad,string fileNameToLoadFrom)
	{
		if (File.Exists(Application.persistentDataPath + "/"+ fileNameToLoadFrom + ".json"))
		{
			var loadedData = Encryption.Decrypt(File.ReadAllText(Application.persistentDataPath + "/"+ fileNameToLoadFrom + ".json"));

			JsonUtility.FromJsonOverwrite(loadedData, objToLoad);

		}
	}
}
