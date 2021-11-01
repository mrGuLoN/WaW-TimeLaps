using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewEnemyController : MonoBehaviour
{
    [SerializeField] private Vector2 speedMinMax, rotationMinMax;
    [SerializeField] private float damage, distanceAttack, timeAnimationAttack;
    [SerializeField] private GameObject[] damageSphere;

    private SearchTarget _searchTarget;
    private MovementAndRotation _movementAndRotation;
    public EnemySound _enemySound;

    private float _realSpeed, _realSpeedRotation;
    private GameObject _target;
    private bool _run, _inAttack;
    private Animator _animator;
    // Start is called before the first frame update
    void Awake()
    {
        _searchTarget = GetComponent<SearchTarget>();
        _movementAndRotation = GetComponent<MovementAndRotation>();
        _animator = GetComponent<Animator>();
        _enemySound = GetComponent<EnemySound>();
        _realSpeed = Random.Range(speedMinMax.x, speedMinMax.y);
        _realSpeedRotation = Random.Range(rotationMinMax.x, rotationMinMax.y);
        _searchTarget.SearchPlayer(out _target);
        _animator.speed = _realSpeed / 3;
        _animator.SetBool("Run", false);
        _run = false;
        _inAttack = false;
        for (int i = 0; i < damageSphere.Length; i++)
        {
            damageSphere[i].GetComponent<DamageSphere>().damage = damage;
            damageSphere[i].SetActive(false);
        }
        _enemySound.Pitch(_realSpeed, speedMinMax);
    }

    private void Start()
    {
        _enemySound.Stay();
    }
    // Update is called once per frame
    void Update()
    {
        if (_run == true && _inAttack == false)
        {
            _movementAndRotation.Rotation(_target, _realSpeedRotation);
            _movementAndRotation.Movement(_realSpeed);
            _animator.SetBool("Attack", false);
            Attack();
        }
        else if (Input.GetMouseButton(0) || Input.GetMouseButton(1))
        {
            _run = true;
            _animator.SetBool("Run", true);
            _enemySound.Run();
        }        
    }

    private void Attack()
    {
        RaycastHit hit;
        if (Physics.Raycast(new Vector3(transform.position.x, transform.position.y+0.8f, transform.position.z), transform.forward, out hit, distanceAttack))
        {
            if (hit.transform.CompareTag("Player"))
            {
                _inAttack = true;
                _animator.SetBool("Attack", true);
                for (int i = 0; i < damageSphere.Length; i++)
                {
                    damageSphere[i].SetActive(true);
                }
                StartCoroutine(TimeOut());
            }
            else
            {
                _animator.SetBool("Attack", false);
                for (int i = 0; i < damageSphere.Length; i++)
                {
                    damageSphere[i].SetActive(false);
                    _animator.SetBool("Attack", false);
                }
            }
        }
    }
    IEnumerator TimeOut()
    {
        yield return new WaitForSeconds(timeAnimationAttack/(4*3)+0.3f);
        _inAttack = false;
        for (int i = 0; i < damageSphere.Length; i++)
        {
            damageSphere[i].SetActive(false);
        }
    }
}
