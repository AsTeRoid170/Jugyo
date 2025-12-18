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

    public float area => width * height;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
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

    void Awake()
    {
        sr = GetComponent<SpriteRenderer>();
    }

    public void SetDirectionLeft()
    {
        if (sr != null && leftSprite != null)
        {
            sr.sprite = leftSprite;
        }
    }

    public void SetDirectionRight()
    {
        if (sr != null && rightSprite != null)
        {
            sr.sprite = rightSprite;
        }
    }

    public void SetDirectionUp()
    {
        if (sr != null && upSprite != null)
        {
            sr.sprite = upSprite;
        }
    }

    public void SetDirectionDown()
    {
        if (sr != null && downSprite != null)
        {
            sr.sprite = downSprite;
        }
    }
}
