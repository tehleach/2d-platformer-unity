using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerCharacter : PhysObject {
    Dash dash;
    Wallslide wallslide;
    Shoot shoot;

    public float maxJumpHeight = 4;
    public float minJumpHeight = 1;
    public float timeToJumpApex = .4f;
    public float moveSpeed = 6;
    float accelerationTimeAirborne = .2f;
    float accelerationTimeGrounded = .1f;

    float maxJumpVelocity;
    float minJumpVelocity;
    float velocityXSmoothing;

    Vector2 directionalInput;

    public override void Start() {
        dash = GetComponent<Dash>();
        wallslide = GetComponent<Wallslide>();
        shoot = GetComponent<Shoot>();

        base.Start();
        
        effectiveGravity = -(2 * maxJumpHeight) / Mathf.Pow(timeToJumpApex, 2);
        maxJumpVelocity = Mathf.Abs(effectiveGravity) * timeToJumpApex;
        minJumpVelocity = Mathf.Sqrt(2 * Mathf.Abs(effectiveGravity) * minJumpHeight);

        if (shoot) {
            shoot.CreateIcons();
        }
    }

    public override void Update() {
        if (dash) {
            if (!dash.IsDashing) {
                CalculateVelocity();
                if (wallslide) {
                    wallslide.HandleWallSliding(directionalInput, controller.collisions, ref velocity, ref velocityXSmoothing);
                }
            }
        } else {
            CalculateVelocity();
        }

        base.Update();
    }

    public void SetDirectionalInput(Vector2 input) {
        directionalInput = input;
    }

    public void OnJumpInputDown() {
        if (wallslide && wallslide.IsWallsliding) {
            wallslide.WallJump(directionalInput, ref velocity);
        }
        if (controller.collisions.below) {
            velocity.y = maxJumpVelocity;
        }
    }

    public void OnJumpInputUp() {
        if (velocity.y > minJumpVelocity) {
            velocity.y = minJumpVelocity;
        }
    }

    public void OnDashInputDown() {
        if (dash) {
            dash.BeginDash(directionalInput, ref velocity);
        }
    }

    public void OnDashInputUp() { }

    public void OnFireInputDown() {
        if (shoot) {
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mousePos.z = 0.0f;
            shoot.TryToShoot(mousePos - transform.position, dir);
        }
    }

    public void OnFireInputUp() { }

    public virtual void OnCollisionEnter2D(Collision2D other) {
        //TODO: retrieve arrows that I started standing on while CanRetrieve was false
        if (other.gameObject.tag == Constants.PROJECTILE && shoot && shoot.CanRetrieve()) {
            Arrow otherArr = other.gameObject.GetComponent<Arrow>();
            if (otherArr.canRetrieve) {
                shoot.Retrieve(otherArr.gameObject);
            }
        }
        //if (other.gameObject.tag == Constants.ENEMY) {
        //    Destroy(this.gameObject);
        //}
    }

    public override void CalculateVelocity() {
        base.CalculateVelocity();
        float targetVelocityX = directionalInput.x * moveSpeed;
        velocity.x = Mathf.SmoothDamp(velocity.x, targetVelocityX, ref velocityXSmoothing, (controller.collisions.below) ? accelerationTimeGrounded : accelerationTimeAirborne);
    }
}
