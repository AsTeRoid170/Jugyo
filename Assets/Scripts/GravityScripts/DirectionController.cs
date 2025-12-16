using UnityEngine;

public class DirectionController : MonoBehaviour
{
    private MouseControll mouseControll;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // 探索
        GameObject obj = GameObject.Find("MouseControllSystem");
        if (obj != null)
        {
            mouseControll = obj.GetComponent<MouseControll>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Up()
    {
        Debug.Log("上が押された!");
        mouseControll.RotateLastField(90);
        mouseControll.ModeChange();
    }

    public void Down()
    {
        Debug.Log("下が押された!");
        mouseControll.RotateLastField(-90);
        mouseControll.ModeChange();
    }
    public void Left()
    {
        Debug.Log("左が押された!");

        // 直近に生成されたフィールドを取得
        if (mouseControll == null) return;

        GameObject field = mouseControll.LastCreatedField;
        if (field == null) return;

        GravityField gf = field.GetComponent<GravityField>();
        if (gf == null) return;

        // 向きを左に変更
        gf.SetDirectionLeft();

        mouseControll.ModeChange();
    }
    public void Right()
    {
        Debug.Log("右が押された!");
        mouseControll.RotateLastField(0);
        mouseControll.ModeChange();
    }

}
