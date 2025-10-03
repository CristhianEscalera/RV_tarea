using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public float velocityEnemy = 1f;
    public Transform player;
    void Start()
    {
        if (player == null)
        {
            GameObject playerObj = GameObject.FindWithTag("Player");
            if (playerObj != null)
            {
                player = playerObj.transform;
            }
            else
            {
                Debug.LogError("No se encontró el objeto con la etiqueta 'Player'");
            }
        }
    }


    // Update is called once per frame
    void Update()
    {
        if(player == null) return;

        transform.position = Vector2.MoveTowards(
            transform.position,
            new Vector2(player.position.x, transform.position.y),
            velocityEnemy * Time.deltaTime
        );
    }
}
