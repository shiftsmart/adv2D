using System.Collections;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float hp = 5;
    public float atk = 1;
    [SerializeField, Header("是否在地板上")]
    public bool isGround = false;

    [SerializeField] public Rigidbody2D body;
    [SerializeField, Header("動畫控制")]
    public Animator anim;
    public Navigation2D nav;
    public Vector2 detcetRange = new Vector2(10, 5);

    public Transform target;
    float searchInterval = 1;

    [SerializeField, Header("檢查地板尺寸")]
    private Vector3 v3CheckGroundSize = Vector3.one;
    [SerializeField, Header("檢查地板位移")]
    private Vector3 v3CheckGroundOffset;
    [SerializeField, Header("檢查地板顏色")]
    private Color colorCheckGround = new Color(1, 0, 0.2f, 0.5f);
    [SerializeField, Header("檢查地板圖層")]
    private LayerMask layerCheckGround;
    private void Awake() {
        body = GetComponent<Rigidbody2D>();
        nav = GetComponent<Navigation2D>();
        anim = GetComponentInChildren<Animator>();
    }

    private void Start() {
        StartCoroutine("SearchTimer");
    }


    private void FixedUpdate() {
        CheckGround();

    }

    IEnumerator SearchTimer() {
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
    private void CheckGround() {
        Collider2D hit = Physics2D.OverlapBox(transform.position + v3CheckGroundOffset, v3CheckGroundSize, 0, layerCheckGround);
        //   print("碰到的物件:" + hit.name);

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
      

    }
    public void Die() {

        Destroy(gameObject);
    }

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
