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
            Destroy(this.gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            other.GetComponent<Health>().InflictDamage(5);
            Destroy(this.gameObject);
            Debug.Log("DAMAGE");
        }
        
    }

    public override void HitByGun(AbstractGun gun)
    {
        base.HitByGun(gun);
        
        //float enemyHue = gun.GetColorHue(color) * 360;
        //float hueDiff = Mathf.Abs(enemyHue - gun.Hue());
        //if (hueDiff <= gun.HueDamageRange) Destroy(this.gameObject);
    }
}
