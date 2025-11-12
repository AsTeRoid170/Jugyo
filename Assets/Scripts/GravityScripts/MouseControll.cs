using UnityEngine;

public class MouseControll : MonoBehaviour
{
    private Vector2 startPoint;
    private Vector2 endPoint;
    private bool isDragging = false;

    // LineRenderer
    public LineRenderer lineRenderer;

    // プレハブ(インスペクターで割り当て)
    public GameObject GravityField;

    // Update is called once per frame
    void Update()
    {
        // 始点設定
        if (Input.GetMouseButtonDown(0))
        {
            startPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            isDragging = true;
            lineRenderer.enabled = true;// / 描画も有効化
        }
        //　ドラッグ中は終点を更新
        if (Input.GetMouseButton(0)&&isDragging)
        {
            endPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            DrawRectangle();
        }

        // 左クリックを離したらドラッグ終了
        if (Input.GetMouseButtonUp(0))
        {
            if (isDragging)
            {
                CreateGrivetyField();
                CancelRectangle();//画像挿入後、線は消す
            }
            isDragging = false;

        }
        // 右クリックで削除（ドラッグ中でも後でも
        if (Input.GetMouseButtonDown(1))
        {
            CancelRectangle();
            return;// 押してる間の誤作動防止（将来のこの下に拡張する場合に備えて）
        }
    }

    // 描画（線、面ではない）
    void DrawRectangle()
    {
        //残りの2点を計算
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

    void CancelRectangle()
    {
        if(lineRenderer != null)
        {
            lineRenderer.positionCount = 0; // 頂点を消す
            lineRenderer.enabled = false; // 描画も非表示に
        }
        isDragging = false; //　状態リセット
    }

    void CreateGrivetyField()
    {
        if(GravityField == null)
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



        //スケールを調整（プレハブの基準サイズが1×１と仮定）
        field.transform.localScale = new Vector3(width, height, 1f);
    }
}
