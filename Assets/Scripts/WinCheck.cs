using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinCheck : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        Player p = other.gameObject.GetComponent<Player>();
        if (p != null)
        {
            PanelManager.instance.ShowPanel("win");
            GameMain.instance.running = false;
        }
    }
}
