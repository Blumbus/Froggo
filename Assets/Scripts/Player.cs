using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    public static Player instance;
    public bool alive = true;

    private void Awake()
    {
        instance = this;
    }

    public void Kill()
    {
        alive = false;
        PanelManager.instance.ShowPanel("lose");
        GameMain.instance.running = false;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
