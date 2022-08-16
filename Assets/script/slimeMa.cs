using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class slimeMa : MonoBehaviour
{

    public GameObject slime;
    public float TimeWait;

    private void Start() {
        StartCoroutine(Waitshot());
    }
    // Update is called once per frame
    void Update() {



    }


    IEnumerator Waitshot() {

        yield return new WaitForSeconds(TimeWait);
        GameObject g2 = Instantiate(slime, transform.position, Quaternion.identity);
        g2.GetComponent<Rigidbody2D>().velocity = new Vector2(2, 3);
        StartCoroutine(Waitshot());
    }
}
