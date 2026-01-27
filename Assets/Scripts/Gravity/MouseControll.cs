using UnityEngine;
using System.Collections.Generic;
using Unity.VisualScripting;

public class MouseControll : MonoBehaviour
{
    private Vector2 startPoint;
    private Vector2 endPoint;
    private bool isDragging = false;

    // カーソル関連
    public Texture2D cursorYes;
    public Texture2D cursorNo;
    public Texture2D cursorDefault;
    // 現在のカーソルがどちらか
    private bool isYesCursor = true;
    //　現在のマウスのモードがどちらか
    [SerializeField] bool mouseMode = true;
    //  重力場の個数が最大数かどうか trueの場合、最大数に達している
    public bool checkMaxField = false;
    // LineRenderer
    public LineRenderer lineRenderer;

    // プレハブ(インスペクターで割り当て)
    public GameObject GravityField;



    // 作成した GravityField 管理（FIFO）
    //private Queue<GameObject> fieldQueue = new Queue<GameObject>();
    // Queue => Listに変更
    private List<GameObject> fieldList = new List<GameObject>();
    //現在の生成されている重力場の数
    private int currentList = 0;
    //現在の生成に必要なパワー
    private float currentPower;
    private const int maxFieldCount = 2;
    //生成に必要なパワーの初期値
    private float maxPower = 100;
    public float CurrentPower => currentPower;

    public GameObject DirectionBotton;

    // 方向別プレハブを Inspector から割り当て
    public GameObject prefabUp;
    public GameObject prefabDown;
    public GameObject prefabLeft;
    public GameObject prefabRight;

    // 追加：最後に生成した GravityField への参照
    public GameObject LastCreatedField { get; private set; }


    // 方向ごとに対応するプレハブを返す
    GameObject GetPrefabByDirection(string dir)
    {
        switch (dir)
        {
            case "Up": return prefabUp;
            case "Down": return prefabDown;
            case "Left": return prefabLeft;
            case "Right": return prefabRight;
            default: return GravityField;   // デフォルト
        }
    }


    private void Start()
    {
        // 方向指定ボタンUIを非表示
        DirectionBotton.SetActive(false);

        // 初期化
        currentPower = maxPower;

    }

    // Update is called once per frame
    void Update()
    {

        // カーソル 毎フレーム判定
        CursorState();
        //Debug.Log(fieldList.Count);
        // 範囲指定モード
        if (mouseMode == true) {

            if (checkMaxField==false)
            {
                // 始点設定
                if (Input.GetMouseButtonDown(0))
                {
                    startPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                    isDragging = true;
                    lineRenderer.enabled = true;// 描画も有効化
                } //if
                  // ドラッグ中は終点を更新
                if (Input.GetMouseButton(0) && isDragging)
                {
                    //時よとまれー
                    Time.timeScale = 0.05f;
                    endPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                    DrawRectangle();
                }// if
                else
                {
                    Time.timeScale = 1;
                }

                // 左クリック終了(確定)
                if (Input.GetMouseButtonUp(0) && isDragging)
                {
                    Time.timeScale = 1;
                    isDragging = false;
                    CreateGravityField(); // 範囲フィット生成
                    lineRenderer.enabled = false; // 線は自動で消す
                }// if
            }


            // 右クリックで削除（キャンセル or FIFO削除）
            if (Input.GetMouseButtonDown(1))
            {
                RightClickDelete();
            }// if

        }//if

        // 方向指定モード
        if (mouseMode == false)
        {
            Time.timeScale = 0;
            // 右クリックで作成をキャンセル
            if (Input.GetMouseButtonDown(1))
            {
                RightClickDelete();
                ModeChange();
                Time.timeScale = 1;
            }
        }

    }// Update
    private void FixedUpdate()
    {

        // 12/18に直す箇所（時間が早すぎるのでタイマーに）
        currentPower++;
        if (currentPower > maxPower)
        {
            currentPower = maxPower;
        }
    }

