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

    // このフィールドが与える重力の向き（インスペクタで設定）
    [SerializeField]Vector2 fieldGravityDirection = new Vector2(0, -1 * 2);

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

    private void OnTriggerStay2D(Collider2D other)
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

        if (other.CompareTag("MoveObject"))
        {
            ObjectMove pm = other.GetComponent<ObjectMove>();
            if (pm != null)
            {
                pm.SetGravityDirection(fieldGravityDirection);
            }
        }
    }

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

        if (other.CompareTag("MoveObject"))
        {
            ObjectMove pm = other.GetComponent<ObjectMove>();
            if (pm != null)
            {
                // 例: 画面下方向に戻す
                pm.SetGravityDirection(new Vector2(0, -1));
            }
        }
    }




}//GravityField


