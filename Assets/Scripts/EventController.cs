using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EventController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyUp(KeyCode.R)&&GameObject.FindGameObjectWithTag("Player")==null)
        {
            SceneManager.LoadScene("SampleScene");
        }
    }
}
