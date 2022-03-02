using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeTest : MonoBehaviour
{


    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyUp(KeyCode.E))
        {
            if (gameObject.tag == "Key")
            {
                Debug.Log(gameObject.name);
                Destroy(gameObject);
            }
        }
    }


}

