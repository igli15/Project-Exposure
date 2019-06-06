using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class Enemy : Hittable
{
    //public Color color;
    public GameObject crystalsParrent;
    private List<GameObject> m_crystals;
    private int m_maxCrystalCount = 1;

    public void Start()
    {
        SetColor(color);
        m_crystals = new List<GameObject>();

        Transform[] buffer = crystalsParrent.transform.GetComponentsInChildren<Transform>();
        for (int i = 1; i < buffer.Length; i++)
        {
            buffer[i].GetComponent<Crystal>().Start();
            buffer[i].GetComponent<Crystal>().enabled = false;

             m_crystals.Add(buffer[i].gameObject);
        }
        m_maxCrystalCount =buffer.Length-1;
        GetComponent<Health>().OnHealthDecreased += OnHealthChanged;
    }

    public void RemoveCrystal()
    {
        if (m_crystals.Count == 0) return;

        GameObject crystal = m_crystals[0];
        m_crystals.RemoveAt(0);
        crystal.gameObject.SetActive(false);
        //crystal.GetComponent<Rigidbody>().AddExplosionForce(2, transform.position, 10);
    }

    public void OnHealthChanged(Health health)
    {
        float healthPercantage = health.HP / health.MaxHealth;
        float crystalPercentage = m_crystals.Count / m_maxCrystalCount;
        if (crystalPercentage > healthPercantage)
        {
            float delta = crystalPercentage - healthPercantage;
            int countToRemove = (int) ( delta * m_maxCrystalCount );
            Debug.Log("DELTA: " + delta+" / "+countToRemove);
            for (int i = 0; i < countToRemove; i++)
            {
                RemoveCrystal();
            }
        }
    }

    private void Update()
    {
     
    }

}
