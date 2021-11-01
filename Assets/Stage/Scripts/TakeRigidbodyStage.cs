using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TakeRigidbodyStage : MonoBehaviour
{  
    private void Start()
    {
        foreach (Transform _childGameObject in transform.GetComponentsInChildren<Transform>())
        {
            _childGameObject.gameObject.AddComponent<Rigidbody>();
            _childGameObject.gameObject.GetComponent<Rigidbody>().useGravity = false;
            _childGameObject.gameObject.GetComponent<Rigidbody>().isKinematic = true;
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
