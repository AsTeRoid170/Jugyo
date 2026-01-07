using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GravityField : MonoBehaviour
{
    public float width;
    public float height;

    // 向き別の Sprite をインスペクターで設定
    public Sprite upSprite;
    public Sprite downSprite;
    public Sprite leftSprite;
    public Sprite rightSprite;

    private SpriteRenderer sr;

    // 自動削除までの残り時間（秒）
    [SerializeField] float lifeTime;

    private MouseControll mouseControll;
    private float defG = 9.81f;

    //どの方向の重力場なのか判別する
    [SerializeField]int gravityDirection;

    public float area => width * height;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //初期化
        //gravityDirection = 0;

        // 面積/２秒に四捨五入した時間だけ生存
        lifeTime = Mathf.Round(area / 2f);

        // 探索
        GameObject obj = GameObject.Find("MouseControllSystem");
        if (obj != null)
        {
            mouseControll = obj.GetComponent<MouseControll>();
        }

        //ログ
        //Debug.Log($"重力場生成: 面積={area:F1}, 生存時間={lifeTime}秒");
    }

    // Update is called once per frame
    void Update()
    {

        lifeTime -= Time.deltaTime;
        if(lifeTime <= 0f)
        {
            if (mouseControll != null)
            {
                mouseControll.CountDown();
            }

            // 削除時のログ出力
            //Debug.Log($"重力場自動削除: 面積={area:F1}, 生存時間終了");
            Destroy(gameObject);
        }

    }

    /*private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Debug.Log("Playerが重力場に入った！");
            // ここにコメント表示処理（UIテキスト更新など）を書く

            switch (gravityDirection)
            {
                
                case 1:
                    Physics2D.gravity = new Vector2(-defG/2, 0f);
                    break;
                case 2:
                    Physics2D.gravity = new Vector2(defG/2, 0f);
                    break;
                case 3:
                    Debug.Log("重力変更　上");
                    //Physics.gravity = new Vector3(0, 9.81f, 0);
                    Physics2D.gravity = new Vector2(0f, defG);
                    break;
                case 4:
                    Physics2D.gravity = new Vector2(0f, -defG*2);
                    break;
                
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Debug.Log("Playerが重力場を離れた！");
            // ここにコメント表示処理（UIテキスト更新など）を書く

            //Playerの重力方向を元に戻す
            Physics2D.gravity = new Vector2(0f, -9.81f);
        }
    }*/

   

}//GravityField


