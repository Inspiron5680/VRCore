using UnityEngine;

/// <summary>
/// レーザーポインターの選択受付サンプルクラス
/// </summary>
public class VRUI : MonoBehaviour, ILaserSelectReceiver
{
    float TRANSLUCENT = 0.5f;
    float OPACITY = 1f;
    Material material;
    bool isTranslucent;

    void Start()
    {
        Initialize();
    }

    /// <summary>
    /// VRUI初期化
    /// </summary>
    public void Initialize()
    {
        material = this.GetComponent<MeshRenderer>().material;
        changeTransparency(true);
    }

    /// <summary>
    /// レーザーポインター選択受付
    /// </summary>
    public void LaserSelectReceiver()
    {
        changeTransparency(!isTranslucent);
    }

    void changeTransparency(bool doMakeTranslucent)
    {
        var color = material.color;
        if (doMakeTranslucent)
        {
            material.color = new Color(color.r, color.g, color.b, TRANSLUCENT);
        }
        else
        {
            material.color = new Color(color.r, color.g, color.b, OPACITY);
        }

        isTranslucent = doMakeTranslucent;
    }
}
