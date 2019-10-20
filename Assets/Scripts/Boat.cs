using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boat : MonoBehaviour
{

    public Vector2 velocity;

    // Update is called once per frame
    void Update()
    {
        transform.position += new Vector3(velocity.x, 0f, velocity.y) * Time.deltaTime;
    }

    private void OnTriggerEnter(Collider other)
    {
        Player p = other.gameObject.GetComponent<Player>();
        if (p != null)
        {
            p.Kill();
        }
    }
}
