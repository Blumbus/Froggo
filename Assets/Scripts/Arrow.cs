using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour
{



    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Transform cam = Camera.main.transform;
        transform.position = cam.transform.position + new Vector3(0f, -1.4f, 0f) + cam.transform.forward * 1.4f;
        Vector3 camEul = cam.transform.eulerAngles;
        transform.rotation = Quaternion.Euler(0f, camEul.y, 0f);
    }
}
