using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Navigation2D : MonoBehaviour
{
    [SerializeField]
    public float speed = 23;
    [SerializeField] float stoppingDistance = 0.05f;
    public Vector3 targetPos;
    [SerializeField] private Rigidbody2D body;
    public bool moving = false;
    public Vector2 v2;
    private void Awake() {
        body = GetComponent<Rigidbody2D>();

    }
    private void FixedUpdate() {

        float moveSpeed = Time.fixedDeltaTime * speed * Mathf.Sign(targetPos.x - transform.position.x);
        //body.velocity = new Vector2((moving == true ? moveSpeed : 0), 0);
        v2 = new Vector2((moving == true ? moveSpeed : 0), 0);
        body.velocity = Vector2.Lerp(v2, targetPos, (moving == true ? 0.1f : 0));
        //print(moving);
        //print( v2);
       // print(targetPos);

        if (body.velocity.x != 0)
        {
            transform.eulerAngles = new Vector3(0, (body.velocity.x > 0 ? 0 : 180), 0);

        }



        if (ReachGoal())
        {
            StopMove();

        }

    }
    public void StartMove() {

        moving = true;
    }
    public void StopMove() {
        moving = false;

    }
    public void MoveTo(Vector3 p) {
        targetPos = p;
        StartMove();

    }
    public void follow(Transform tar) {
        targetPos = tar.position;
        if (!ReachGoal())
        {

            StartMove();

        }
    }

    public bool ReachGoal() {
        return Mathf.Abs(transform.position.x - targetPos.x) <= stoppingDistance;

    }

    public void StateMachine() {



    }
}
