using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBody : MonoBehaviour
{

    public Camera cam;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 eulers = transform.rotation.eulerAngles;
        transform.rotation = Quaternion.Euler(eulers.x, cam.transform.rotation.eulerAngles.x, eulers.z);
    }
}
