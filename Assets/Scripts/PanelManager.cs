using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PanelManager : MonoBehaviour
{

    public static PanelManager instance;
    public GameObject winPanel;
    public GameObject losePanel;
    public GameObject currPanel;

    private void Awake()
    {
        instance = this;
    }

    public void ShowPanel(string pType)
    {
        GameObject panel;
        if (pType == "win")
        {
            panel = winPanel;
        }
        else
        {
            panel = losePanel;
        }
        Destroy(currPanel);
        GameObject inst = Instantiate(panel);
        inst.transform.SetParent(this.transform);
        inst.GetComponent<RectTransform>().offsetMin = Vector2.zero;
        inst.GetComponent<RectTransform>().offsetMax = Vector2.zero;
        currPanel = inst;

    }
}
