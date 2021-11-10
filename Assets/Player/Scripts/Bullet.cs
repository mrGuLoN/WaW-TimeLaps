using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour, IPooledObjects
{
    
    public float speed;
    public float damage;
    [SerializeField] private PollerObject.ObjectInfo.ObjectType blood, rebound;
    [SerializeField] private float bulletPower;

   

    public PollerObject.ObjectInfo.ObjectType Type => type;
    [SerializeField] private PollerObject.ObjectInfo.ObjectType type;
    private float _currentTime;

    void Start()
    {
        _currentTime = 0;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        transform.Translate(Vector3.forward * Time.deltaTime * speed);
        _currentTime += Time.deltaTime;
        RaycastHit hit;
        Ray ray = new Ray(this.transform.position, this.transform.forward);
        if (_currentTime >= 1)
        {
            _currentTime = 0;
            PollerObject.Instance.DestroyGameObject(this.gameObject);
        }
        else if (Physics.Raycast(ray, out hit, 0.2f))
        {
            if (hit.collider.CompareTag("Enemy") || hit.collider.CompareTag("Player"))
            {
                hit.collider.GetComponent<hp>().health -= damage;
                var bulletFire = PollerObject.Instance.GetObject(blood);
                bulletFire.transform.position = this.transform.position;                       
            }
            else
            {
                var bulletFire = PollerObject.Instance.GetObject(rebound);
                bulletFire.transform.position = this.transform.position;               
            }
            PollerObject.Instance.DestroyGameObject(this.gameObject);
        }
    }

}
