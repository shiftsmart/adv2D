
using System.Collections;
using UnityEngine;

public class slime : Enemy
{

    public Vector3 pos1;
    public Vector3 pos2;
    [SerializeField] private Vector2 waitTime;
    //  [SerializeField, Header("剛體控制")]
    // public Rigidbody2D body;
    public GameObject effect, effect2;

    private bool air = false;
    private void OnTriggerEnter2D(Collider2D other)
    {

        // body = GetComponent<Rigidbody2D>();
        if (other.CompareTag("Player"))
        {
            aud.PlayOneShot(sound, 1.5f);
            Vector3 v3 = new Vector3(0, 0, 0.1f);
            CinemachineShake.Instance.ShakeCamera(5f, 0.1f);
            GameObject g1 = Instantiate(effect, transform.position, Quaternion.identity);//特效1
            GameObject g2 = Instantiate(effect2, transform.position, Quaternion.identity);//特效2
            g2.transform.localRotation = Quaternion.Euler(0, 0, Random.Range(0.0f, 360.0f));
            //  nav.enabled = false;
            //    body.velocity = new Vector2(1.5f, 0f);
            if (!isGround)
                body.velocity = new Vector2(0, 2);

        }
        if (other.CompareTag("UPPlayer"))
        {
            aud.PlayOneShot(sound, 1.5f);
            air = true;
            nav.enabled = false;
            GameObject g1 = Instantiate(effect, transform.position, Quaternion.identity);
            GameObject g2 = Instantiate(effect2, transform.position, Quaternion.identity);
            g2.transform.localRotation = Quaternion.Euler(0, 0, Random.Range(0.0f, 360.0f));
            body.velocity = new Vector2(0, 6);
            StopCoroutine(SearchTimer());

        }

        if (other.CompareTag("DPlayer"))
        {
            aud.PlayOneShot(sound, 1.5f);
            nav.enabled = false;
            GameObject g1 = Instantiate(effect, transform.position, Quaternion.identity);
            GameObject g2 = Instantiate(effect2, transform.position, Quaternion.identity);
            g2.transform.localRotation = Quaternion.Euler(0, 0, Random.Range(0.0f, 360.0f));
            body.velocity = new Vector2(0, -6);
            //    StopCoroutine("SearchPlayer");
        }
        StartCoroutine(WaitGround());
    }



    private void Update()
    {

        if (body.velocity.x != 0)
        {
            transform.eulerAngles = new Vector3(0, (body.velocity.x > 0 ? 0 : 180), 0);

        }
        if (isGround)
        {
            air = true;
        }
        StateMachine();
        if (target != null)
        {
            nav.follow(target);
        }

    }



    public void StateMachine()
    {
        anim.SetBool("moving", nav.moving);

        anim.SetBool("attack", nav.ReachGoal() && target != null && isGround == true);
       Wander();
        //  JumpAttack();

    }
    private void JumpAttack()
    {

        float distanceFromPlayer = nav.targetPos.x - body.position.x;

        if (isGround)
        {
            body.velocity = new Vector2(distanceFromPlayer, 3);

        }

    }

    public override void Damage(float dmg)
    {
        base.Damage(dmg);
        if (!this.enabled) { return; };

        if (hp <= 0)
        {
            anim.SetTrigger("Die");
            Destroy(nav);
            this.enabled = false;
        }
    }

    IEnumerator WaitGround()
    {

        yield return new WaitForSeconds(1);
        if (isGround)
        {
            nav.enabled = true;
        }
    }


    IEnumerator Wander()
    {
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
}
