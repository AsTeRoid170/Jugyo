using UnityEngine;

public class GameEnd : MonoBehaviour
{
    public void QuitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false; // エディタで停止
#else
        Application.Quit(); // ビルド版でアプリ終了
#endif
    }
}
