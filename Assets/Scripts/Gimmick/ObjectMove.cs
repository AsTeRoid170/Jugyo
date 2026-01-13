using UnityEngine;

public class ObjectMove : MonoBehaviour
{
    Rigidbody2D rb;
    private Vector2 gravityDirection = new Vector2(0, -1);
    private float defG = 9.81f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.gravityScale = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        Vector2 dir = gravityDirection.normalized;
        Vector2 g = dir * defG;

        rb.AddForce(g, ForceMode2D.Force);
    }

    public void SetGravityDirection(Vector2 dir)
    {
        gravityDirection = dir.normalized;
    }

    public void GravityDirectionControl(int gravityDirection)
    {
        switch (gravityDirection)
        {

            case 1:
                Physics2D.gravity = new Vector2(-defG, 0f);
                break;
            case 2:
                Physics2D.gravity = new Vector2(defG, 0f);
                break;
            case 3:
                Debug.Log("èdóÕïœçXÅ@è„");
                Physics2D.gravity = new Vector2(0f, defG);
                break;
            case 4:
                Physics2D.gravity = new Vector2(0f, -defG * 2);
                break;
            default:
                Physics2D.gravity = new Vector2(0f, -defG);
                break;

        }
    }
}
