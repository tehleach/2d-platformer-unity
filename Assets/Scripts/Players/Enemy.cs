using UnityEngine;
using System.Collections;

public class Enemy : PlayerCharacter {
    SimpleFollow ai;
    bool shouldChangeInput = true;

    public override void Start() {
        ai = GetComponent<SimpleFollow>();

        base.Start();
    }

    public override void Update() {
        if (shouldChangeInput) {
            SetDirectionalInput(ai.GetDirectionalInput(transform.position, controller.collisions));
            shouldChangeInput = false;
        } else if (!controller.collisions.below || controller.collisions.left || controller.collisions.right) {
            shouldChangeInput = true;
        }
        base.Update();
    }

    public override void OnCollisionEnter2D(Collision2D other) {
        if(other.gameObject.tag == Constants.PROJECTILE) {
            Arrow projectile = other.gameObject.GetComponent<Arrow>();
            if(!projectile.isLodged) {
                print("enemy is kill");
                Destroy(this.gameObject);
            }
        }
        base.OnCollisionEnter2D(other);
    }
}
