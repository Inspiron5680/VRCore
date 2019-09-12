using UnityEngine;
using UniRx.Triggers;
using UniRx;

/// <summary>
/// 離れたオブジェクトを操作するレーザーを作り出すクラス
/// </summary>
public class LaserBeam : MonoBehaviour
{
    readonly float RAY_LENGTH = 1f;
    /// <summary>
    /// レーザーの先に存在するGameObect
    /// </summary>
    public ILaserSelectReceiver Target { get; private set; }

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

        this.UpdateAsObservable()
            .Where(_ => Target != null)
            .Where(_ => OVRInput.GetDown(OVRInput.RawButton.RIndexTrigger) || OVRInput.GetDown(OVRInput.RawButton.LIndexTrigger))
            .Subscribe(_ => Target.LaserSelectReceiver());
    }

    void Update()
    {
        if (!lineRenderer || !anchor)
        {
            Debug.LogError("LeaserBeam properties are not set.");
            return;
        }

        Ray ray = new Ray(anchor.position, anchor.forward);
        RaycastHit hit;
        if(Physics.Raycast(ray,out hit, RAY_LENGTH,rayExclusionLayers))
        {
            Target = hit.transform.GetComponent<ILaserSelectReceiver>();
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
