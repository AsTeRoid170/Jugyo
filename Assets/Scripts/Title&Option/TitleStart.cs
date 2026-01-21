using UnityEngine;
using UnityEngine.SceneManagement;  
using UnityEngine.UI;              

public class TitleStart : MonoBehaviour
{

    void Start()
    {
        // このオブジェクトについている Button を取得してクリック時の処理を登録
        Button button = GetComponent<Button>();
        button.onClick.AddListener(OnClickStart);
    }

    void Awake()
    {
        if (TryGetComponent(out Button button))
        {
            button.onClick.AddListener(OnClickStart);
        }
        else
        {
            Debug.LogError("Button コンポーネントが見つかりません");
        }
    }


    // スタートボタンが押されたときに呼ばれる
    public void OnClickStart()
    {

        SceneManager.LoadScene("SelectScene");
        Debug.Log("LoadScene呼ばれた");
    }
}
