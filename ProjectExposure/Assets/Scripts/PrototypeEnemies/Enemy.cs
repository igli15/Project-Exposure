using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public Color color;
    public Color damageColor;
    public float range = 20;

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
            float myHue = GetColorHue(color);
            float attackHue = GetColorHue(damageColor);
            float hueDiff = Mathf.Abs(myHue - attackHue);

            if (hueDiff < range)
            {
                m_health.InflictDamage(5);
                Debug.Log("Damaged");
            }
            Debug.Log("DIF: " + hueDiff);
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
