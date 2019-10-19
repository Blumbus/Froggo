using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR.ARFoundation;

public class GameMain : MonoBehaviour
{

    public static GameMain instance;
    public bool running = false;
    public Text gameText;
    public ARPlaneManager planeManager;
    public float groundHeight = 0f;
    public GameObject water;

    private void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        running = false;
        gameText.text = "Configuring...\nPlease look around at the ground";
        StartCoroutine(Configure());
    }

    private IEnumerator Configure()
    {
        while (planeManager.trackables.count < 1)
        {
            yield return new WaitForSeconds(0.1f);
        }
        gameText.text = planeManager.trackables.count.ToString();
        yield return new WaitForSeconds(4f);
        gameText.text = planeManager.trackables.count.ToString();
        planeManager.detectionMode = UnityEngine.XR.ARSubsystems.PlaneDetectionMode.None;
        foreach (UnityEngine.XR.ARFoundation.ARPlane p in planeManager.trackables)
        {
            float h = p.center.y;
            if (h < groundHeight)
            {
                groundHeight = h;
            }
        }
        gameText.text = groundHeight.ToString();
        running = true;
        BeginGame();
    }

    private void BeginGame()
    {
        water.transform.position = new Vector3(0f, groundHeight, 0f);
        //StartCoroutine(FadeWaterIn(1f, 0f, 0.8f));
    }

    private IEnumerator FadeWaterIn(float t, float minA, float maxA)
    {
        Material mat = water.GetComponent<Renderer>().material;
        float elapsed = 0f;
        while (elapsed < t)
        {
            Color c = mat.color;
            c.a = Mathf.Lerp(minA, maxA, elapsed / t);
            mat.color = c;
            elapsed += Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }
        Color fullC = mat.color;
        fullC.a = maxA;
        mat.color = fullC;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
