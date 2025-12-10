using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GravityField : MonoBehaviour
{
    public float width;
    public float height;

    // 自動削除までの残り時間（秒）
    private float lifeTime;

    public MouseControll mouseControll;


    public float area => width * height;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // 面積/２秒に四捨五入した時間だけ生存
        lifeTime = Mathf.Round(area / 2f);        

        //ログ
        //Debug.Log($"重力場生成: 面積={area:F1}, 生存時間={lifeTime}秒");
    }

    // Update is called once per frame
    void Update()
    {
        lifeTime -= Time.deltaTime;
        if(lifeTime <= 0f)
        {
            mouseControll.CountDown();
            // 削除時のログ出力
            //Debug.Log($"重力場自動削除: 面積={area:F1}, 生存時間終了");
            Destroy(gameObject);
        }
    }
}
