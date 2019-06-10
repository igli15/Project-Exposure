using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;

public abstract class AbstractGun : MonoBehaviour
{
    [SerializeField] protected Transform m_origin;
    
    [SerializeField] private GunEffectGroup[] gunEffectGroups;

    [SerializeField] private float m_sphereCastRadius = 2;

    private Color m_color = Color.red;

    public Transform origin
    {
        get { return m_origin; }
    }

    public Color color
    {
        get { return m_color; }
        set { m_color = value; }
    }

    public int GetEffectGroupCount()
    {
        return gunEffectGroups.Length;
    }
	
    public GunEffectGroup GetEffectGroupAt(int index)
    {
        return gunEffectGroups[index];
    }
    
    public abstract void Shoot();
    
    public Vector3 GetDirFromGunToMouse()
    {
        Ray r = Camera.main.ScreenPointToRay(Input.mousePosition);
		
        r.origin =  origin.position;
        return r.direction;
    }
    
    public Vector3 LookInRayDirection(Transform t,Ray ray)
    {
        Ray r = ray;
        r.origin = origin.position;
        Quaternion rot = Quaternion.LookRotation(r.direction.normalized,Vector3.up);
        t.DORotate(rot.eulerAngles, 0.5f);
        return r.direction;
    }
    
    public Hittable RaycastFromGuns()
    {        
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (EventSystem.current.IsPointerOverGameObject()) return null;

        if (Physics.SphereCast(ray.origin, m_sphereCastRadius, ray.direction, out hit, 2000))
        {
            Hittable h = hit.transform.GetComponent<Hittable>();
            if (h != null)
            {
                return h;
            }
        }

        return null;
    }
}
