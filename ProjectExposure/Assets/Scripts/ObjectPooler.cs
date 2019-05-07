using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class ObjectPooler : MonoBehaviour
{

	[System.Serializable]
	public class Pool
	{
		public string tag;
		public GameObject prefab;
		public int size;
	}

	public List<Pool> pools;

	public Dictionary<string, Queue<GameObject>> poolDictionary;

	[HideInInspector]
	public static ObjectPooler instance;

	private void Awake()
	{
		if (instance == null)
		{
			instance = this;
		}
	}

	// Use this for initialization
	void Start () 
	{
		poolDictionary = new Dictionary<string, Queue<GameObject>>();
		
		foreach (Pool pool in pools)
		{
			Queue<GameObject> objectPool = new Queue<GameObject>();

			for (int i = 0; i < pool.size; i++)
			{
				GameObject poolObject = Instantiate(pool.prefab);
				poolObject.SetActive(false);
				objectPool.Enqueue(poolObject );
			}
			
			poolDictionary.Add(pool.tag,objectPool);
		}
	}

	public GameObject SpawnFromPool(string pTag,Vector3 pPos,Quaternion pRot)
	{

		GameObject objToSpawn =	poolDictionary[pTag].Dequeue();
        
		objToSpawn.SetActive(true);
		objToSpawn.transform.position = pPos;
		objToSpawn.transform.rotation = pRot;

		IPooleableObject pooledObj = objToSpawn.GetComponent<IPooleableObject>();
		
		if(pooledObj != null)
		pooledObj.OnObjectSpawn();
		
		poolDictionary[pTag].Enqueue(objToSpawn);
		

		return objToSpawn;
	}

	public void DestroyFromPool(string pTag,GameObject objToDestroy)
       	{
        //GameObject objToDestroy = poolDictionary[pTag].Dequeue();
       Debug.Log("SIZE: "+ poolDictionary[pTag].Count );
       		objToDestroy.SetActive(false);

        if (!poolDictionary[pTag].Contains(objToDestroy))
            poolDictionary[pTag].Enqueue(objToDestroy);
        //else Debug.Log("Somehting went wrong with tag: "+pTag);
       	}

}
