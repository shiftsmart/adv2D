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
    public AnimatorStateInfo state;

    #endregion

    #region 事件:程式入口

    void Start() {
        body = GetComponent<Rigidbody2D>();
        anim = GetComponentInChildren<Animator>();

    }
    private void Update() {
        PlayerCtl();

    }


    #endregion

    #region 功能:實作該系統的複雜方法


    //玩家控制器
    public virtual void PlayerCtl() {

        bool upkey = Input.GetKeyUp(KeyCode.RightArrow) || Input.GetKeyUp(KeyCode.LeftArrow);


        if (Input.GetKey(KeyCode.RightArrow))
        {

            Move(1);
            durction(1);
        }

        if (Input.GetKey(KeyCode.LeftArrow))
        {

            Move(-1);
            durction(0);
        }


        if (Input.GetKeyDown(KeyCode.Space))
        {
            Jump();

        }

        if (upkey)
        {

            Move(0);

        }

        StatMachine();
        if (body.velocity.y < -0.07)
        {
            anim.SetBool("Jump2", false);
        }


    }

    void Jump() {
        if (!isGround) { return; }
        body.velocity = new Vector2(body.velocity.x, jumpheight);// 舊的跳/20220620

        //  body.AddForce(new Vector2(0, jumpheight));

        anim.SetBool("Jump2", true);
        /// anim.SetTrigger("Jump");

    }



    public virtual void Move(int i) {

      //  body.velocity = new Vector2(i * speed * Time.deltaTime, body.velocity.y);

        anim.SetFloat("MOVE", Mathf.Abs(i));
        print(Mathf.Abs(i));


    }


    void durction(int i) {
        transform.eulerAngles = new Vector3(0, 180 * i, 0);


    }

    /// <summary>
    /// 控制動畫
    /// </summary>
    void StatMachine() {

        anim.SetBool("Ground", isGround);

        anim.SetFloat("Y", body.velocity.y);
        state = anim.GetCurrentAnimatorStateInfo(0);//0是動畫layer的index

    }
    /// <summary>
    /// 控制碰撞
    /// </summary>

    private void OnCollisionStay2D(Collision2D collision) {


        isGround = true;

    }

    private void OnCollisionExit2D(Collision2D collision) {
        isGround = false;

    }
    #endregion



}
