using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeMA : MonoBehaviour
{
    public GameObject slma;

    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.CompareTag("kenshi"))
        {

            slma.SetActive(true); 
            StartCoroutine(Waitslma());
        }
    }



    IEnumerator Waitslma() {

        yield return new WaitForSeconds(10.0f);
        slma.SetActive(false);
        
    }


}
