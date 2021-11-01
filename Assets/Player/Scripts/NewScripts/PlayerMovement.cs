using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private CharacterController _characterController;
    private Vector3 _screenMousePosition, _worldMousePos, _direction, _currenDirection;
    private Camera _cam;


    private void Start()
    {
        _characterController = GetComponent<CharacterController>();
        _cam = Camera.main;
    }

    public void Movement(float speed, float gravity, Vector3 moveDir)
    {
        moveDir *= speed;
        moveDir.y -= gravity * Time.deltaTime;
        _characterController.Move(moveDir * Time.deltaTime);
    }

    public void Rotation(float speedRotation)
    {
        _screenMousePosition = Input.mousePosition;
        RaycastHit hit;
        Ray ray = _cam.ScreenPointToRay(_screenMousePosition);
        if (Physics.Raycast(ray, out hit))
        {
            if (hit.transform.CompareTag("Enemy"))
            {
                _worldMousePos = new Vector3(hit.transform.position.x, this.transform.position.y, hit.transform.position.z);
            }
            else
            {
                _worldMousePos = new Vector3(hit.point.x, this.transform.position.y, hit.point.z);
            }
            _direction = _worldMousePos - this.transform.position;
            _currenDirection = Vector3.Lerp(_currenDirection, _direction, speedRotation * Time.deltaTime);
            this.transform.forward = _currenDirection;
        }
    }

    public void MouseRotation (float speedRotation)
    {
        this.transform.Rotate(0, Input.GetAxis("Mouse X") * speedRotation, 0);
    }
}
