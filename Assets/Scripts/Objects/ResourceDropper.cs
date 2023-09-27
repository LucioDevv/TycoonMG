using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceDropper : MonoBehaviour
{
    public GameObject[] resources;
    public float spawnTime;

    public int dropperTier;


    // Start is called before the first frame update
    void Start()
    {
        dropperTier = 1;
        InvokeRepeating("DropResource", spawnTime, 1f);
    }

    // Update is called once per frame
    void DropResource()
    {
        if(resources[dropperTier - 1] != null || dropperTier <= resources.Length)
        {
            Instantiate(resources[dropperTier - 1], transform.position, Quaternion.identity);
        }

    }

    public void UpgradeDropper()
    {
        dropperTier++;
    }
}
