﻿using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Serialization;

public class Gun : MonoBehaviour,IAgent
{
    public Action<Gun> OnHueChanged;

    [SerializeField] private Vector2 m_gunColorRange = Vector2.zero;

    [SerializeField] private Color m_gunColor;

    [SerializeField] private float m_gunChargeSpeed = 10;
    
    [SerializeField] private float m_hueDamageRange = 40;

    [SerializeField] private float m_aoeRange = 5;
    
    [SerializeField] private float m_baseDamage = 10;

    [SerializeField] private float m_extraDamage = 20;

    [SerializeField] private float m_aoeDamage = 5;

    [SerializeField] private GameObject m_mergeBeam;
    
    [SerializeField] private KeyCode m_increaseKeyCode;
    [SerializeField] private KeyCode m_decreaseKeycode;

    private GunManager m_manager;
    
    private Renderer m_renderer;

    private float m_hue = 0;


    private bool m_isAoe = false;

    private Fsm<Gun> m_fsm;
    
    // Use this for initialization
    void Start()
    {
    
        if (m_fsm == null)
        {
            m_fsm = new Fsm<Gun>(this);
        }
        
        m_fsm.ChangeState<SeperatedGunState>();
        
        m_renderer = GetComponent<Renderer>();

        //m_renderer.material.color = m_gunColor;
        m_renderer.material.SetColor("_Color",m_gunColor);
        
        m_hue = GetColorHue(m_renderer.material.color) * 360;
        
        OnHueChanged += SetBeamColor;

        if(OnHueChanged!= null) OnHueChanged(this);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Shoot();
        }

        if (Input.GetKey(m_decreaseKeycode))
        {
            IncreaseHue();
        }
        else if (Input.GetKey(m_increaseKeyCode))
        {
            DecreaseHue();
        }
    }

    void Shoot()
    {
        if(EventSystem.current.IsPointerOverGameObject()) return;
        
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        
        if(fsm.GetCurrentState() is SeperatedGunState)
        LookInRayDirection(ray);
      
        RaycastHit[] hits;
        hits = Physics.RaycastAll(ray);

        for (int i = 0; i < hits.Length; i++)
        {
            RaycastHit hit = hits[i];
            Hittable hittable = hit.transform.gameObject.GetComponent<Hittable>();
            
            if(hittable != null)
            {
                hittable.HitByGun(CalculateDamage(hittable.color),this);
            }
        }
    }

    void LookInRayDirection(Ray ray)
    {
        Ray r = ray;
        r.origin = transform.position;
        Quaternion rot = Quaternion.LookRotation(r.direction.normalized,Vector3.up);
        Sequence s = DOTween.Sequence();
        Tween t = transform.DORotate(rot.eulerAngles, 0.5f);
    }
    public float GetColorHue(Color color)
    {
        float hue = 1;
        float saturation = 0;
        float value = 0;

        Color.RGBToHSV(color, out hue, out saturation, out value);
        return hue;
    }

    public void IncreaseHue()
    {
        if (m_hue < m_gunColorRange.y)
        {
            m_hue += m_gunChargeSpeed * Time.deltaTime;
            m_renderer.material.color = ChangeHue(m_gunColor, m_hue);
        }
    }

    public void DecreaseHue()
    {
        if (m_hue > m_gunColorRange.x)
        {
            m_hue -= m_gunChargeSpeed * Time.deltaTime;
            m_renderer.material.color = ChangeHue(m_gunColor, m_hue);
        }
    }

    Color ChangeHue(Color color, float newHue)
    {
        float hue = 0;
        float saturation = 0;
        float value = 0;

        Color.RGBToHSV(color, out hue, out saturation, out value);

        if (OnHueChanged != null) OnHueChanged(this);
        return Color.HSVToRGB(newHue / 360, saturation, value);
    }

    float CalculateDamage(Color color)
    {
        float damage = 0;
        float enemyHue = GetColorHue(color)*360;

        float hue = m_hue;

        if (m_isAoe)
        {
            hue = GetColorHue(manager.mergeSphere.GetComponent<Renderer>().material.color) * 360;
        }
        
        
        float hueDiff = Mathf.Abs(enemyHue - hue);

        if (hueDiff <= m_hueDamageRange)
        {
            float precisionLevel= ((m_hueDamageRange - hueDiff) / m_hueDamageRange);
            damage =  m_baseDamage + precisionLevel * m_extraDamage;
        }
        Debug.Log(damage);
        return damage;
    }
    
    public void SetBeamColor(Gun gun)
    {
        Color c = m_renderer.material.GetColor("_Color");
        c.a = 0.5f;

        Renderer beamRender = m_mergeBeam.GetComponent<Renderer>();
        beamRender.material.color = c;
        beamRender.material.SetFloat("_Wavelength",((m_hue/300 - 2 ) * -1) * 2);
    }

    public void SetHue(float hue)
    {
        m_hue = hue * 300;
        Color color = ChangeHue(m_gunColor, m_hue);
        
        if(m_renderer != null)
            m_renderer.material.SetColor("_Color",color);
    }

    public void ChangeToMergeState()
    {
        m_fsm.ChangeState<MergedGunState>();
    }
    
    public void ChangeToSeperatedState()
    {
        m_fsm.ChangeState<SeperatedGunState>();
    }
    
    public float Hue()
    {
        return m_hue;
    }

    public void SetAoe(bool b)
    {
        m_isAoe = b;
    }

    public bool IsAoe()
    {
        return m_isAoe;
    }

    public float AoeRange()
    {
        return m_aoeRange;
    }

    public float AoeDamage()
    {
        return m_aoeDamage;
    }

    public float HueDamageRange
    {
        get { return m_hueDamageRange; }
    }

    public Color gunColor
    {
        get { return m_gunColor; }
    }

    public Fsm<Gun> fsm
    {
        get { return m_fsm; }
    }
    

    public GunManager manager
    {
        get { return m_manager; }
        set { m_manager = value; }
    }
}
