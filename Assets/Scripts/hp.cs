using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class hp : MonoBehaviour
{
    [SerializeField] private float maxHealth;
    public float health;
    [SerializeField] private bool player;

    public delegate void ZombieDeath();
    public static event ZombieDeath zombieDeath;
    public delegate void HowManyZombies();
    public static event HowManyZombies howManyZombies;

    private Animator _ani;
    private NewEnemyController _enemyController;
    private CharacterController _characterController;
    private PlayerData _playerController;
    private Rigidbody[] _rb;
    private bool _firstDead = true;
    void Start()
    {
        health = maxHealth;
        _ani = GetComponent<Animator>();
        _characterController = GetComponent<CharacterController>();
        if (player == false)
        {
            _enemyController = GetComponent<NewEnemyController>();
        }
        else
        {
            _playerController = GetComponent<PlayerData>();
        }
        _rb = GetComponentsInChildren<Rigidbody>();
       
        foreach (Rigidbody rb in _rb)
        {            
            rb.isKinematic = true;           
        }
       
        if (player == false)
        {
            howManyZombies();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (health <= 0 && _firstDead == true)
        {
            _firstDead = false;
            _ani.enabled = false;
            _characterController.enabled = false;
            if (player == false)
            {
                _enemyController._enemySound.Dead();
                _enemyController.enabled = false;                
                zombieDeath();
            }
            else 
            {
                _playerController.enabled = false;            
            }
            foreach (Rigidbody rb in _rb)
            {                
                rb.isKinematic = false;                
            }
        }
    }

   
}
