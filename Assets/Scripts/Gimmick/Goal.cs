using UnityEngine;

public class Goal : MonoBehaviour
{

    private MouseControll mouseControll;
    //セーブするスコア
    private int score;

    private void Start()
    {
        GameObject obj = GameObject.Find("MouseControllSystem");
        if (obj != null)
        {
            mouseControll = obj.GetComponent<MouseControll>();
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            //スコア計算
            float power = mouseControll.CurrentPower;
            score = (int)power;
            //スコアをセーブ
            //PlayerPrefs.SetInt("score", score);

            Debug.Log("クリア");
        }
    }
}
