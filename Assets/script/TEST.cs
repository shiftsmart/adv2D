using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TEST : MonoBehaviour
{
    private Rigidbody2D rb;
    public Transform leftpoint, rightpoint;
    public float Speed;
    private float leftx, rightx;
    private bool Faceleft = true;
    void Start()       
    {

        rb = GetComponent<Rigidbody2D>();
        transform.DetachChildren();
        leftx = leftpoint.position.x;
        rightx = rightpoint.position.x;
        Destroy(leftpoint.gameObject);
        Destroy(rightpoint.gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        move();
    }
    private void move()
    {
       
            rb.velocity = new Vector2(Speed, rb.velocity.x);
            if (rb.transform.position.x < leftx)
            {
                rb.velocity = new Vector2(Speed, -1);
             
                Faceleft = false;
            }
      
           // rb.velocity = new Vector2(Speed, rb.velocity.x);
            if (rb.transform.position.x > rightx)
            {
                rb.velocity = new Vector2(Speed, 1);
                Faceleft = true;
            }
      
    }
}
