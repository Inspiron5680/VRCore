using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserBeam : MonoBehaviour
{
    float DEFAULT_LASER_LENGTH = 0.5f;
    float RAY_LENGTH = 1f;
    bool isEnable;
    /// <summary>
    /// レーザーの先に存在するGameObect
    /// </summary>
    public GameObject Target { get; private set; }

    [SerializeField] LayerMask rayExclusionLayers;
    [SerializeField] Transform anchor;
    [SerializeField] LineRenderer lineRenderer;

    void Start()
    {
        Initialize();
    }

    /// <summary>
    /// LaserBeam初期化関数
    /// </summary>
    public void Initialize()
    {
        if (lineRenderer == null)
        {
            lineRenderer = GetComponent<LineRenderer>();
        }

        isEnable = true;
    }

    void Update()
    {
        if (!isEnable || !anchor)
        {
            Debug.LogError("LeaserBeam properties are not set.");
            return;
        }

        Ray ray = new Ray(anchor.position, anchor.forward);
        RaycastHit hit;
        if(Physics.Raycast(ray,out hit, RAY_LENGTH,rayExclusionLayers))
        {
            Target = hit.transform.gameObject;
            drawBeam(hit.point);
            return;
        }

        Target = null;
        lineRenderer.enabled = false;
    }

    void drawBeam(Vector3 position)
    {
        lineRenderer.enabled = true;
        lineRenderer.SetPosition(0, anchor.position);
        lineRenderer.SetPosition(1, position);
    }
}
