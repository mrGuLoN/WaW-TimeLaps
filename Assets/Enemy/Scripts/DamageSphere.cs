using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageSphere : MonoBehaviour
{
    public float damage;
    

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            other.GetComponent<hp>().health -= damage;
        }      
    }

}
