using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Linq;

[Serializable]
public class HighScoreManager : MonoBehaviour
{
	[Serializable]
	public class HighscoreData      //Making a dictionary manually
	{
		public string name;
		public int score;
	}

	[HideInInspector]
	public HighscoreData[] highscoreArray;   //an array of data ;D


	Dictionary<string,int> highscoreDictionary = new Dictionary<string,int>();


	public static HighScoreManager instance;

	private void Awake()
	{
		#region SINGELTON          
		
		if (instance == null)
		{
			instance = this;
		}
		else
		{
			Destroy(gameObject);
		}
		
		#endregion  
		
		DontDestroyOnLoad(gameObject);
		
		//SceneManager.sceneLoaded += OnSceneLoaded;
		
		SaveLoadScript.Load(this,"HighScores");
		if (highscoreArray != null)
		{
			highscoreDictionary = HighScoreDictionaryFromArray(highscoreArray);   //Load array if there is one
		}
	}


	private void SaveHighscore()
	{
		highscoreArray = HighScoreDictionaryToArray(highscoreDictionary);
		SaveLoadScript.Save(this,"HighScores");
	}
	
	public HighscoreData[] HighScoreDictionaryToArray(Dictionary<string,int> dictionaryToSerialize)   //Returns An array from dictionary
	{
		List<HighscoreData> highscoreDataList = new List<HighscoreData>();

		foreach (KeyValuePair<string,int> pairs in dictionaryToSerialize)
		{
			HighscoreData highscoreData = new HighscoreData();
			highscoreData.name = pairs.Key;
			highscoreData.score = pairs.Value;
			highscoreDataList.Add(highscoreData);
		}
		

		HighscoreData[] arrayToReturn = highscoreDataList.ToArray();
		return arrayToReturn;
	}

	public Dictionary<string, int> HighScoreDictionaryFromArray(HighscoreData[] arrayOfData)    //Returns a dictionary from the array
	{
		Dictionary<string, int> dictionaryToReturn = new Dictionary<string, int>();
		
		for (int i = 0; i < arrayOfData.Length ; i++)
		{
			dictionaryToReturn.Add(arrayOfData[i].name,arrayOfData[i].score);
		}
		
		var list = dictionaryToReturn.ToList();

		list.Sort((pair1,pair2) => pair1.Value.CompareTo(pair2.Value));
		
		return dictionaryToReturn;
	}

}
