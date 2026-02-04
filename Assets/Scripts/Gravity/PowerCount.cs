using TMPro;
using UnityEngine;

public class PowerCount : MonoBehaviour
{
    [SerializeField] private MouseControll mouseControll; // 参照をもらう
    [SerializeField] private TMP_Text powerText;          // 表示するテキスト
    private TMP_Text limitText;

    void Update()
    {
        if (mouseControll == null || powerText == null) return;

 
        float power = mouseControll.CurrentPower;
        float limit = mouseControll.currentCreateLimitTimer;
        //powerText.text = $"Power : {Mathf.RoundToInt(power)}";
        powerText.text = $"limit : {Mathf.RoundToInt(limit)}";
    }
}
