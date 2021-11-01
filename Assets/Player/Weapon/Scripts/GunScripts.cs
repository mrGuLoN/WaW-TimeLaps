using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunScripts : MonoBehaviour
{
    [SerializeField] private Transform gunPoint;
    [SerializeField] private bool automatic;
    [SerializeField] private PollerObject.ObjectInfo.ObjectType bullet;
    [SerializeField] private GameObject fire;
    [SerializeField] private float reloadTime;

    public delegate void BulletCheck(int  bullet, int magazine);
    public static event BulletCheck bulletCheck;  

    public float bulletInSec;
    public float damage;
    public float bulletSpeed;
    public int magazineAmmo;
    public int maxSizeAmmo;

    private bool _firstOffFire;
    private float _curentTime;
    private float _timeOut;
    private int _bulletInMagazine;
    private bool _reload;
    private GameObject _player;
    private PlayerController _playerController;

    // Start is called before the first frame update
    void Start()
    {
        _player = GameObject.FindGameObjectWithTag("Player");
        _playerController = _player.GetComponent<PlayerController>();
        _timeOut = 1 / bulletInSec;
        _curentTime = _timeOut;
        _playerController._animator.SetBool("RifleShoot", false);
        fire.SetActive(false);
        _firstOffFire = true;
        _bulletInMagazine = magazineAmmo;
        _reload = false;
        bulletCheck(_bulletInMagazine, magazineAmmo);
    }

    // Update is called once per frame
    void Update()
    {
        if (automatic == false && Input.GetMouseButtonDown(0) && _curentTime >= _timeOut && _reload == false)
        {
            Fire();                   
        }
        else if (automatic == true && Input.GetMouseButton(0) && _curentTime >= _timeOut && _reload == false)
        {
            Fire();           
        }       
        else if (_reload == false)
        {
            _curentTime += Time.deltaTime;
            _playerController._animator.SetBool("RifleShoot", false);
            fire.SetActive(false);
            if (_firstOffFire == true)
            {
                _firstOffFire = false;
                StartCoroutine(FireOff());
            }
        }
      

        if ((_bulletInMagazine <= 0 || Input.GetKeyDown(KeyCode.R))&& _reload == false)
        {
            Reloading();
        }
    }

    private void Fire()
    {
        _curentTime = 0;
        _bulletInMagazine--;
        var bulletFire = PollerObject.Instance.GetObject(bullet);
        bulletFire.transform.position = gunPoint.position;
        bulletFire.transform.up = -1* gunPoint.forward;
        bulletFire.GetComponent<Bullet>().speed = bulletSpeed;
        bulletFire.GetComponent<Bullet>().damage = damage;
        fire.SetActive(true);
        _playerController._animator.SetBool("RifleShoot", true);
        bulletCheck(_bulletInMagazine, magazineAmmo);
    }

    private void Reloading()
    {        
        _reload = true;
        fire.SetActive(false);
        _firstOffFire = true;
        _playerController._animator.SetTrigger("Reload Rifle");
        _curentTime = _timeOut;
        StartCoroutine(Reload());
        _playerController._animator.SetBool("RifleShoot", false);
    }

    IEnumerator FireOff()
    {
        yield return new WaitForSeconds(_timeOut / 2);
        fire.SetActive(false);
        _firstOffFire = true;
    }

    IEnumerator Reload()
    {
        yield return new WaitForSeconds(reloadTime);
        _reload = false;
        _bulletInMagazine = magazineAmmo;
        bulletCheck(_bulletInMagazine, magazineAmmo);
    }
}
