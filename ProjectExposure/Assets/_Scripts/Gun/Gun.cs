using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Serialization;

public class Gun : MonoBehaviour
{

    public Action<Gun> OnChargeChanged;

    [SerializeField]
    private Vector2 m_gunColorRange = Vector2.zero;

    [SerializeField]
    private Color m_gunColor;

    [SerializeField]
    private float m_gunChargeSpeed = 10;
    
    [SerializeField]
    private float m_hueDamageRange = 40;

    [SerializeField]
    private float m_aoeRange = 5;
    
    [SerializeField]
    private KeyCode m_keyCode;

    private Renderer m_renderer;

    private float m_hue = 0;
    
    [SerializeField]
    private float m_baseDamage = 10;

    [SerializeField] 
    private float m_extraDamage = 20;

    [SerializeField] 
    private float m_aoeDamage = 5;

    private bool m_isAoe = false;
    // Use this for initialization
    void Start()
    {
        m_renderer = GetComponent<Renderer>();

        m_renderer.material.color = m_gunColor;

        m_hue = GetColorHue(m_renderer.material.color) * 360;
        if (OnChargeChanged != null) OnChargeChanged(this);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Shoot();
        }
    }

    void Shoot()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        RaycastHit[] hits;
        hits = Physics.RaycastAll(ray);

        for (int i = 0; i < hits.Length; i++)
        {
            RaycastHit hit = hits[i];
            if (hit.transform.gameObject.CompareTag("Enemy"))
            {
                Enemy enemy = hit.transform.gameObject.GetComponent<Enemy>();
                enemy.GetDamagedByHue(CalculateDamage(enemy));
                
                if (m_isAoe)
                {
                    Debug.Log("AOE");
                    Collider[] aoeColliders = Physics.OverlapSphere(hit.point, m_aoeRange);
                    foreach (Collider coll in aoeColliders)
                    {
                        if(hit.collider!=coll && coll.CompareTag("Enemy"))
                        coll.gameObject.GetComponent<Enemy>().GetDamagedByHue(m_aoeDamage);
                    }
                }
            }
            if (hit.transform.gameObject.CompareTag("Projectile"))
            {
                Projectile projectile = hit.collider.GetComponent<Projectile>();
                float enemyHue = GetColorHue(projectile.color) * 360;
                float hueDiff = Mathf.Abs(enemyHue - m_hue);
                if (hueDiff <= m_hueDamageRange) Destroy(hit.collider.gameObject);
            }
        }
    }

    float GetColorHue(Color color)
    {
        float hue = 1;
        float saturation = 0;
        float value = 0;

        Color.RGBToHSV(color, out hue, out saturation, out value);
        return hue;
    }

    public void IncreaseCharge()
    {
        if (m_hue < m_gunColorRange.y)
        {
            m_hue += m_gunChargeSpeed * Time.deltaTime;
            m_renderer.material.color = ChangeHue(m_gunColor, m_hue);
            if (OnChargeChanged != null) OnChargeChanged(this);
        }
    }

    public void DecreaseCharge()
    {
        if (m_hue > m_gunColorRange.x)
        {
            m_hue -= m_gunChargeSpeed * Time.deltaTime;
            m_renderer.material.color = ChangeHue(m_gunColor, m_hue);
            if (OnChargeChanged != null) OnChargeChanged(this);
        }
    }

    Color ChangeHue(Color color, float newHue)
    {
        float hue = 0;
        float saturation = 0;
        float value = 0;

        Color.RGBToHSV(color, out hue, out saturation, out value);


        return Color.HSVToRGB(newHue / 360, saturation, value);
    }

    float CalculateDamage(Enemy enemy)
    {
        float damage = 0;
        float enemyHue = GetColorHue(enemy.color)*360;
        float hueDiff = Mathf.Abs(enemyHue - m_hue);
        Debug.Log("GUN_HUE: " + m_hue);
        Debug.Log("ENEMY_HUE: " + enemyHue);
        Debug.Log("DIFF " + hueDiff);
        if (hueDiff <= m_hueDamageRange)
        {
            float precisionLevel= ((m_hueDamageRange - hueDiff) / m_hueDamageRange);
            damage =  m_baseDamage + precisionLevel * m_extraDamage;
        }

        return damage;
    }

    public float Hue()
    {
        return m_hue;
    }

    public void SetAoe(bool b)
    {
        m_isAoe = b;
    }
    
}
