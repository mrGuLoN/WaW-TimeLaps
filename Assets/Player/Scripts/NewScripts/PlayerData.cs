using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerData : MonoBehaviour
{
    [SerializeField] private float speedMovement, speedRotation, gravitation;
    [SerializeField] private Transform rifleInHand, rifleNotHand;
    public GameObject gun1, gun2;

    private PlayerMovement _playerMovement;
    private PlayerAnimation _playerAnimation;
    private PlayerAudio _playerAudio;
    private CharacterController _characterController;
    private Camera _cam;
    private PlayerGun _plGun1, _plGun2, _activeGun;

    private GameObject _gun1, _gun2;
    private Vector3 _moveDir;
    private bool _inFly, _gun1InHand;
    void Awake()
    {
        _playerMovement = GetComponent<PlayerMovement>();
        _playerAnimation = GetComponent<PlayerAnimation>();
        _playerAudio = GetComponent<PlayerAudio>();
        _characterController = GetComponent<CharacterController>();
        _cam = Camera.main;
        _gun1 = Instantiate(gun1, rifleInHand.position, rifleInHand.rotation);
        _gun1.transform.parent = rifleInHand;
        _plGun1 = _gun1.GetComponent<PlayerGun>();
        _gun2 = Instantiate(gun2, rifleNotHand.position, rifleNotHand.rotation);
        _gun2.transform.parent = rifleNotHand;
        _gun1InHand = false;
        _plGun2 = _gun2.GetComponent<PlayerGun>();
        _plGun1.enabled = true;
        _plGun2.enabled = false;
        _activeGun = _plGun1;              
    }

    // Update is called once per frame
    void Update()
    {
        _moveDir = new Vector3(Input.GetAxis("Horizontal"), 0,  Input.GetAxis("Vertical"));

        if (_moveDir != Vector3.zero)
        {
            _playerAudio.PlayStep();
        }
        else
        {
            _playerAudio.StopStep();
        }

        _moveDir = this.transform.TransformVector(_moveDir);

        if (_characterController.isGrounded)
        {
            _playerMovement.Movement(speedMovement, gravitation, _moveDir);           
            _playerAnimation.RunAnimation(_moveDir);
            _playerMovement.MouseRotation(speedRotation);
            _inFly = false;
        }
        else if (_inFly==false)
        {
            _playerMovement.Movement(speedMovement, gravitation, Vector3.zero);
          //  _playerAnimation.Falling();
            _inFly = true;
        }
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            SwitchWeapon();
        }
        if (Input.GetMouseButton(0))
        {
            _activeGun.needFire = true;           
        }
        else
        {
            _activeGun.needFire = false;
        }
    }

    private void SwitchWeapon()
    {
        if (_gun1InHand == true)
        {
            _gun1.transform.position = rifleNotHand.transform.position;
            _gun1.transform.rotation = rifleNotHand.transform.rotation;
            _gun1.transform.parent = rifleNotHand.transform;
            _gun2.transform.position = rifleInHand.transform.position;
            _gun2.transform.rotation = rifleInHand.transform.rotation;
            _gun2.transform.parent = rifleInHand.transform;
            _gun1InHand = false;
            _plGun1.enabled = false;
            _plGun2.enabled = true;
            _activeGun = _plGun2;
            _activeGun.CheckBullet();
        }
        else
        {
            _gun1.transform.position = rifleInHand.transform.position;
            _gun1.transform.rotation = rifleInHand.transform.rotation;
            _gun1.transform.parent = rifleInHand.transform;
            _gun2.transform.position = rifleNotHand.transform.position;
            _gun2.transform.rotation = rifleNotHand.transform.rotation;
            _gun2.transform.parent = rifleNotHand.transform;
            _gun1InHand = true;
            _plGun1.enabled = true;
            _plGun2.enabled = false;
            _activeGun = _plGun1;
            _activeGun.CheckBullet();
        }
    }
}
