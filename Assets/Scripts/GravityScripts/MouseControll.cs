using UnityEngine;

public class MouseControll : MonoBehaviour
{
    private Vector2 startPoint;
    private Vector2 endPoint;
    private bool isDragging = false;

    public LineRenderer lineRenderer;

    // Update is called once per frame
    void Update()
    {
        // 始点設定
        if (Input.GetMouseButtonDown(0))
        {
            startPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            isDragging = true;
        }
        //　ドラッグ中は終点を更新
        if (Input.GetMouseButton(0)&&isDragging)
        {
            endPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            DrawRectangle();

            
        }

        // マウスを離したらドラッグ終了
        if (Input.GetMouseButtonUp(0))
        {
            isDragging = false;
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

    void deleteLine()
    {

    }
}
