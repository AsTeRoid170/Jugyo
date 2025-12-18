using TMPro;
using UnityEngine;

public class PowerCount : MonoBehaviour
{
    [SerializeField] private MouseControll mouseControll; // 参照をもらう
    [SerializeField] private TMP_Text powerText;          // 表示するテキスト

    void Update()
    {
        if (mouseControll == null || powerText == null) return;

        // プロパティ名は CurrentPower / 変数名は currentPower に合わせて変更
        float power = mouseControll.CurrentPower;
        powerText.text = $"Power : {Mathf.RoundToInt(power)}";
    }
}
