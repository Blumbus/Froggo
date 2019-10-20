using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoatSpawner : MonoBehaviour
{

    public List<GameObject> prefabs;
    public float rate = 0.2f;
    public float speed = 2f;
    public List<GameObject> boats = new List<GameObject>();
    public float offset = 40f;
    public Vector2 dir;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ManualSpawn()
    {
        int nPs = prefabs.Count;
        int i = Random.Range(0, nPs);
        GameObject inst = Instantiate(prefabs[i]);
        Vector3 dir3 = new Vector3(dir.x, 0f, dir.y);
        inst.transform.position = transform.position + (-dir3 * offset);
        inst.GetComponent<Boat>().velocity = dir.normalized * speed;
        inst.transform.rotation = Quaternion.LookRotation(dir3, Vector3.up);
        boats.Add(inst);
    }

    public void TrySpawn()
    {
        if (Random.value < rate)
        {
            ManualSpawn();
        }

        int i = 0;
        while (i < boats.Count)
        {
            GameObject b = boats[i];
            if (Vector3.Distance(b.transform.position, transform.position) > offset + 1)
            {
                Destroy(b);
                boats.RemoveAt(i);
            }
            else
            {
                i++;
            }
        }
    }
}
