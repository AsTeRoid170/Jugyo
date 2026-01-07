using UnityEngine;

public class playerMove : MonoBehaviour
{
    Animator animator;
    public float moveSpeed = 5f;
    public float jumpForce = 7f;

    Rigidbody2D rb;
    bool isGrounded = false;
    public CameraController cameraController; // カメラ制御クラス

    private Vector2 gravityDirection = new Vector2(0, -1);
    //重力の強さ
    private float defG = 9.81f;

    public float speed;        
    public GroundCheck ground;
    [SerializeField] bool isGround = false;

    void Start()
    {

        Application.targetFrameRate = 60;
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        rb.gravityScale = 0f;

        // カメラ初期位置
        cameraController.SetPosition(transform.position);
    }

    bool canMove = true; // プレイヤーが動けるかどうか

    void Update()
    {
        isGround = ground.IsGround();
        if (!canMove)
        {
            Debug.Log("Skill");
            // スキル発動中は動作しない
            return;
        }

        float moveX = 0f;

        if (Input.GetKey(KeyCode.A)) moveX = -1f;
        if (Input.GetKey(KeyCode.D)) moveX = 1f;

        transform.position += new Vector3(moveX * moveSpeed * Time.deltaTime, 0f, 0f);

        

        if (Input.GetMouseButtonDown(0))  // 0は左クリック
        {
            SkillAttack(); // 仮のスキル発動メソッド
        }
        if (Input.GetMouseButtonUp(0))
        {
            SkillAttackNo();
        }
        // 空中判定（ジャンプ中・落下中）
        animator.SetBool("IsGrounded", isGrounded);

        if (Input.GetMouseButtonDown(1))  // 0は左クリック
        {
            animator.SetBool("Turn", true);
           
         }
        if (Input.GetMouseButtonUp(1))
        {
            animator.SetBool("Turn", false);
        }
        
        // 落下状態
        bool isFalling = !isGrounded && rb.linearVelocity.y < -0.1f;
        animator.SetBool("IsFalling", isFalling);

        bool isMoving = Mathf.Abs(moveX) > 0.01f;
        animator.SetBool("IsMoving", isMoving);
        animator.SetFloat("MoveX", moveX);
        animator.SetBool("FacingRight", moveX > 0);

        // カメラに自身の座標を渡す
        cameraController.SetPosition(transform.position);
    }//Update

    public void SetGravityDirection(Vector2 dir)
    {
        gravityDirection = dir.normalized;
    }
    private void FixedUpdate()
    {
        // ジャンプ処理
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            animator.SetTrigger("Jump"); // ジャンプアニメーション開始
        }

        if (isGround == true /*collision.gameObject.layer == LayerMask.NameToLayer("Ground")*/)
        {
            isGrounded = true;
            animator.ResetTrigger("Jump"); // 着地でジャンプアニメーション終了
            animator.SetBool("IsGrounded", true); // 状態を維持する場合
        }

        if (isGround == false /*collision.gameObject.layer == LayerMask.NameToLayer("Ground")*/)
        {
            isGrounded = false;
            animator.SetBool("IsGrounded", false); // 空中状態
        }


        Vector2 dir = gravityDirection.normalized;
        Vector2 g = dir * defG;

        rb.AddForce(g, ForceMode2D.Force);

    }

    void SkillAttack()
    {
        Debug.Log("Skill Used!");
        animator.SetBool("Skill", true);
        rb.gravityScale = 0f;

       

    }

    void SkillAttackNo() {

        Debug.Log("Skill");
        canMove = true;
        animator.SetBool("Skill", false);
        rb.gravityScale = 1f;

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
                Debug.Log("重力変更　上");
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