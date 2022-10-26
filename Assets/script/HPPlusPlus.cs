using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HPPlusPlus : MonoBehaviour
{

    [SerializeField]
    private Game gg2;
    private void OnTriggerEnter2D(Collider2D collision) {


        if (collision.CompareTag("kenshi"))
        {
            gg2.sav.HPPlus();
        }
    }
    private void Awake() {
        gg2 = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Game>();
    }
 
}
