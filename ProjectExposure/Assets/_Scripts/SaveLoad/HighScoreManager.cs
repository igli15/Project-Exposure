using System;
using System.Collections.Generic;
using System.IO;
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
	public HighscoreData[] highscoreArray;   //an array of data 
	
	public HighscoreData[] highscoreArrayYearly;  

	public IOrderedEnumerable<KeyValuePair<string, int>> orderedScores;

	Dictionary<string,int> highscoreDictionaryDaily = new Dictionary<string,int>();

	Dictionary<string,int> highscoreDictionaryYearly = new Dictionary<string,int>();
	
	private int m_highScore;
	
	public static HighScoreManager instance;

	[HideInInspector] public long m_saveDate = DateTime.Now.ToBinary();

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

		DateTime oldDate = DateTime.FromBinary(m_saveDate);

		TimeSpan saveSpan = DateTime.Now.Subtract(oldDate);
		
		if (highscoreArray != null)
		{
			highscoreDictionaryDaily = HighScoreDictionaryFromArray(highscoreArray);   //Load array if there is one
			highscoreDictionaryYearly = HighScoreDictionaryFromArray(highscoreArrayYearly);
		}
		
		if (saveSpan.Days >= 1)
		{
			highscoreDictionaryDaily.Clear();
			Debug.Log("one day passed");
			m_saveDate = DateTime.Now.ToBinary();
			SaveHighscore();
		}

		if (saveSpan.Days >= 364)
		{
			highscoreDictionaryYearly.Clear();
			Debug.Log("one year passed");
			m_saveDate = DateTime.Now.ToBinary();
			SaveHighscore();
		}

		
		OrderScores();
	}

	private void OrderScores()
	{
		orderedScores = from pair in highscoreDictionaryDaily
			orderby pair.Value descending 
			select pair;

		orderedScores = from pair in highscoreDictionaryYearly
			orderby pair.Value descending 
			select pair;
	}

	private void Update()
	{
		if (Input.GetKeyDown(KeyCode.R))
		{
			SaveLoadScript.DeleteJson("HighScores");
		}
		
		if (Input.GetKeyDown(KeyCode.S))
		{
			m_highScore = 100;
			SubmitHighScore("Igli");		
		}
	}

	public void SubmitHighScore(string userName)         //Check if there is a username or not and apply score properly then save locally
	{
		if(!highscoreDictionaryDaily.ContainsKey(userName))
		{
			highscoreDictionaryDaily.Add(userName,m_highScore);
			highscoreDictionaryYearly.Add(userName,m_highScore);
			SaveHighscore();
		}
		else
		{
			int score = highscoreDictionaryDaily[userName];

			if (score < m_highScore)
			{
				highscoreDictionaryDaily[userName] = m_highScore;
				highscoreDictionaryYearly[userName] = m_highScore;
			}
			
			SaveHighscore();
		}
	}

	private void SaveHighscore()
	{
		highscoreArray = HighScoreDictionaryToArray(highscoreDictionaryDaily);
		highscoreArrayYearly = HighScoreDictionaryToArray(highscoreDictionaryYearly);
		
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
