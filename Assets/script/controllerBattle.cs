
using System.Collections;
using UnityEngine;


public class controllerBattle : controller
{




    //public float timeBtwSpawns;
    //public float startTimeBtwSpawns;
    //public GameObject echo;
    private bool canDash = true;
    private bool isDashing;
    private float dashingPower = 10f;
    private float dashingTime = 0.2f;
    private float dashingCooldown = 0.5f;

    [SerializeField] private TrailRenderer tr;
    [SerializeField]
    private Game gg;


    public GameObject effect;
    public override void PlayerCtl() {

        if (isDashing)
        {
            return;
        }
        base.PlayerCtl();

        if (!state.IsName("Base.hurt"))
        {
            if (Input.GetKeyDown(KeyCode.Z))
            {
                if (!isGround)
                {

                    body.velocity = new Vector2(0, 1);
                    //body.AddForce(new Vector2(0, 65)); 

                }
                //  body.velocity = new Vector2(0, 4);
                body.velocity = new Vector2(0, 0);
                //   body.velocity = new Vector2(0, body.velocity.y);

                anim.SetInteger("Attack", anim.GetInteger("Attack") + 1);

            }
            if (Input.GetKeyDown(KeyCode.Z) && Input.GetKey(KeyCode.UpArrow))
            {
                if (!isGround) { return; }
                if (state.IsName("Base.hurt")) { return; }
                anim.SetTrigger("UPATK");
                body.velocity = new Vector2(0, 6);
            }

            if (Input.GetKeyDown(KeyCode.Z) && Input.GetKey(KeyCode.DownArrow))
            {
                if (state.IsName("Base.UPATK")) { return; }
                anim.SetTrigger("DOWNATK");
                body.velocity = new Vector2(0, -6);
            }
            //�sdash
            if (Input.GetKeyDown(KeyCode.C) && canDash && Input.GetKey(KeyCode.RightArrow))
            {
                StartCoroutine(Dash(1));

            }
            if (Input.GetKeyDown(KeyCode.C) && canDash && Input.GetKey(KeyCode.LeftArrow))
            {
                StartCoroutine(Dash(-1));

            }
            #region   ��dash
            //if (timeBtwSpawns <= 0)
            //{
            //    Instantiate(echo, transform.position, Quaternion.identity);
            //    timeBtwSpawns = startTimeBtwSpawns;
            //}
            //else
            //{
            //    timeBtwSpawns -= Time.deltaTime;
            //}

            //if (direction == 0)
            //{


            //    if (Input.GetKeyDown(KeyCode.A) && Input.GetKey(KeyCode.LeftArrow))
            //    {



            //        direction = 1;

            //    }
            //    else if (Input.GetKeyDown(KeyCode.A) && Input.GetKey(KeyCode.RightArrow))
            //    {

            //        direction = 2;

            //    }
            //}
            //else
            //{

            //    if (DashTime <= 0)
            //    {
            //        direction = 0;
            //        DashTime = StartDashTime;
            //        body.velocity = Vector2.zero;


            //    }
            //    else
            //    {
            //        DashTime -= Time.deltaTime;

            //        if (direction == 1)
            //        {
            //            body.velocity = Vector2.left * DashSpeed;
            //        }
            //        else if (direction == 2)
            //        {
            //            body.velocity = Vector2.right * DashSpeed;
            //        }

            //    }
            //}

            #endregion


            //if (Input.GetKeyDown(KeyCode.X)) {

            //    Time.timeScale = 0.2f; ��C�ɶ�
            //}
        }
    }

    public override void Move(int i) {
        // print(CanMove());
        if (!CanMove())
        {

            body.velocity = new Vector2(i * speed, body.velocity.y);
            //body.velocity = Vector2.Lerp(body.velocity, new Vector2(i, 0) * speed, 0.1f * 0.2f); ///new0804
        }
        anim.SetFloat("MOVE", Mathf.Abs(i));

    }

    private bool CanMove() {

        return state.IsTag("lock");
    }

    private IEnumerator Dash(int i) {

        Physics2D.IgnoreLayerCollision(3, 3, true);//�����ϼh
        canDash = false;
        isDashing = true;
        float originalGravity = body.gravityScale;
        body.gravityScale = 0f;
        body.velocity = new Vector2(transform.localScale.x * i * dashingPower, 0f);
        tr.emitting = true;
        yield return new WaitForSeconds(dashingTime);
        tr.emitting = false;
        body.gravityScale = originalGravity;
        isDashing = false;
        yield return new WaitForSeconds(dashingCooldown);
        canDash = true;
        StartCoroutine(DashTime());
    }

    IEnumerator DashTime() {

        yield return new WaitForSeconds(0.3f);

        Physics2D.IgnoreLayerCollision(3, 3, false);//�����ϼh
    }

    public void Damage(float dmg) {
        if (state.IsName("Base.hurt")) { body.velocity = new Vector2(0, -2); return; }
        if (state.IsName("Base.UPATK")) { return; }
        if (state.IsName("Base.downATK")) { return; }
        anim.SetTrigger("hurt");
        gg.sav.Damage(dmg);
        GameObject g1 = Instantiate(effect, transform.position, Quaternion.identity);
    }

}
