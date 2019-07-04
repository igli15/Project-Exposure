using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Serialization;

public abstract class AbstractGun : MonoBehaviour
{
    [SerializeField] protected Transform m_origin;
    
    [FormerlySerializedAs("gunEffectGroups")] [SerializeField] private EffectGroup[] effectGroups;

    [SerializeField] private float m_sphereCastRadius = 2;

    private Color m_color = Color.red;

    private GunManager m_manager;
    

    public GunManager manager
    {
        get { return m_manager; }
        set { m_manager = value; }
    }

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
        return effectGroups.Length;
    }
	
    public EffectGroup GetEffectGroupAt(int index)
    {
        return effectGroups[index];
    }
    
    public abstract Hittable Shoot(int touchIndex);
    public abstract Hittable Shoot();
    
    public Vector3 GetDirFromGunToTouch()
    {
        Ray r = Camera.main.ScreenPointToRay(Input.GetTouch(manager.touchManager.GetIndexTheFirstTouchOnShootingArea()).position);
		
        r.origin =  origin.position;
        return r.direction;
    }
    
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
//        Debug.Log(manager.touchManager.GetIndexTheFirstTouchOnShootingArea());
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

    public Hittable RaycastFromGuns(int touchIndex)
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.GetTouch(touchIndex).position);
       // Debug.Log(touchIndex);
        RaycastHit hit;

      //  if (EventSystem.current.IsPointerOverGameObject()) return null;

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
