using UnityEngine;
using UnityEngine.UI;

public class EmptyMeter_Mask : MonoBehaviour
{
    [SerializeField] private RectMask2D target_rectMask;

    //ゲージの最大幅
    private float maxWidth;
    //ゲージを減らす幅を管理する
    private float downWidth;
    //ゲージの減る速度
    private float downSpeed = 50f;
    //DownEnergyの補正値
    private float correctionvalue = 1.5f;


    void Start()
    {
        // 同じGameObjectにアタッチされているMaskを取得
        target_rectMask = GetComponent<RectMask2D>();

        // RectTransform の幅を上限値として記録
        maxWidth = target_rectMask.rectTransform.rect.width;

        // 初期状態（全部表示）
        target_rectMask.padding = new Vector4(0, 0, 0, 0);

        downWidth = maxWidth;
    }

    private void FixedUpdate()
    {
        
        Vector4 pad = target_rectMask.padding;
        Debug.Log(pad.x);
        //ゲージの描画制限(ゲージが減らす処理)
        pad.x =Mathf.Clamp(pad.x - Time.deltaTime * downSpeed, downWidth, maxWidth);

        target_rectMask.padding = pad;

    }

    public void MeterDown(float DownEnergy)
    {
        /*
        Vector4 pad = target_rectMask.padding;
        pad.z = Mathf.Clamp(pad.z + DownEnergy, 0, maxWidth);
        target_rectMask.padding = pad;
        */
        DownEnergy = DownEnergy * correctionvalue;
        downWidth = downWidth - DownEnergy;
        Debug.Log(downWidth);
    }
}
