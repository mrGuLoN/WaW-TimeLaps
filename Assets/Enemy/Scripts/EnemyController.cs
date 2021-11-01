using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{

    [SerializeField] private Vector2 speedMinMax, rotationMinMax;
    [SerializeField] private float damage, distanceAttack, distanceJump;    
    [SerializeField] private bool jump;
    [SerializeField] private GameObject[] damageSphere;
    [SerializeField] private Transform rayCastPoint;
    
    
    
    private GameObject _player;
    private Vector3 _target, _direction, _currentDirection, _movement;
    private float _realSpeed;
    private float _realRoundSpeed;
    private CharacterController _characterController;
    private Animator _animator;
    private bool _run, _firstAttack, _inAttack;
    private float _speedAnimation;
   
    

    void Start()
    {
        _player = GameObject.FindGameObjectWithTag("Player");
        _realSpeed = Random.Range(speedMinMax.x, speedMinMax.y);
        _realRoundSpeed = Random.Range(rotationMinMax.x, rotationMinMax.y);
        _characterController = GetComponent<CharacterController>();
        _animator = GetComponent<Animator>();
        _animator.SetBool("Run", false);
        _run = false;
        _speedAnimation = _realSpeed/3;
        _animator.speed = _speedAnimation;        
        _firstAttack = jump;
        _inAttack = false;
        for (int i =0; i<damageSphere.Length;i++)
        {
            damageSphere[i].GetComponent<DamageSphere>().damage = damage;
            damageSphere[i].SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
       
        if (_run == true && _inAttack == false)
        {            
            Movement();
            Rotation();            
        }
        else if (Input.GetMouseButton(0) || Input.GetMouseButton(1))
        {
            _run = true;
            _animator.SetBool("Run", true);
        }      

        Attack();      
    }

    private void Rotation()
    {
        _direction = new Vector3 (_target.x, this.transform.position.y, _target.z) - this.transform.position;
        _currentDirection = Vector3.Lerp(_currentDirection, _direction, _realRoundSpeed * Time.deltaTime);
        this.transform.forward = _currentDirection;
    }

    private void Movement()
    {
        _target = new Vector3(_player.transform.position.x, this.transform.position.y, _player.transform.position.z);
        _movement = new Vector3(0, -10, 0) + this.transform.forward * _realSpeed;
        _characterController.Move(_movement * Time.deltaTime);
    }

    private void Attack()
    {
        RaycastHit hit;
        if (Physics.Raycast(rayCastPoint.position, rayCastPoint.forward, out hit, distanceAttack))
        {
            if (hit.transform.CompareTag("Player"))
            {
                _inAttack = true;
                _animator.SetTrigger("Attack");
                for (int i = 0; i < damageSphere.Length; i++)
                {
                    damageSphere[i].SetActive(true);
                }
                StartCoroutine(TimeOut());
            }            
        }               
    }

   IEnumerator TimeOut()
   {
        yield return new WaitForSeconds(2);
        _inAttack = false;
        for (int i = 0; i < damageSphere.Length; i++)
        {
            damageSphere[i].SetActive(false);
        }
   }

}
