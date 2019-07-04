using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : Hittable {
    
    public float lifeTime = 10;
    private float m_creationTime;

    public void Start()
    {
        m_creationTime = Time.time;
    }

    private void Update()
    {
        if (Time.time > m_creationTime + lifeTime)
        {
            ObjectPooler.instance.DestroyFromPool("Projectile",this.gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            other.GetComponent<Health>().InflictDamage(5);
            ObjectPooler.instance.DestroyFromPool("Projectile", this.gameObject);
        }
        
    }

}
