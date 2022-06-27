
using UnityEngine;

public class controllerBattle : controller
{

    public float timeBtwSpawns;
    public float startTimeBtwSpawns;
    public GameObject echo;


    public override void PlayerCtl() {

        base.PlayerCtl();
        if (Input.GetKeyDown(KeyCode.Z))
        {
            if (!isGround) { body.AddForce(new Vector2(0, 65)); }
            body.velocity = new Vector2(0, body.velocity.y);
            anim.SetInteger("Attack", anim.GetInteger("Attack") + 1);
        }
        if (timeBtwSpawns <= 0)
        {
            Instantiate(echo, transform.position, Quaternion.identity);
            timeBtwSpawns = startTimeBtwSpawns;
        }
        else
        {
            timeBtwSpawns -= Time.deltaTime;
        }

        if (direction == 0)
        {


            if (Input.GetKeyDown(KeyCode.A) && Input.GetKey(KeyCode.LeftArrow))
            {
             
                   

                direction = 1;

            }
            else if (Input.GetKeyDown(KeyCode.A) && Input.GetKey(KeyCode.RightArrow))
            {
               
                direction = 2;

            }
        }
        else
        {

            if (DashTime <= 0)
            {
                direction = 0;
                DashTime = StartDashTime;
                body.velocity = Vector2.zero;


            }
            else
            {
                DashTime -= Time.deltaTime;
                
                if (direction == 1)
                {
                    body.velocity = Vector2.left * DashSpeed;
                }
                else if (direction == 2)
                {
                    body.velocity = Vector2.right * DashSpeed;
                }

            }
        }


    }
    public override void Move(int i) {
        print(CanMove());
        if (!CanMove())
        {
            body.velocity = new Vector2(i * speed * Time.deltaTime, body.velocity.y);
        }
        anim.SetFloat("MOVE", Mathf.Abs(i));

    }

    private bool CanMove() {

        return state.IsTag("lock");
    }

}
