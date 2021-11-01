using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WorldController : MonoBehaviour
{
    [SerializeField] private Text bulletText;
    [SerializeField] private Text zombieText;   


    private int needToKill = 0;
    

    // Start is called before the first frame update

    private void Awake()
    {              
        hp.howManyZombies += HowManyZombies;
        hp.zombieDeath += ZombieDeath;
        PlayerGun.bulletCheck += BulletInMagazine;        
    }
    void Start()
    {
      
    }

    // Update is called once per frame
    void Update()
    {
       
       
    }

    void ZombieDeath()
    {
        needToKill--;
        zombieText.text = needToKill.ToString();
    }

    void HowManyZombies()
    {
        needToKill++;
        zombieText.text = needToKill.ToString();
    }

    void BulletInMagazine(int bullet, int magazine)
    {
        bulletText.text = bullet.ToString() + "/" + magazine.ToString();
        
    }   
}
