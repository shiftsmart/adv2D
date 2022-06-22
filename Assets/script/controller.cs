using UnityEngine;

public class controller : MonoBehaviour
{


    #region 資料:保存系統需要的資料
    [SerializeField, Header("速度"), Range(100, 500)]
    public float speed = 100;
    [SerializeField, Header("跳躍高度"), Range(0, 5000)]
    public float jumpheight = 1000;
    [SerializeField, Header("剛體控制")]
    public Rigidbody2D body;
    [SerializeField, Header("動畫控制")]
    public Animator anim;
    [SerializeField, Header("是否在地板上")]
    public bool isGround = false;
    private bool clickjump;
    private bool Moving;
    private int moveRightLeft = 0;
    #endregion

    #region 事件:程式入口

    void Start()
    {
        body = GetComponent<Rigidbody2D>();
        anim = GetComponentInChildren<Animator>();

    }
    private void Update()
    {
        PlayerCtl();
        jumpkey();

    }

    private void FixedUpdate()
    {
        JumpForce();
        Move();


    }

    #endregion

    #region 功能:實作該系統的複雜方法
    private void PlayerCtl()
    {

        bool upkey = Input.GetKeyUp(KeyCode.RightArrow) || Input.GetKeyUp(KeyCode.LeftArrow);
        moveRightLeft = 0;

        if (Input.GetKey(KeyCode.RightArrow))
        {
            Moving = true;
            moveRightLeft = 1;
            //  Move(1);
            durction(1);
        }

        if (Input.GetKey(KeyCode.LeftArrow))
        {
            Moving = true;
            moveRightLeft = -1;
            //  Move(-1);
            durction(0);
        }


        if (Input.GetKeyDown(KeyCode.Space))
        {
            clickjump = true;
            moveRightLeft = 0;
        }

        if (upkey)
        {
            moveRightLeft = 0;
            print(upkey);
            print("moveRightLeft：" + moveRightLeft);
        }

        StatMachine();
        if (body.velocity.y < -0.07)
        {
            anim.SetBool("Jump2", false);
        }


    }

    private void JumpForce()
    {

        if (clickjump)
        {
            if (!isGround) { return; }
             body.velocity = new Vector2(body.velocity.x,jumpheight);// 舊的跳/20220620

            //body.AddForce(new Vector2(0, jumpheight));
            anim.SetBool("Jump2", true);
            clickjump = false;
        }
    }


    void Jump()
    {
        if (!isGround) { return; }
        // body.velocity = new Vector2(body.velocity.x,jumpheight); 舊的跳/20220620

        body.AddForce(new Vector2(0, jumpheight));

        anim.SetBool("Jump2", true);
        /// anim.SetTrigger("Jump");

    }
    private void jumpkey()
    {

        if (Input.GetKeyDown(KeyCode.Space))
        {
            clickjump = true;
            // print("aaa");
        }
    }


    private void Move()
    {
        if (Moving)
        {
            body.velocity = new Vector2(moveRightLeft * speed * Time.deltaTime, body.velocity.y);
            print("MOVE：" + Mathf.Abs(moveRightLeft));
            anim.SetFloat("MOVE", Mathf.Abs(moveRightLeft));
            Moving = false;
        }

    }

    void durction(int i)
    {
        transform.eulerAngles = new Vector3(0, 180 * i, 0);


    }

    void StatMachine()
    {

        anim.SetBool("Ground", isGround);

        anim.SetFloat("Y", body.velocity.y);
    }

    private void OnCollisionStay2D(Collision2D collision)
    {


        isGround = true;

    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        isGround = false;

    }
    #endregion



}
