
using UnityEngine;

public class slime : Enemy
{

    //  [SerializeField, Header("≠Ë≈È±±®Ó")]
    // public Rigidbody2D body;
    public GameObject effect, effect2;

    private void OnTriggerEnter2D(Collider2D other) {
        // body = GetComponent<Rigidbody2D>();
        if (other.CompareTag("Player"))
        {
            Vector3 v3 = new Vector3(0, 0, 0.1f);
            //   nav.enabled = false;
            GameObject g1 = Instantiate(effect, transform.position, Quaternion.identity);
            //  GameObject g2 = Instantiate(effect2, transform.position , Quaternion.Euler(0, Random.Range(0.0f, 360.0f), 0)
            GameObject g2 = Instantiate(effect2, transform.position, Quaternion.identity );
            // g2.transform.localScale = new Vector3(2, 2, 0);
            g2.transform.localRotation = Quaternion.Euler(0, 0, Random.Range(0.0f, 360.0f));
            // body.velocity = new Vector2(0, 6);
            body.velocity = new Vector2(2, 4);
            // body.AddForce(new Vector2(0, 200));

            //    StopCoroutine("SearchPlayer");
        }
    }


    private void Update() {

        StateMachine();
        if (target != null)
        {
            nav.follow(target);
        }

    }



    public void StateMachine() {
        anim.SetBool("moving", nav.moving);
        anim.SetBool("attack", nav.ReachGoal() && target != null);

    }


    public override void Damage(float dmg) {
        hp -= dmg;
        if (!this.enabled) { return; };

        if (hp <= 0)
        {
            anim.SetTrigger("Die");
            Destroy(nav);
            this.enabled = false;
        }
    }
}
