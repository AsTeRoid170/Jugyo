using UnityEngine;
using UnityEngine.UI;

public class GravityMeter_Mask : MonoBehaviour
{
    [SerializeField] private RectMask2D target_rectMask;

    public float speed = 10f; // 1秒あたり増える値（px）
    private float maxWidth;
    private float downWidth;

    void Start()
    {
        // 同じGameObjectにアタッチされているMaskを取得
        target_rectMask = GetComponent<RectMask2D>();

        // RectTransform の幅を上限値として記録
        maxWidth = target_rectMask.rectTransform.rect.width;

        // 初期状態（全部表示）
        target_rectMask.padding = new Vector4(0, 0, 0, 0);

        downWidth = 0;
    }

    private void FixedUpdate()
    {
        
        Vector4 pad = target_rectMask.padding;
        pad.z =Mathf.Clamp(pad.z + Time.deltaTime * downWidth, 0, downWidth);

        target_rectMask.padding = pad;

        Debug.Log(pad.x);
        
    }

    public void MeterDown(float DownEnergy)
    {
        /*
        Vector4 pad = target_rectMask.padding;
        pad.z = Mathf.Clamp(pad.z + DownEnergy, 0, maxWidth);
        target_rectMask.padding = pad;
        */
        downWidth = downWidth + DownEnergy;
    }
}
