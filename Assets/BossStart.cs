using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossStart : MonoBehaviour
{

    public GameObject boss;
    public GameObject BOSSUU;
    [Header("BGM")]
    public GameObject BGM;
    [Header("BOSSBGM")]
    public GameObject BOSSBGM;
    private void OnTriggerEnter2D(Collider2D collision) {


        if (collision.CompareTag("kenshi"))
        {
            boss.SetActive(true);
            BOSSUU.SetActive(true);
            BGM.GetComponent<AudioSource>().Pause();
            BGM.SetActive(false);

            BOSSBGM.SetActive(true);
        }
    }
    private void OnTriggerExit2D(Collider2D collision) {
        if (collision.CompareTag("kenshi"))
        {

            this.GetComponent<Collider2D>().isTrigger = false;
        }
    }

    void Update() {




    }
}
