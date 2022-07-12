using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageSystem : MonoBehaviour
{
    private float dmg = 1.0f;
    private TargetType type = TargetType.Enemy;




    private void OnTriggerEnter2D(Collider2D collision) {
        switch(type) {
            case 0:
                if (collision.gameObject.layer==11) {  }
                break;

            case (TargetType)1:
                if (collision.gameObject.layer == 9)
                {

                }
                break;

            case (TargetType)2:
                if (collision.gameObject.layer == 1 || collision.gameObject.layer == 9)
                {

                }
                break;


            default:
                break;
        }



    }

    enum TargetType { Enemy,Player, ALL}


}
