using System.Collections;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float hp = 5;
    public float atk = 1;
    [SerializeField, Header("是否在地板上")]
    public bool isGround = false;
    public AudioClip sound;

    public AudioSource aud;



    [SerializeField] public Rigidbody2D body;
    [SerializeField, Header("動畫控制")]
    public Animator anim;
    public Navigation2D nav;
    public Vector2 detcetRange = new Vector2(10, 5);
    public Vector2 detcetRange2 = new Vector2(10, 5);
    public Transform target;
    float searchInterval = 1;
    [SerializeField]
    private Game gg2;

    [SerializeField, Header("檢查地板尺寸")]
    private Vector3 v3CheckGroundSize = Vector3.one;
    [SerializeField, Header("檢查地板位移")]
    private Vector3 v3CheckGroundOffset;
    [SerializeField, Header("檢查地板顏色")]
    private Color colorCheckGround = new Color(1, 0, 0.2f, 0.5f);
    [SerializeField, Header("檢查地板圖層")]
    private LayerMask layerCheckGround;
    public virtual void Awake() {

        try
        {

            Physics2D.IgnoreCollision(GameObject.FindGameObjectWithTag("SlimeBoss").GetComponent<BoxCollider2D>(), gameObject.GetComponent<BoxCollider2D>());//忽略圖層

        }
        catch (System.Exception)
        {


        }
        gg2 = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Game>();
        body = GetComponent<Rigidbody2D>();
        nav = GetComponent<Navigation2D>();
        anim = GetComponentInChildren<Animator>();
        aud = GetComponent<AudioSource>();
    }

    public virtual void Start() {
        StartCoroutine(SearchTimer());

    }


    private void FixedUpdate() {
        CheckGround();

    }

    public IEnumerator SearchTimer() {
        while (true)
        {

            yield return new WaitForSeconds(searchInterval);


            SearchPlayer();


        }


    }
    public void SearchPlayer() {
        Collider2D[] hits = new Collider2D[1];
        int result = Physics2D.OverlapBoxNonAlloc(transform.position, detcetRange, 0, hits, LayerMask.GetMask("player"));
        if (result > 0)
        {
            target = GameObject.FindWithTag("kenshi").transform;
            StopCoroutine("SearchTimer");
        }
        //  print(result);


    }

    ///// <summary>
    ///// 檢查是否碰到地板
    ///// </summary>
    public void CheckGround() {
        Collider2D hit = Physics2D.OverlapBox(transform.position + v3CheckGroundOffset, v3CheckGroundSize, 0, layerCheckGround);
        //print("碰到的物件:" + hit.name);

        isGround = hit;
        print(isGround);
    }

    public void OnDrawGizmosSelected() {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position + new Vector3(detcetRange.x, 0, 0), 0.2f);
        Gizmos.DrawWireSphere(transform.position + new Vector3(0, detcetRange.y, 0), 0.2f);

    }
    private void OnDrawGizmos() {
        Gizmos.color = colorCheckGround;
        Gizmos.DrawCube(transform.position + v3CheckGroundOffset, v3CheckGroundSize);

    }
    public virtual void Damage(float dmg) {

        hp -= dmg;
    }
    public void Die() {
        if (this.gameObject.layer == 6)
            gg2.sav.KillPlus(1);
        Destroy(gameObject);

    }
    public virtual void StateMachine() { }
    /// <summary>
    /// 控制碰撞
    /// </summary>

    //private void OnCollisionStay2D(Collision2D collision) {


    //    isGround = true;

    //}

    //private void OnCollisionExit2D(Collision2D collision) {
    //    isGround = false;

    //}
}
