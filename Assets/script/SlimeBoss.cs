using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SlimeBoss : Enemy
{

    public Transform SlimePoint;
    public Transform[] locators;
    public GameObject SlimeBullet;
    public GameObject WIN;

    public int CurrentPosition = 0;//²{¦b¦ì¸m
    public int atkCount = 0;

    public GameObject effect, effect2;
    public Image hpUI;
    private void OnTriggerEnter2D(Collider2D other) {

        if (other.CompareTag("Player") || other.CompareTag("UPPlayer") || other.CompareTag("DPlayer"))
        {
            aud.PlayOneShot(sound,  1.5f );
            Vector3 v3 = new Vector3(0, 0, 0.1f);

            GameObject g1 = Instantiate(effect, transform.position, Quaternion.identity);

            GameObject g2 = Instantiate(effect2, transform.position, Quaternion.identity);

            g2.transform.localRotation = Quaternion.Euler(0, 0, Random.Range(0.0f, 360.0f));
            g1.transform.localScale = Vector3.one * 3;
            g2.transform.localScale = Vector3.one * 2;
        }


    }

    public override void Start() {
        StartCoroutine(BattleStart());
    }

    private void Update() {
        hpUI.fillAmount = hp / 50f;
        //StateMachine();
        //if (Input.GetKeyDown(KeyCode.RightArrow))
        //{
        //    nav.MoveTo(locators[0].position);

        //}
        //if (Input.GetKeyDown(KeyCode.LeftArrow))
        //{
        //    nav.MoveTo(locators[1].position);

        //}
        //if (Input.GetKeyDown(KeyCode.UpArrow))
        //{
        //    ShotSlime();

        //}
        if (hp <= 0)
        {
            anim.SetTrigger("Die");
            WIN.SetActive(true);
            Destroy(nav);
            this.enabled = false;
        }
    }
    public override void StateMachine() {
        //    anim.SetBool("jump", nav.moving);

    }


    public void ShotSlime(int i) {
        GameObject SlimeBullet2 = Instantiate(SlimeBullet, transform.position, Quaternion.identity);
        SlimeBullet2.GetComponent<Rigidbody2D>().velocity = new Vector2(3 * i, 5);
   //     GameObject s1 = Instantiate(SlimeBullet, SlimePoint.position, SlimePoint.rotation);
        //Vector3 angle = transform.eulerAngles;
        //angle.y = i == 1 ? 0 : 180;

        //s1.transform.eulerAngles = angle;

    }

    IEnumerator BattleStart() {
        yield return new WaitForSeconds(1);
        MoveTo(0);
        nav.OnReach = TurnAround;
    }


    public void MoveTo(int i) {
        nav.MoveTo(locators[i].position);
        CurrentPosition = i;
    }
    public void TurnAround() {
        StartCoroutine(DelayAttack(0.5f));
    }
    IEnumerator DelayAttack(float time) {
        bool right = CurrentPosition == 0;
        float distanceFromPlayer = nav.targetPos.x - locators[right ? 0 : 1].position.x;
        yield return new WaitForSeconds(time);
        nav.FaceTo(right ? -1 : 1);
        yield return new WaitForSeconds(time);
        ShotSlime(right ? -1 : 1);
        anim.SetTrigger("jump");
        yield return new WaitForSeconds(time*2.0F);
        ShotSlime(right ? -1 : 1);
        yield return new WaitForSeconds(time * 8.0f);
        MoveTo(right ? 1 : 0);
        //body.velocity = new Vector2(distanceFromPlayer, 3);
        nav.OnReach = TurnAround;

    }

}
