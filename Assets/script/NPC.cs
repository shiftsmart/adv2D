using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class NPC : MonoBehaviour
{

    public Vector3 pos1;
    public Vector3 pos2;
    [SerializeField] private Vector2 waitTime;

    public Navigation2D nav;
    public Animator anim;

    private void Awake() {
        nav = GetComponent<Navigation2D>();
        anim = GetComponent<Animator>();
        pos1 += transform.position;
        pos2 += transform.position;


    }


    // Start is called before the first frame update
    private void Start() {
        StartCoroutine("Wander");
     
    }
    private void Update() {
        StateMachine();
    }



    IEnumerator Wander() {
        bool point1 = true;
        nav.MoveTo(pos1);
        while (true)
        {
            if (!nav.moving)
            {
               yield return new WaitForSeconds(Random.Range(waitTime.x, waitTime.y));
                point1 = !point1;
                nav.MoveTo(point1 ? pos1 : pos2);

            }
            yield return new WaitForSeconds(Time.deltaTime);
        }
    }

    private void StateMachine() {
        anim.SetBool("move", nav.moving);

    }

    public void OnDrawGizmosSelected() {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position + pos1, 0.2f);
        Gizmos.DrawWireSphere(transform.position + pos2, 0.2f);



    }
}
