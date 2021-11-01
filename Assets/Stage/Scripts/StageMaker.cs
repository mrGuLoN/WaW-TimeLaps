using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageMaker : MonoBehaviour
{
    
    [SerializeField] private GameObject[] stagePrefabs;
    [SerializeField] private Animator door;

    private bool firstEnter;
    private int stageInt;
    private GameObject stagePooler;

   
    void Start()
    {
        stagePooler = GameObject.FindGameObjectWithTag("StagePrefabs");
        stagePrefabs = stagePooler.GetComponent<StagePrefabs>().stagePrefabs;
    }

    // Update is called once per frame
    void Update()
    {
        firstEnter = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (firstEnter == true)
            {
                firstEnter = false;
                stageInt = Random.Range(0, stagePrefabs.Length);                
                Instantiate(stagePrefabs[stageInt], this.transform.position, this.transform.rotation);
                door.SetBool("character_nearby", true);
            }
            else 
            {
                door.SetBool("character_nearby", true);
            }
        }
    }

  
}
