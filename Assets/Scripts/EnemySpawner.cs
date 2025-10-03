using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemyType1;
    public GameObject enemyType2;
    public GameObject enemyType3;
    void Start()
    {
        int probability = Random.Range(1,9);
        if (probability <4&& probability>0)
        {
            Instantiate(enemyType1,transform.position,transform.rotation);
        }else if (probability >=4 & probability<=6)
        {
            Instantiate(enemyType2, transform.position, transform.rotation);
        }
        else if(probability >= 7 & probability <= 9)
        {
            Instantiate(enemyType3, transform.position, transform.rotation);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
