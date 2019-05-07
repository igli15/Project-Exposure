using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class HealthBarManager : MonoBehaviour
{
    [SerializeField]
    private Slider m_slider;

    private Health m_health;


	void Start ()
    {
        m_health = GetComponent<Health>();
        m_health.OnHealthDecreased += OnHealthChanged;
        m_health.OnHealthIncreased += OnHealthChanged;
        OnHealthChanged(m_health);
	}
	
	// Update is called once per frame
	void Update ()
    {
        //m_health.InflictDamage(0.1f);
    }
    void OnHealthChanged(Health health)
    {
        m_slider.value = health.HP;
    }
}
