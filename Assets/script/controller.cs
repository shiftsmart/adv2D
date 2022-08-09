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
    public ParticleSystem dust;
    //[SerializeField, Header("攻擊判定物件")]
    //public Collider2D hitbox;
    //public GameObject hiteffect;


    //public float DashSpeed;
    //public float DashTime;
    //public float StartDashTime;
    //public int direction;
    private void Awake() {

        Physics2D.IgnoreLayerCollision(3, 6, true);//忽略圖層
        Physics2D.IgnoreLayerCollision(3, 7, true);//忽略圖層
        body = GetComponent<Rigidbody2D>();
        anim = GetComponentInChildren<Animator>();


    }

    #endregion

    #region 事件:程式入口

    void Start() {

        //DashTime = StartDashTime;
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
            if (state.IsName("Base.UPATK")) { return; }
            if (state.IsName("Base.downATK")) { return; }
            Move(1);
            durction(1);
        }

        if (Input.GetKey(KeyCode.LeftArrow))
        {
            if (state.IsName("Base.UPATK")) { return; }
            if (state.IsName("Base.downATK")) { return; }
            Move(-1);
            durction(0);
        }




        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (state.IsName("Base.UPATK")) { return; }
            if (state.IsName("Base.downATK")) { return; }
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
        CreatDust();
        body.velocity = new Vector2(body.velocity.x, jumpheight);// 舊的跳/20220620

        //  body.AddForce(new Vector2(0, jumpheight));

        anim.SetBool("Jump2", true);
        /// anim.SetTrigger("Jump");

    }



    public virtual void Move(int i) {

        //  body.velocity = new Vector2(i * speed * Time.deltaTime, body.velocity.y);
        body.velocity = Vector2.Lerp(body.velocity, new Vector2(i, 0), 0.1f * 0.2f); ///new0804
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

    void CreatDust() {
        dust.Play();


    }

}
