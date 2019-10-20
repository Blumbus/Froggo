using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrownCheck : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    

    private bool LilyCheck()
    {
        RaycastHit[] hits;
        hits = Physics.SphereCastAll(transform.position, 0.5f, -Vector3.up, 70f);

        for (int i = 0; i < hits.Length; i++)
        {
            RaycastHit hit = hits[i];
            GameObject hob = hit.collider.gameObject;
            if (hob.tag == "Lily")
            {
                return true;
            }
        }
        return false;
    }
    private void Drown()
    {
        Player.instance.Kill();
    }

    float offTime = 0f;
    // Update is called once per frame
    void Update()
    {
        if (GameMain.instance.running)
        {
            bool lil = LilyCheck();
            if (lil)
            {
                offTime = 0f;
            }
            else
            {
                offTime += Time.deltaTime;
                if (offTime > 0.29f)
                {
                    if (Player.instance.alive)
                    {
                        Drown();
                    }
                }
            }
        }
    }
}
