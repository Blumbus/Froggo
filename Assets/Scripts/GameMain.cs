using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR.ARFoundation;

public class GameMain : MonoBehaviour
{

    public static GameMain instance;
    public bool running = false;
    public int numRows = 2;
    public Text gameText;
    public Text gameCount;
    public GameObject boatSpawner;
    public GameObject lilypad;
    public GameObject finishLine;
    public ARPlaneManager planeManager;
    public GameObject ammo;
    public ARPointCloudManager pointManager;
    public float groundHeight = 0f;
    public GameObject water;
    public GameObject arrow;
    public Vector2 gameDir;
    public Vector3 gameOrigin;
    private List<BoatSpawner> spawners = new List<BoatSpawner>();
    public int numPads = 20;

    private void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        Player.instance.alive = true;
        running = false;
        gameText.text = "Calibrating...Please look around at the ground";
        gameCount.gameObject.SetActive(false);
        ammo.SetActive(false);
        numPads = 20;
        StartCoroutine(Configure());
    }

    private IEnumerator Configure()
    {
        water.SetActive(false);
        arrow.SetActive(false);
        while (planeManager.trackables.count < 1)
        {
            yield return new WaitForSeconds(0.1f);
        }
        yield return new WaitForSeconds(1f);
        arrow.SetActive(true);
        gameText.text = "Point the phone in the direction of gameplay";
        StartCoroutine(Countdown(5));
        yield return new WaitForSeconds(5f);
        planeManager.detectionMode = UnityEngine.XR.ARSubsystems.PlaneDetectionMode.None;
        foreach (UnityEngine.XR.ARFoundation.ARPlane p in planeManager.trackables)
        {
            float h = p.center.y;
            if (h < groundHeight)
            {
                groundHeight = h;
            }
        }
        BeginGame();
    }

    private IEnumerator Countdown(int t)
    {
        gameCount.gameObject.SetActive(true);
        for (int i = t; i > 0; i--)
        {
            gameCount.text = i.ToString();
            float cum = 1;
            while (cum > 0)
            {
                gameCount.fontSize = (int) (Mathf.Lerp(100f, 20f, cum));
                cum -= Time.deltaTime;
                yield return new WaitForEndOfFrame();
            }
        }
        gameCount.gameObject.SetActive(false);
    }

    private void BeginGame()
    {
        water.transform.position = new Vector3(0f, groundHeight - 1f, 0f);
        foreach (UnityEngine.XR.ARFoundation.ARPlane p in planeManager.trackables)
        {
            p.gameObject.SetActive(false);
        }
        water.SetActive(true);
        arrow.SetActive(false);
        ammo.SetActive(true);
        UpdatePads();
        gameText.text = "";
        Vector3 camDir = Camera.main.transform.rotation * Vector3.forward;
        gameDir = new Vector3(camDir.x, camDir.z).normalized;
        gameOrigin = new Vector3(Camera.main.transform.position.x, groundHeight, Camera.main.transform.position.z);
        GameObject lily = Instantiate(lilypad);
        lily.transform.position = gameOrigin;

        Vector2 initialOffset = gameDir * 6f;
        Vector2 perOffset = gameDir * 5f;
        for (int i = 0; i < numRows; i++)
        {
            Vector2 thisOffset = perOffset * i;
            Vector3 pos = gameOrigin + new Vector3(initialOffset.x + thisOffset.x, 0f, initialOffset.y + thisOffset.y);
            GameObject inst = Instantiate(boatSpawner);
            inst.transform.position = pos;
            BoatSpawner sp = inst.GetComponent<BoatSpawner>();
            Vector2 perp = Vector2.Perpendicular(gameDir);
            if (i % 2 == 0)
            {
                perp = -1 * perp;
            }
            sp.dir = perp;
            sp.speed = 3f + 0.2f * i;
            sp.rate = 0.2f + 0.09f * i;
            spawners.Add(sp);
        }
        GameObject finInst = Instantiate(finishLine);
        Vector2 finOffset = perOffset * numRows;
        Vector3 finPos = gameOrigin + new Vector3(initialOffset.x + finOffset.x, 0f, initialOffset.y + finOffset.y);
        finInst.transform.position = finPos;
        Vector3 dir3 = new Vector3(gameDir.x, 0f, gameDir.y);
        finInst.transform.rotation = Quaternion.LookRotation(dir3, Vector3.up);
        running = true;
        StartCoroutine(BoatRoutine());
    }

    public void UpdatePads()
    {
        ammo.transform.Find("AmmoText").GetComponent<Text>().text = "x" + numPads.ToString();
    }

    private IEnumerator BoatRoutine()
    {
        while (true)
        {
            if (running)
            {
                foreach (BoatSpawner sp in spawners)
                {
                    sp.TrySpawn();
                }
            }
            yield return new WaitForSeconds(4f);
        }
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
