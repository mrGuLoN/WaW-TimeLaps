using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SearchTarget : MonoBehaviour
{
    private GameObject _player;
    // Start is called before the first frame update
    void Awake()
    {
        _player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    public void SearchPlayer(out GameObject _target)
    {       
        _target = GameObject.FindGameObjectWithTag("Player");       
    }  
}
