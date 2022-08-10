
using System.Collections;
using UnityEngine;

public class slime : Enemy
{

    //  [SerializeField, Header("≠Ë≈È±±®Ó")]
    // public Rigidbody2D body;
    public GameObject effect, effect2;
    private bool air = false;
    private void OnTriggerEnter2D(Collider2D other) {
        // body = GetComponent<Rigidbody2D>();
        if (other.CompareTag("Player"))
        {
            Vector3 v3 = new Vector3(0, 0, 0.1f);
            //   nav.enabled = false;
            GameObject g1 = Instantiate(effect, transform.position, Quaternion.identity);
            //  GameObject g2 = Instantiate(effect2, transform.position , Quaternion.Euler(0, Random.Range(0.0f, 360.0f), 0)
            GameObject g2 = Instantiate(effect2, transform.position, Quaternion.identity);
            // g2.transform.localScale = new Vector3(2, 2, 0);
            g2.transform.localRotation = Quaternion.Euler(0, 0, Random.Range(0.0f, 360.0f));
            //this.GetComponent<Navigation2D>().moving = true;
            //body.velocity = new Vector2(3000, 300);
            //this.GetComponent<Navigation2D>().targetPos = new Vector2(-3000, 300);
            //this.GetComponent<Navigation2D>().v2 = new Vector2(-3000, 300);

            //print(this.GetComponent<Navigation2D>().v2);


            if (!isGround)
                body.velocity = new Vector2(0, 2);

        }
        if (other.CompareTag("UPPlayer"))
        {
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
          nav.enabled = false;
            GameObject g1 = Instantiate(effect, transform.position, Quaternion.identity);
            GameObject g2 = Instantiate(effect2, transform.position, Quaternion.identity);
            g2.transform.localRotation = Quaternion.Euler(0, 0, Random.Range(0.0f, 360.0f));
            body.velocity = new Vector2(0, -6);
            //    StopCoroutine("SearchPlayer");
        }
        StartCoroutine(WaitGround());

    }


    private void Update() {


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



    public void StateMachine() {
        anim.SetBool("moving", nav.moving);
        anim.SetBool("attack", nav.ReachGoal() && target != null&&isGround==true);

    }


    public override void Damage(float dmg) {
        base.Damage(dmg);
        if (!this.enabled) { return; };

        if (hp <= 0)
        {
            anim.SetTrigger("Die");
            Destroy(nav);
            this.enabled = false;
        }
    }

    IEnumerator WaitGround() {

        yield return new WaitForSeconds(1);
        if (isGround)
        {
            nav.enabled = true;
        }
    }
}
