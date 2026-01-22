using UnityEngine;

public class FallManager : MonoBehaviour
{
    [Header("プレイヤーのワープ先")]
    [SerializeField]Transform respawnPoint; // ワープ先

    void OnTriggerEnter2D(Collider2D t)
    {
        if (t.CompareTag("Player"))
        {
            // プレイヤーをワープさせる
            t.transform.position = respawnPoint.position;
        }
    }
}
