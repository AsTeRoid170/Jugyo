using UnityEngine;
using System.Collections.Generic;
using Unity.VisualScripting;

public class MouseControll : MonoBehaviour
{
    private Vector2 startPoint;
    private Vector2 endPoint;
    private bool isDragging = false;

    // LineRenderer
    public LineRenderer lineRenderer;

    // プレハブ(インスペクターで割り当て)
    public GameObject GravityField;

    // 作成した GravityField 管理（FIFO）
    private Queue<GameObject> filedQueue = new Queue<GameObject>();
    private const int maxFieldCount = 2;

    // Update is called once per frame
    void Update()
    {
        // 始点設定
        if (Input.GetMouseButtonDown(0))
        {
            startPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            isDragging = true;
            lineRenderer.enabled = true;// 描画も有効化
        }
        // ドラッグ中は終点を更新
        if (Input.GetMouseButton(0)&&isDragging)
        {
            endPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            DrawRectangle();
        }

        // 左クリック終了(確定)
        if (Input.GetMouseButtonUp(0) && isDragging)
        {
            isDragging = false;
            CreateGravityField(); // 範囲フィット生成
            lineRenderer.enabled = false; // 線は自動で消す
        }
        // 右クリックで削除（キャンセル or FIFO削除）
        if (Input.GetMouseButtonDown(1))
        {
            RightClickDelete();
        }
       
    }

    // 描画（線、面ではない）
    void DrawRectangle()
    {
        // 残りの2点を計算
        Vector2 p1 = startPoint;
        Vector2 p2 = new Vector2(startPoint.x, endPoint.y);
        Vector2 p3 = endPoint;
        Vector2 p4 = new Vector2(endPoint.x, startPoint.y);

        // LineRendrerの頂点にセット
        if (lineRenderer != null)
        {
            lineRenderer.positionCount = 5;// 閉じた長方形のため５点
            lineRenderer.SetPosition(0, p1);
            lineRenderer.SetPosition(1, p2);
            lineRenderer.SetPosition(2, p3);
            lineRenderer.SetPosition(3, p4);
            lineRenderer.SetPosition(4, p1);// 閉じる
        }
    }

    // 枠線を元に画像を作成,FIFO管理
    void CreateGravityField()
    {
        // 画像がなかった場合に警告
        if (GravityField == null)
        {
            Debug.LogWarning("GravityField prefab not assigend!!");
            return;
        }

        // 中心座標を計算
        Vector2 center = (startPoint + endPoint) / 2f;

        // 幅と高さを算出
        float width = Mathf.Abs(endPoint.x - startPoint.x);
        float height = Mathf.Abs(endPoint.y - startPoint.y);

        // プレハブ生成
        GameObject field = Instantiate(GravityField, center, Quaternion.identity);

        // GravityField.csを参照(2025//11/19 に追加)
        GravityField areaInfo = field.AddComponent<GravityField>();
        areaInfo.width = width;
        areaInfo.height = height;


        // SpriteRendrer取得
        SpriteRenderer sr = field.GetComponent<SpriteRenderer>();
        if (sr != null)
        {
            // 現在のSpriteのサイズ
            Vector2 spriteSize = sr.bounds.size;

            // scale補正係数
            float scaleX = width / spriteSize.x;
            float scaleY = height / spriteSize.y;

            // 適用
            field.transform.localScale = new Vector3(scaleX, scaleY , 1f);
        }
        else
        {
            // spriteRendererがない場合は通常通り
            // スケールを調整（プレハブの基準サイズが1×１と仮定）
            field.transform.localScale = new Vector3(width, height, 1f);
        }

        // 画像の面積を生成直後に取得
        float area = areaInfo.area;
        Debug.Log(" 生成した重力場の面積 :" + area);


        // FIFO上限管理
        filedQueue.Enqueue(field);

        // 上限こえたら古いほうを削除
        if (filedQueue.Count > maxFieldCount)
        {
            GameObject oldField = filedQueue.Dequeue();
            Destroy(oldField);
        }


    }
    // 枠線を削除
    void RightClickDelete()
    {
        if (isDragging)
        {
            //  生成途中（手動キャンセル、枠線だけ削除）
            // lineRenderer.positionCount = 0; // 頂点を消す
            isDragging = false; //　状態リセット
            lineRenderer.enabled = false; // 描画も非表示に
            return;
        }

        // 生成済みGravityFieldをFIFOで削除
        if (filedQueue.Count > 0)
        {
            GameObject oldField = filedQueue.Dequeue();

            // 削除する重力場の面積を求める
            var area = oldField.GetComponent<GravityField>();
            if(area != null)
            {
                Debug.Log("削除する重力場の面積 = " + area.area);
            }

            Destroy(oldField);

            // 上で求めた面積の半分を四捨五入してエネルギーに足す

        }
       
    }
}
