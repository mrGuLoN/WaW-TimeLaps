using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RespawnZombi : MonoBehaviour
{
    [SerializeField] private int minZombi, maxZombi;
    [SerializeField] private PollerObject.ObjectInfo.ObjectType typeZombi;
    [SerializeField] private Animator door;
    [SerializeField] private float roundZombiResp;
    [SerializeField] private Transform myTransform;

    private int howManyZombi;
    private float xZomby, zZombi;
    private bool needZombi;
    void Start()
    {
        howManyZombi = Random.Range(minZombi, maxZombi + 1);
        if (door != null)
        {
            door.SetBool("character_nearby", true);
        }
        this.transform.localScale = new Vector3 (2*roundZombiResp, 1, 2*roundZombiResp);
        needZombi = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (needZombi == true)
        {
            for (int i = 0; i <= howManyZombi; i++)
            {
                xZomby = transform.position.x + Random.Range(-roundZombiResp, roundZombiResp);
                zZombi = transform.position.z + Random.Range(-roundZombiResp, roundZombiResp);
                var zombi = PollerObject.Instance.GetObject(typeZombi);
                zombi.transform.position = myTransform.position+new Vector3 (xZomby,0,zZombi);
                Debug.Log(zombi.transform.position);
            }
            needZombi = false;
        }
    }
}
