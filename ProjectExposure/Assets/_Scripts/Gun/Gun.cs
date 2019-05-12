using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Serialization;

public class Gun : MonoBehaviour,IAgent
{

    public Action<Gun> OnChargeChanged;
    public Action<Gun> OnHueChanged;

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
    private float m_baseDamage = 10;

    [SerializeField] 
    private float m_extraDamage = 20;

    [SerializeField] 
    private float m_aoeDamage = 5;

    [SerializeField] 
    private GameObject m_mergeBeam;
    
    [SerializeField]
    private KeyCode m_increaseKeyCode;
    [SerializeField]
    private KeyCode m_decreseKeycode;
    
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
        if (OnChargeChanged != null) OnChargeChanged(this);
        if (OnChargeChanged != null) OnHueChanged(this);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Shoot();
        }

        if (Input.GetKey(m_decreseKeycode))
        {
            IncreaseCharge();
        }
        else if (Input.GetKey(m_increaseKeyCode))
        {
            DecreaseCharge();
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
            Hittable hittable = hit.transform.gameObject.GetComponent<Hittable>();
            
            if(hittable != null)
            {
                hittable.HitByGun(CalculateDamage(hittable),this);
            }
        }
    }

    public float GetColorHue(Color color)
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

        if (OnHueChanged != null) OnHueChanged(this);
        return Color.HSVToRGB(newHue / 360, saturation, value);
    }

    float CalculateDamage(Hittable enemy)
    {
        float damage = 0;
        float enemyHue = GetColorHue(enemy.color)*360;
        float hueDiff = Mathf.Abs(enemyHue - m_hue);
        //Debug.Log("GUN_HUE: " + m_hue);
        //Debug.Log("ENEMY_HUE: " + enemyHue);
        //Debug.Log("DIFF " + hueDiff);
        if (hueDiff <= m_hueDamageRange)
        {
            float precisionLevel= ((m_hueDamageRange - hueDiff) / m_hueDamageRange);
            damage =  m_baseDamage + precisionLevel * m_extraDamage;
        }

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
}
