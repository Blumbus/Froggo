using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

[RequireComponent(typeof(ARRaycastManager))]
public class LilypadPlace : MonoBehaviour
{
    public GameObject placedPrefab;
    public GameObject targetPrefab;

    /// <summary>
    /// The object instantiated as a result of a successful raycast intersection with a plane.
    /// </summary>
    public List<GameObject> spawnedObjects = new List<GameObject>();
    public GameObject target { get; private set; }

    void Awake()
    {
        m_RaycastManager = GetComponent<ARRaycastManager>();
    }

    bool TryGetTouchPosition(out Vector2 touchPosition)
    {
#if UNITY_EDITOR
        if (Input.GetMouseButton(0))
        {
            var mousePosition = Input.mousePosition;
            touchPosition = new Vector2(mousePosition.x, mousePosition.y);
            return true;
        }
#else
        if (Input.touchCount > 0)
        {
            touchPosition = Input.GetTouch(0).position;
            return true;
        }
#endif

        touchPosition = default;
        return false;
    }

    void Update()
    {
        if (GameMain.instance.running)
        {
            Ray ray = Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
            Plane p = new Plane(Vector3.up, new Vector3(0f, GameMain.instance.groundHeight, 0f));
            float rayOut;
            if (p.Raycast(ray, out rayOut))
            {
                Vector3 hitPos = ray.GetPoint(rayOut);

                if (target == null)
                {
                    target = Instantiate(targetPrefab);
                }
                target.transform.position = hitPos + new Vector3(0f, -0.01f, 0f);

                Touch touch = Input.GetTouch(0);
                if (touch.phase == TouchPhase.Began)
                {
                    GameObject pad = Instantiate(placedPrefab, hitPos, Quaternion.identity);
                    spawnedObjects.Add(pad);
                }
            }
        }
    }

    static List<ARRaycastHit> s_Hits = new List<ARRaycastHit>();

    ARRaycastManager m_RaycastManager;
}
