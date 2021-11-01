using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementAndRotation : MonoBehaviour
{
   private Vector3 _direction, _currentDirection, _movement;
    private CharacterController _controller;

    private void Start()
    {
        _controller = GetComponent<CharacterController>();
    }
    public void Rotation(GameObject _target, float _speedRotation)
   {
        _direction = new Vector3(_target.transform.position.x, transform.position.y, _target.transform.position.z) - this.transform.position;
        _currentDirection = Vector3.Lerp(_currentDirection, _direction, _speedRotation * Time.deltaTime);
        this.transform.forward = _currentDirection;
   }
   
    public void Movement(float _realSpeed)
    {      
        _movement = new Vector3(0, -10, 0) + this.transform.forward * _realSpeed;
        _controller.Move(_movement * Time.deltaTime);
    }
}
