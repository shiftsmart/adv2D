using System.Collections;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float hp = 10;
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


    private void Awake() {
        body = GetComponent<Rigidbody2D>();
        nav = GetComponent<Navigation2D>();
        anim = GetComponentInChildren<Animator>();
    }

    private void Start() {
        StartCoroutine("SearchTimer");
    }
    public void SearchPlayer() {
        Collider2D[] hits = new Collider2D[1];
        int result = Physics2D.OverlapBoxNonAlloc(transform.position, detcetRange, 0, hits, LayerMask.GetMask("player"));
        if (result > 0)
        {
            target = GameObject.FindWithTag("kenshi").transform;
            StopCoroutine("SearchTimer");
        }
        print(result);


    }


    IEnumerator SearchTimer() {
        while (true)
        {
            yield return new WaitForSeconds(searchInterval);
            SearchPlayer();
        }


    }
    public void OnDrawGizmosSelected() {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position + new Vector3(detcetRange.x, 0, 0), 0.2f);
        Gizmos.DrawWireSphere(transform.position + new Vector3(0, detcetRange.y, 0), 0.2f);

    }

    public void Damage(float dmg) {
        hp -= dmg;

    }


    //public void OnCollisionStay2D(Collision2D collision) {


    //    isGround = true;

    //}

    //public void OnCollisionExit2D(Collision2D collision) {
    //    isGround = false;

    //}
}
