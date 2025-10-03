using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapController : MonoBehaviour
{
    public GameObject map; // Este debe ser un PREFAB, no un objeto de escena
    public float spaceMap = 1.15f;
    public bool generate = false;

    void Start()
    {
        if (map == null)
        {
            Debug.LogError("El prefab 'map' no está asignado en MapController");
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            if (!generate)
            {
                Instantiate(map, new Vector3(transform.position.x + spaceMap, 0, 10), transform.rotation);
                generate = true;
            }
        }
    }
}
