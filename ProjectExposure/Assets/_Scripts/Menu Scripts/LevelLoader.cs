using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Events;

public class LevelLoader : MonoBehaviour
{
	public UnityEvent OnLoadFinished;
	
	public UnityEvent OnLoadStarted;

	[SerializeField] 
	private float m_extraTimeToWait = 0.5f;

	private const float maxTimeNeededToCompleteLoad = 0.9f;

	AsyncOperation sceneLoadingData;

	private bool startedLoading = false;
	
	void Start ()
	{
		Application.backgroundLoadingPriority = ThreadPriority.Low;
	}

	public void LoadLevel(string levelName)
	{
		if (!startedLoading)
		{
			startedLoading = true;
			StartCoroutine(LoadLevelAsync(levelName));  //Start out Async Loading
		} 
	}
	
	public void LoadLevel(int index)
	{
		if (!startedLoading)
		{
			startedLoading = true;
			StartCoroutine(LoadLevelAsync(index));  //Start out Async Loading
		} 
	}

	IEnumerator LoadLevelAsync(string levelName)     //Coroutine allows to load the level without disturbing the main thread.
	{
		OnLoadStarted.Invoke();
		
		yield return new WaitForSeconds(m_extraTimeToWait);         // if you want to load slowly */
		
		sceneLoadingData = SceneManager.LoadSceneAsync(levelName);   //Get the data and load async
		
		sceneLoadingData.allowSceneActivation = false;           //Don't automatically go to the next level if finished

		while (!sceneLoadingData.isDone)
		{
			if (sceneLoadingData.progress >= maxTimeNeededToCompleteLoad) //If we are completed check If specified buttons are pressed and then go to next scene
			{
				OnLoadFinished.Invoke();
				sceneLoadingData.allowSceneActivation = true;
			}
			yield return null;
		}
	}
	
	IEnumerator LoadLevelAsync(int index)     //Coroutine allows to load the level without disturbing the main thread.
    	{
    		OnLoadStarted.Invoke();
    		
    		yield return new WaitForSeconds(m_extraTimeToWait);         // if you want to load slowly */
    		
    		sceneLoadingData = SceneManager.LoadSceneAsync(index);   //Get the data and load async
    		
    		sceneLoadingData.allowSceneActivation = false;           //Don't automatically go to the next level if finished
    
    		while (!sceneLoadingData.isDone)
    		{
    			if (sceneLoadingData.progress >= maxTimeNeededToCompleteLoad) //If we are completed check If specified buttons are pressed and then go to next scene
    			{
    				OnLoadFinished.Invoke();
    				sceneLoadingData.allowSceneActivation = true;
    			}
    			yield return null;
    		}
    	}

	public void StartLevel()
	{
		sceneLoadingData.allowSceneActivation = true;
	}
}
