using UnityEngine;

public class Goal : MonoBehaviour
{

    [SerializeField] private MouseControll mouseControll;
    //セーブするスコア
    private int score;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            //スコア計算
            float power = mouseControll.CurrentPower;
            score = (int)power;
            //スコアをセーブ
            PlayerPrefs.SetInt("score", score);

            Debug.Log("クリア");
        }
    }
}
