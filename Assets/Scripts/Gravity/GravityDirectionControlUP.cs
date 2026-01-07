using UnityEngine;

public class GravityDirectionControlUP : MonoBehaviour
{
    // このフィールドが与える重力の向き（インスペクタで設定）
    private Vector2 fieldGravityDirection = new Vector2(0, 1);

    private void OnTriggerEnter2D(Collider2D other)
    {
        // Player に触れたときだけ処理
        if (other.CompareTag("Player"))
        {
            playerMove pm = other.GetComponent<playerMove>();
            if (pm != null)
            {
                pm.SetGravityDirection(fieldGravityDirection);
            }
        }
    }

    // フィールドから出たときに元の重力に戻したい場合
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerMove pm = other.GetComponent<playerMove>();
            if (pm != null)
            {
                // 例: 画面下方向に戻す
                pm.SetGravityDirection(new Vector2(0, -1));
            }
        }
    }
}
