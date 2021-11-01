using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageMaker : MonoBehaviour
{
    
    [SerializeField] private GameObject[] stagePrefabs;
    public GameObject door;

    private bool firstEnter, _deadEndNeed;
    private int stageInt;
    private GameObject stagePooler;
    private GameObject _deadEnd;

   
    void Start()
    {
        stagePooler = GameObject.FindGameObjectWithTag("StagePrefabs");
        stagePrefabs = stagePooler.GetComponent<StagePrefabs>().stagePrefabs;
        stageInt = Random.Range(0, stagePrefabs.Length);
        firstEnter = true;
        _deadEnd = stagePooler.GetComponent<StagePrefabs>().deadEnd;
        _deadEndNeed = false; 
    }

    // Update is called once per frame
    void Update()
    {
        Debug.DrawRay(this.transform.position + new Vector3(0, 1, 0), this.transform.forward, Color.red, 25f);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Doors"))
        {
            Destroy(other.transform.GetComponent<StageMaker>().door);
            Destroy(other.transform.gameObject);
            door.GetComponent<Animator>().SetBool("character_nearby", true);
            Destroy(this.gameObject);
        }
        else if (other.CompareTag("Player"))
        {
            RaycastHit hit;
            if (Physics.Raycast(this.transform.position + new Vector3(0, 1, 0), this.transform.forward, out hit, 12f))
            {               
                    firstEnter = false;
                    Instantiate(_deadEnd, this.transform.position, this.transform.rotation);
                    door.GetComponent<Animator>().SetBool("character_nearby", true);              
            }           
            else
            {
                firstEnter = false;                          
                Instantiate(stagePrefabs[stageInt], this.transform.position, this.transform.rotation);
                door.GetComponent<Animator>().SetBool("character_nearby", true);
            }          
            Destroy(this.gameObject);
        }  
        
    }

  
}
