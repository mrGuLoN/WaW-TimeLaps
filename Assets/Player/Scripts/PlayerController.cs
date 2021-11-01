using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float speedRotation;
    [SerializeField] private float speedRun;
    [SerializeField] private float gravity;
    [SerializeField] private float punchInSec;
    [SerializeField] private float damagePunch;
    [SerializeField] private float distancePunch;
    [SerializeField] private float boxPunchSize;
    [SerializeField] private PollerObject.ObjectInfo.ObjectType blood;
    [SerializeField] private Transform rightFoot;
    [SerializeField] private GameObject rifleInHand, rifleNotHand;
    public GameObject gun1, gun2;
    
    public int weapon;

    private CharacterController _characterController;
    private Camera _cam;
    public Animator _animator;
    private Vector3 _screenMousePosition, _direction, _worldMousePos, _currenDirection;
    private Vector3 _moveDir, _animatorMoveDir;
    private float _punchTimeOut, _speedPunchAnimation;
    private float _curenTime;
    private bool _inHit, _firstPunch, _gun1InHand;
    private GameObject _gun1, _gun2;

   
    void Awake()
    {
        _cam = Camera.main;
        _characterController = GetComponent<CharacterController>();
        _animator = GetComponent<Animator>();
        weapon = 2;
        _punchTimeOut = 1 / punchInSec;
        _curenTime = _punchTimeOut;
        _speedPunchAnimation = 2.677f / _punchTimeOut;
        _animator.SetFloat("SpeedPunch",_speedPunchAnimation);
        _inHit = false;
        _firstPunch = true;
        _gun1 = Instantiate(gun1, rifleInHand.transform.position, rifleInHand.transform.rotation);
        _gun1.transform.parent = rifleInHand.transform;
        _gun2 = Instantiate(gun2, rifleNotHand.transform.position, rifleNotHand.transform.rotation);
        _gun2.transform.parent = rifleNotHand.transform;
        _gun1InHand = true;
    }

   
    void Update()
    {
        _moveDir = new Vector3 (Input.GetAxis("Vertical"), 0, -1* Input.GetAxis("Horizontal")) ;       
        if (_curenTime >= _punchTimeOut && Input.GetMouseButtonDown(1) && _inHit == false)
        {
            _curenTime = 0;
            _animator.SetTrigger("Punch");
            _inHit = true;
            Punch();
        }
        else if (_inHit == true)
        {
            Punch();
        }
        else
        {
            _curenTime += Time.deltaTime;
        }

        if (_inHit == false)
        {
            MouseRotation();
            Movement();
            Animation();
        }
        if (Input.GetKeyDown(KeyCode.Tab))
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
            }
            else
            {
                _gun1.transform.position = rifleInHand.transform.position;
                _gun1.transform.rotation = rifleInHand.transform.rotation;
                _gun1.transform.parent = rifleInHand.transform;
                _gun2.transform.position = rifleNotHand.transform.position;
                _gun2.transform.rotation = rifleNotHand.transform.rotation;
                _gun2.transform.parent = rifleNotHand.transform;
                _gun1InHand=true;
            }

        }
    }

    private void MouseRotation()
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

    private void Movement()
    {
        _moveDir = _cam.transform.InverseTransformVector(_moveDir);
        _moveDir *= speedRun;
        _moveDir.y = 0;
        _moveDir.y -= gravity*Time.deltaTime;
        _characterController.Move(_moveDir * Time.deltaTime);
    }

    private void Animation()
    {
        _animatorMoveDir = transform.InverseTransformVector(_moveDir)/speedRun;       
        _animator.SetFloat("x", _animatorMoveDir.x);
        _animator.SetFloat("y", _animatorMoveDir.z);       
    }    

    private void Punch()
    {        
        RaycastHit[] hit = Physics.SphereCastAll(rightFoot.position, boxPunchSize, Vector3.forward, distancePunch);
        for (int i = 0; i < hit.Length; i++)
        {
            if (hit[i].transform.CompareTag("Enemy"))
            {
                hit[i].collider.GetComponent<hp>().health -= damagePunch;
                var bloodPoint = PollerObject.Instance.GetObject(blood);
                bloodPoint.transform.position =rightFoot.position;
            }
        }
        if (_firstPunch == true)
        {
            StartCoroutine(PunchTimer());
            _firstPunch = false;
        }
    }
   

    IEnumerator PunchTimer()
    {
        yield return new WaitForSeconds(2.677f/_speedPunchAnimation);
        _inHit = false;
        _firstPunch = true;
    }
}
