using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGun : MonoBehaviour
{
    public float bulletInSec, damage, bulletSpeed, scater;
    public int magazineSize, allBullet;
    [SerializeField] bool shotgun;
    [SerializeField] private Transform gunPoint;
    [SerializeField] private GameObject fire;
    [SerializeField] private PollerObject.ObjectInfo.ObjectType bullet;
    [SerializeField] private float _timeFireAnimation, _timeReloadAnimation;
    [SerializeField] private AudioSource shotGunEnject, reloadWeapon;

    public int pellet;
    public bool needFire;

    public delegate void BulletCheck(int bullet, int magazine);
    public static event BulletCheck bulletCheck;

    public delegate void ShotGun (float speedAnimation);
    public static event ShotGun shotGunAnimation;

    public delegate void AllWeapon(float speedReload);
    public static event AllWeapon reload;

    private float _timeOut, _currentTime, _fireFloat, _reloadFloat;
    private float _randHor, _randVer;
    private int _bulletInMagazine;
    private bool _reload;
   

    // Start is called before the first frame update
    void Start()
    {
        if (shotgun == false)
        {
            pellet = 1;
        }
        _timeOut = 1 / bulletInSec;
        _currentTime = _timeOut;
        _bulletInMagazine = magazineSize;
        needFire = false;
        _reload = false;
        fire.SetActive(false);
        bulletCheck(_bulletInMagazine, allBullet);
        _fireFloat = 1 * bulletInSec * _timeFireAnimation;       
    }

    // Update is called once per frame
    void Update()
    {
        _currentTime += Time.deltaTime;
        if (_bulletInMagazine <=0)
        {
            Reload();
        }
        else if (_currentTime >= _timeOut && needFire == true && shotgun == false && _reload == false)
        {
            RifleFire();
        }
        else if (_currentTime >= _timeOut && needFire == true && shotgun == true && _reload == false)
        {
            ShotGunFire();           
        }
        else 
        {
            fire.SetActive(false);
        }       
    }

    private void RifleFire()
    {
        _randHor = Random.Range((-scater - 1) / 180, (scater + 1) / 180);
        _randVer = Random.Range((-scater - 1) / 180, (scater + 1) / 180);
        _currentTime = 0;
        _bulletInMagazine--;            
        var bulletFire = PollerObject.Instance.GetObject(bullet);
        bulletFire.transform.position = gunPoint.position;
        bulletFire.transform.forward = gunPoint.forward + this.transform.TransformVector(new Vector3(_randHor, _randVer, 0));
        bulletFire.GetComponent<Bullet>().speed = bulletSpeed;
        bulletFire.GetComponent<Bullet>().damage = damage;
        fire.SetActive(true);
        bulletCheck(_bulletInMagazine, allBullet);
    }

    private void ShotGunFire()
    {
        _currentTime = 0;
        _bulletInMagazine--;
        shotGunEnject.pitch = _fireFloat;
        shotGunEnject.Play();

        for (int i = 0; i < pellet; i++)
        {
            _randHor = Random.Range((-scater-1)/180, (scater + 1)/180);
            _randVer = Random.Range((-scater/2 - 1) / 180, (scater/2 + 1) / 180);
            var bulletFire = PollerObject.Instance.GetObject(bullet);
            bulletFire.transform.position = gunPoint.position;
            bulletFire.transform.forward = gunPoint.forward + this.transform.TransformVector(new Vector3 (_randHor, _randVer, 0));
            bulletFire.GetComponent<Bullet>().speed = bulletSpeed;
            bulletFire.GetComponent<Bullet>().damage = damage;
            fire.SetActive(true);                  
        }
        bulletCheck(_bulletInMagazine, allBullet);
        shotGunAnimation(_fireFloat + 0.5f);
    }

    private void Reload()
    {
        if (allBullet - magazineSize > 0)
        {
            _bulletInMagazine = magazineSize;
            allBullet -= magazineSize;
        }
        else
        {
            _bulletInMagazine = allBullet;
            allBullet = 0;
        }
        _reload = true;
        reload(1);
        if (shotGunEnject != null)
        {
            shotGunEnject.Stop();
        }
        reloadWeapon.Play();
        StartCoroutine(TimerReload());
    }

    public void CheckBullet()
    {
        bulletCheck(_bulletInMagazine, allBullet);
    }
    
    IEnumerator TimerReload()
    {
        yield return new WaitForSeconds(_timeReloadAnimation+(_timeFireAnimation/_fireFloat));
        _reload = false;
        bulletCheck(_bulletInMagazine, allBullet);
    }
}
