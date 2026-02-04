using UnityEngine;
using UnityEngine.UI;

public class CreateLimitMeter_Mask : MonoBehaviour
{
    [SerializeField] private RectMask2D target_rectMask;

    //ゲージの最大幅
    private float maxWidth;
    //ゲージの増減する幅を管理する
    private float changeWidth;
    //ゲージの増減する速度
    private float changeSpeed;
    //DownEnergyの補正値
    private float correctionvalue = 16f;
    //ゲージの増減を判定
    private bool changeMode = true;


    void Start()
    {
        // 同じGameObjectにアタッチされているMaskを取得
        target_rectMask = GetComponent<RectMask2D>();

        // ゲージの最大幅の設定
        maxWidth = 160;

        // 初期状態（全部表示）
        target_rectMask.padding = new Vector4(0, 0, 0, 0);

        changeWidth = maxWidth;
        changeSpeed= maxWidth/10;
    }

    void Update()
    {
        
        Vector4 pad = target_rectMask.padding;
        if (changeMode == true)
        {
            pad.x = Mathf.Clamp(pad.x + Time.deltaTime*changeSpeed, changeWidth, maxWidth);
        }
        else if(changeMode == false){
            //ゲージの描画制限(ゲージが減らす処理)
            pad.x = Mathf.Clamp(pad.x - changeSpeed, changeWidth, maxWidth);
        }
        target_rectMask.padding = pad;
        Debug.Log(changeWidth);

    }

    public void TimerUp(float time)
    {
        changeMode = true;
        changeWidth = time*correctionvalue;
        if (changeWidth>maxWidth)
        {
            changeWidth = maxWidth;
        }
    }
    public void TimerDown(float time)
    {
        changeMode = false;
        changeWidth= time*correctionvalue;
        if (changeWidth<0)
        {
            changeWidth = 0;
        }
    }
}
