using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Wallslide : MonoBehaviour {
    public Vector2 WallJumpClimb = new Vector2(10, 20);
    public Vector2 WallJumpOff = new Vector2(10, 15);
    public Vector2 WallLeap = new Vector2(15, 18);

    public float WallSlideSpeedMax = 3;
    public float WallStickTime = .25f;
    float timeToWallUnstick;
    int wallDirX;

    [HideInInspector]
    public bool IsWallsliding = false;
    [HideInInspector]
    public bool CanWallslide = true;

    public void HandleWallSliding(Vector2 directionalInput, Controller2D.CollisionInfo collisions, ref Vector3 velocity, ref float velocityXSmoothing) {
        wallDirX = (collisions.left) ? -1 : 1;
        IsWallsliding = false;
        if ((collisions.left || collisions.right) && !collisions.below && velocity.y < 0) {
            IsWallsliding = true;

            if (velocity.y < -WallSlideSpeedMax) {
                velocity.y = -WallSlideSpeedMax;
            }

            if (timeToWallUnstick > 0) {
                velocityXSmoothing = 0;
                velocity.x = 0;

                if (directionalInput.x != wallDirX && directionalInput.x != 0) {
                    timeToWallUnstick -= Time.deltaTime;
                } else {
                    timeToWallUnstick = WallStickTime;
                }
            } else {
                timeToWallUnstick = WallStickTime;
            }

        }
    }

    public void WallJump(Vector2 directionalInput, ref Vector3 velocity) {
        if (wallDirX == directionalInput.x) {
            velocity.x = -wallDirX * WallJumpClimb.x;
            velocity.y = WallJumpClimb.y;
        } else if (directionalInput.x == 0) {
            velocity.x = -wallDirX * WallJumpOff.x;
            velocity.y = WallJumpOff.y;
        } else {
            velocity.x = -wallDirX * WallLeap.x;
            velocity.y = WallLeap.y;
        }
    }
}