    // 生成可能かどうか見る関数
    bool CanCreateSilent()
    {
        // 幅と高さを算出
        float width = Mathf.Abs(endPoint.x - startPoint.x);
        float height = Mathf.Abs(endPoint.y - startPoint.y);

        // 面積
        float area = width * height;
        float limitArea = currentPower / 2;

        // 条件
        if (width < 1f || height < 1f) return false;
        if (area < 1f) return false;
        if (width > 10f || height > 10f) return false;
        if (area > 25f) return false;
        if (area > limitArea) return false;

        return true;

    }// CanCreateSilent

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
        }// if
    } // DrawRectangle

    // nullを除去するヘルパー関数（任意で作成）
    void CleanUpFieldList()
    {
        fieldList.RemoveAll(item => item == null);
    }

    // 枠線を元に画像を作成,FIFO管理
    void CreateGravityField()
    {
        // 画像がなかった場合に警告
        if (GravityField == null)
        {
            Debug.LogWarning("GravityField prefab not assigend!!");
            return;
        }// if

        // 中心座標を計算
        Vector2 center = (startPoint + endPoint) / 2f;

        // 幅と高さを算出
        float width = Mathf.Abs(endPoint.x - startPoint.x);
        float height = Mathf.Abs(endPoint.y - startPoint.y);

        // 面積
        float area = width * height;

        if (width < 1f || height < 1f)//最小サイズチェック 1cm 未満
        {
            //Debug.Log("タテ、ヨコのどちらかが短すぎるッピ！");
            lineRenderer.enabled = false;// 枠線を消す
            isDragging = false;// 状態リセット
            return; //生成を中止
        }// if

        if (area < 1f)// 最小サイズチェック 1cm^2未満
        {
            //Debug.Log("画像が小さすぎるッピ！");
            lineRenderer.enabled = false;// 枠線を消す
            isDragging = false;// 状態リセット
            return; //生成を中止
        }// if

        if (width > 10f || height > 10f)// 最大サイズチェック 10cm 超過
        {
            //Debug.Log("タテ、ヨコのどちらかが長すぎるッピ！");
            lineRenderer.enabled = false;// 枠線を消す
            isDragging = false;// 状態リセット
            return; //生成を中止
        }// if

        if (area > 25f)// 最大面積チェック 25cm^2 超過
        {
            //Debug.Log("画像がデカすぎるッピ！");
            lineRenderer.enabled = false;// 枠線を消す
            isDragging = false;// 状態リセット
            return; //生成を中止
        }// if

        if (currentPower < area * 4)
        {
            lineRenderer.enabled = false;// 枠線を消す
            isDragging = false;// 状態リセット
            return; //生成を中止
        }//if

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
            field.transform.localScale = new Vector3(scaleX, scaleY, 1f);
            //field.transform.localScale = new Vector3(scaleX*0.28f, scaleY*0.28f, 1f);

            // 現在の重力場の生成数のカウントアップ
            CountUp();
            ModeChange();

        }
        else
        {
            // spriteRendererがない場合は通常通り
            // スケールを調整（プレハブの基準サイズが1×１と仮定）
            field.transform.localScale = new Vector3(width, height, 1f);
        }// if

        // 画像の面積を生成直後にログに取得
        //Debug.Log(" 生成した重力場の面積 :" + area);


        CleanUpFieldList();

        // 最新から削除
        fieldList.Add(field);
        LastCreatedField = field;

        //  ２つを超えたら「一番新しいもの」を削除（大きい古いものは残る）
        while (fieldList.Count > maxFieldCount)
        {
            int lastIndex = fieldList.Count - 1;
            GameObject candidate = fieldList[lastIndex];
            fieldList.RemoveAt(lastIndex);

            // すでに時間切れでDestory済みの場合はnullをスキップ
            if (candidate == null)
            {
                continue;
            }// if

            // ここに来た時点で「まだ生きている一番古いGravityField」
            Destroy(candidate);
            break;// 1つ削除したら終了

        }// while

    }// CreateGravityField

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
        }// if

        // 生成済みGravityFieldをFIFOで削除
        while (fieldList.Count > 0)
        {
            int lastIndex = fieldList.Count - 1;
            GameObject oldField = fieldList[lastIndex];
            fieldList.RemoveAt(lastIndex);


            // すでに寿命で消えている場合終了
            if (oldField == null)
            {
                return;
            }// if

            // 削除する重力場の面積を求める
            var area = oldField.GetComponent<GravityField>();
            if (area != null)
            {
                Debug.Log("削除する重力場の面積 = " + area.area);
                // 上で求めた面積の半分を四捨五入してエネルギーに足す
                //currentPower = currentPower + Mathf.Round(area.area);
                Debug.Log("削除後の現在のエネルギー残量 = " + currentPower);
                Debug.Log("削除で戻ったエネルギー = " + Mathf.Round(area.area));
            }// if

            Destroy(oldField);

            //重力場の現生成数を減らす
            CountDown();

            break; // １つ処理したら終了


        }// while

    }//  RightClickDelete

    // カーソル切り替え(CanCreateSilentを使用)
    void CursorState()
    {
        // ドラッグしていない時はDEFAULTカーソル
        if (!isDragging || !lineRenderer.enabled)
        {
            Cursor.SetCursor(cursorDefault, Vector2.zero, CursorMode.ForceSoftware);
            isYesCursor = true;
            return;
        }// if

        // CanCreateSilentを参照
        bool canCreate = CanCreateSilent();

        // 生成可能な時はYES
        if (canCreate && !isYesCursor)
        {
            Cursor.SetCursor(cursorYes, Vector2.zero, CursorMode.ForceSoftware);
            isYesCursor = true;
        }
        // 生成不可能な時はNO
        else if (!canCreate && isYesCursor)
        {
            Cursor.SetCursor(cursorNo, Vector2.zero, CursorMode.ForceSoftware);
            isYesCursor = false;
        }// if
    }// CursorState

    // 範囲指定モードと方向指定モードの切り替え
    public void ModeChange()
    {
        // true→false false→true
        mouseMode = !mouseMode;

        if (mouseMode == true)
        {
            // 方向指定ボタンUIを非表示
            DirectionBotton.SetActive(false);
            Time.timeScale = 1;
        }
        if (mouseMode == false)
        {
            // 方向指定ボタンUIを表示
            DirectionBotton.SetActive(true);
            Debug.Log("現在のエネルギー残量 = " + currentPower);
        }

        Debug.Log("モードチェンジ!" + mouseMode);
    }//ModeChange


    void CountUp()
    {
        currentList++;
        if (currentList >= maxFieldCount)
        {
            checkMaxField = true;
        }
    }

    //現在の生成されている重力場のカウントを減らす
    public void CountDown()
    {
        currentList--;
        checkMaxField = false;
        if (currentList < 0)
        {
            currentList = 0;
        }
    }//CountDown

    // 指定方向のプレハブを、LastCreatedField と同じ位置・サイズで生成
    public GameObject CreateDirectionalField(string dir)
    {
        if (LastCreatedField == null) return null;

        GameObject template = GetPrefabByDirection(dir);
        if (template == null) return null;

        Transform orgTr = LastCreatedField.transform;
        GravityField orgGF = LastCreatedField.GetComponent<GravityField>();

        // プレハブを同じ位置・回転で生成
        GameObject newField = Instantiate(template, orgTr.position, orgTr.rotation);

        // スケールもコピー（面積・見た目を完全一致させる）
        newField.transform.localScale = orgTr.localScale;

        // GravityField 情報コピー
        GravityField newGF = newField.GetComponent<GravityField>();
        if (newGF != null && orgGF != null)
        {
            newGF.width = orgGF.width;
            newGF.height = orgGF.height;
            // 必要なら方向情報なども設定

            currentPower = currentPower - Mathf.Round(newGF.area * 2f);
            Debug.Log("生成後のエネルギー残量 = "+currentPower);
        }

        // 管理リストに入れる場合
        RightClickDelete();
        fieldList.Add(newField);
        LastCreatedField = newField;
        CountUp();

        return newField;
    }// CreateDirectionalField

}// MouseControll
