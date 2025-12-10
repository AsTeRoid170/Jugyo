using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleSceneController : MonoBehaviour
{
    // スタートボタンが押されたときに呼ばれる関数
    public void OnStartButton()
    {
        // ステージ選択シーンへ移動
        SceneManager.LoadScene("stageselect");
    }


    // 終了ボタンなどを付けたい場合
    public void OnExitButton()
    {
        Application.Quit();

        // Unityエディタ上で停止したい場合
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }  
}