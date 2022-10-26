using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageSystem : MonoBehaviour
{
    public float dmg = 1.0f;
    [SerializeField]
    private TargetType type = TargetType.Enemy;

    public float time = 0;
    private void Update() {
        time += Time.deltaTime;
    }
    private void OnTriggerStay2D(Collider2D collision) {


        if (time > 1) {
            switch (type)
            {
                case 0:
                    if (collision.gameObject.layer == 6|| collision.gameObject.layer == 7)
                    {
                        collision.GetComponent<Enemy>().Damage(dmg);
                        time = 0;
                    }
                    break;

                case (TargetType)1:
                    if (collision.gameObject.layer == 3)
                    {
                        collision.GetComponent<controllerBattle>().Damage(dmg);
                        time = 0;
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
       
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        switch(type) {
            case 0:
                if (collision.gameObject.layer==6) {
                    collision.GetComponent<Enemy>().Damage(dmg);
                }
                break;

            case (TargetType)1:
                if (collision.gameObject.layer == 3)
                {
                    collision.GetComponent<controllerBattle>().Damage(dmg);
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
