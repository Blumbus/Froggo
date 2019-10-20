using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinCheck : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        PanelManager.instance.ShowPanel("win");
    }
}
