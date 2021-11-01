using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleCameraController : MonoBehaviour
{
    [SerializeField] private GameObject player;

    private Vector3 _distance;
    void Start()
    {
        _distance = player.transform.position - this.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        this.transform.position = player.transform.position - _distance;
    }
}
