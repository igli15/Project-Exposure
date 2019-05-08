using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour {

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            other.GetComponent<Health>().InflictDamage(5);
            Destroy(this.gameObject);
            Debug.Log("DAMAGE");
        }
        
    }
}
