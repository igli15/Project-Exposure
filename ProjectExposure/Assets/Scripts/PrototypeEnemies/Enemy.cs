using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public Color color;
    public Color damageColor;

    public float baseDamage = 10;
    public float extraDamage = 20;

    public float range = 30;

    private Health m_health;

    public void Start()
    {
        GetComponent<MeshRenderer>().material.color = color;
        m_health = GetComponent<Health>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {

            float attackHue = GetColorHue(damageColor);
            GetDamageByHue(attackHue);
        }
    }

    void GetDamageByHue(float hue)
    {
        float myHue = GetColorHue(color);
        float hueDiff = Mathf.Abs(myHue - hue);

        if (hueDiff < range)
        {
            float precisionLevel= ((range - hueDiff) / range);
            m_health.InflictDamage(baseDamage + precisionLevel * extraDamage);
            Debug.Log("precisionLevel: " + precisionLevel);

        }
    }

    float GetColorHue(Color color)
    {
        float hue = 0;
        float saturation = 0;
        float value = 0;

        Color.RGBToHSV(color, out hue, out saturation, out value);
        return hue*360;
    }
}
