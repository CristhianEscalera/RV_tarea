using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnController : MonoBehaviour
{
    public GameObject Coins;
    void Start()
    {
        int probability = Random.Range(1,6);
        if (probability < 4&& probability>0)
        {
            Instantiate(Coins, transform.position, transform.rotation);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
