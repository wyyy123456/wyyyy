using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnWater : MonoBehaviour
{
    public GameObject waterSphere;
    public Transform bottleTrans;
    //public bool isSpawning = false;
    public void SpawnSphere()
    {
        //isSpawning = true;
        StartCoroutine(StartSpawn());
    }

    public void StopSpawnSphere()
    {
        StopCoroutine(StartSpawn());
    }

    IEnumerator StartSpawn()
    {
        while (true)
        {
            Instantiate(waterSphere, bottleTrans);
            yield return new WaitForSeconds(0.25f);
        }
        
    }
}
