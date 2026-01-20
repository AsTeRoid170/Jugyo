using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class BackTitle : MonoBehaviour
{

    private void Start()
    {
        // このオブジェクトについている Button を取得してクリック時の処理を登録
        Button button = GetComponent<Button>();
        button.onClick.AddListener(OnClickStart);
    }

    // スタートボタンが押されたときに呼ばれる
    private void OnClickStart()
    {
        SceneManager.LoadScene("GameTitleScene");
    }
}
