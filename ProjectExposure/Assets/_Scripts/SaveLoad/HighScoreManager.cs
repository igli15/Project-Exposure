using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[Serializable]
public class HighScoreManager : MonoBehaviour
{
	[Serializable]
	public class HighscoreData      //Making a dictionary manually
	{
		public string name;
		public int score;

		public override string ToString()
		{
			return "name: "+ name + " " + "Score: "+ score;
		}
	}

	[HideInInspector]
	public HighscoreData[] highscoreArray;   //an array of data ;D

	public IOrderedEnumerable<KeyValuePair<string, int>> orderedScores;

	Dictionary<string,int> highscoreDictionary = new Dictionary<string,int>();

	private int m_highScore;
	
	public static HighScoreManager instance;


	public int highScore
	{
		get { return m_highScore; }
		set { m_highScore = value; }
	}

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
		
	}

	public void LoadHighScores()
	{
		SaveLoadScript.Load(this,"HighScores");
		if (highscoreArray != null)
		{
			highscoreDictionary = HighScoreDictionaryFromArray(highscoreArray);   //Load array if there is one
		}
		
		OrderScores();
	}

	private void OrderScores()
	{
		orderedScores = from pair in highscoreDictionary
			orderby pair.Value descending 
			select pair;

	}

	private void Update()
	{
		if (Input.GetKeyDown(KeyCode.A))
		{
			SubmitHighScore("Igli");
		}
		
		if (Input.GetKeyDown(KeyCode.B))
		{
			SubmitHighScore("Test");
		}
		
		if (Input.GetKeyDown(KeyCode.C))
		{
			highscoreArray = null;
		}
	}

	public void SubmitHighScore(string userName)         //Check if there is a username or not and apply score properly then save locally
	{
		if(!highscoreDictionary.ContainsKey(userName))
		{
			highscoreDictionary.Add(userName,m_highScore);
			SaveHighscore();
		}
		else
		{
			int score = highscoreDictionary[userName];

			if (score < m_highScore)
			{
				highscoreDictionary[userName] = m_highScore;
			}
			
			SaveHighscore();
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
		
		return dictionaryToReturn;
	}

}
