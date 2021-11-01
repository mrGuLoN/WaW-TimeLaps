using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAudio : MonoBehaviour
{
    private AudioSource _step;

    private bool _inMove;
    void Start()
    {
        _step = GetComponent<AudioSource>();
        _inMove = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void PlayStep()
    {
        if (_inMove == false)
        {
            _inMove = true;
            _step.Play();
        }
    }

    public void StopStep()
    {
        _inMove = false;
        _step.Stop();
    }
}
