
using UnityEngine;

public class controllerBattle : controller
{
    public override void PlayerCtl() {

        base.PlayerCtl();
        if (Input.GetKeyDown(KeyCode.Z))
        {
            if (!isGround) { body.AddForce(new Vector2(0, 65)); }
            body.velocity = new Vector2(0, body.velocity.y);
            anim.SetInteger("Attack", anim.GetInteger("Attack") + 1);
        }

    }
    public override void Move(int i) {
       
        if (!CanMove())
        {
            body.velocity = new Vector2(i * speed * Time.deltaTime, body.velocity.y);
        }
        anim.SetFloat("MOVE", Mathf.Abs(i));

    }

    private bool CanMove() {
    
        return state.IsTag("lock");
    }

}
