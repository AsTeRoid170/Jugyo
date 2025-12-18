using UnityEngine;

public class DirectionController : MonoBehaviour
{
    private MouseControll mouseControll;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // ’Tõ
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
        //Debug.Log("ã‚ª‰Ÿ‚³‚ê‚½!");
        mouseControll.CreateDirectionalField("Up");
        mouseControll.ModeChange();
    }

    public void Down()
    {
        //Debug.Log("‰º‚ª‰Ÿ‚³‚ê‚½!");
        mouseControll.CreateDirectionalField("Down");
        mouseControll.ModeChange();
    }
    public void Left()
    {
        //Debug.Log("¶‚ª‰Ÿ‚³‚ê‚½!");
        mouseControll.CreateDirectionalField("Left");
        mouseControll.ModeChange();
    }
    public void Right()
    {
        //Debug.Log("‰E‚ª‰Ÿ‚³‚ê‚½!");
        mouseControll.CreateDirectionalField("Right");
        mouseControll.ModeChange();
    }

}
