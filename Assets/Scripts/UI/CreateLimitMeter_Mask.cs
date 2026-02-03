using UnityEngine;
using UnityEngine.UI;

public class CreateLimitMeter_Maskk : MonoBehaviour
{
    [SerializeField] private RectMask2D target_rectMask;

    //ゲージの最大幅
    private float maxWidth;
    //ゲージを減らす幅を管理する
    private float downWidth;
    //ゲージの減る速度
    private float downSpeed = 50f;
    //DownEnergyの補正値
    private float correctionvalue = 1.7f;


    void Start()
    {
        // 同じGameObjectにアタッチされているMaskを取得
        target_rectMask = GetComponent<RectMask2D>();

        // ゲージの最大幅の設定
        maxWidth = 180;

        // 初期状態（全部表示）
        target_rectMask.padding = new Vector4(0, 0, 0, 0);

        downWidth = maxWidth;
    }

    private void FixedUpdate()
    {
        
        Vector4 pad = target_rectMask.padding;
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
    }
}
