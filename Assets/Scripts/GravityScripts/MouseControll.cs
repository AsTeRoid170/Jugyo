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
        // �n�_�ݒ�
        if (Input.GetMouseButtonDown(0))
        {
            startPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            isDragging = true;
        }
        //�@�h���b�O���͏I�_���X�V
        if (Input.GetMouseButton(0)&&isDragging)
        {
            endPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            DrawRectangle();

            
        }

        // �}�E�X�𗣂�����h���b�O�I��
        if (Input.GetMouseButtonUp(0))
        {
            isDragging = false;
        }
    }

    // �`��i���A�ʂł͂Ȃ��j
    void DrawRectangle()
    {
        //�c���2�_���v�Z
        Vector2 p1 = startPoint;
        Vector2 p2 = new Vector2(startPoint.x, endPoint.y);
        Vector2 p3 = endPoint;
        Vector2 p4 = new Vector2(endPoint.x, startPoint.y);

        // LineRendrer�̒��_�ɃZ�b�g
        if (lineRenderer != null)
        {
            lineRenderer.positionCount = 5;// ���������`�̂��߂T�_
            lineRenderer.SetPosition(0, p1);
            lineRenderer.SetPosition(1, p2);
            lineRenderer.SetPosition(2, p3);
            lineRenderer.SetPosition(3, p4);
            lineRenderer.SetPosition(4, p1);// ����
        }
    }

    void deleteLine()
    {

    }
}
