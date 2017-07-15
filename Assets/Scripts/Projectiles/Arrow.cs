using System;
using System.Collections.Generic;
using System.Collections;
using System.Diagnostics;
using System.Linq;
using System.Text;
using UnityEngine;

public class Arrow : PhysObject {
    public float fireSpeed = 15f;
    public float grav = -25f;
    
    public bool isLodged = false;
    public bool canRetrieve = false;

    public override void Start() {
        effectiveGravity = grav;
        base.Start();
    }
    
    public override void Update() {
        if (!isLodged) {
            updateRotation();
            base.CalculateVelocity();
            base.Update();
        }

        if (controller.collisions.Any) {
            isLodged = true;
            canRetrieve = true;
        } else {
            isLodged = false;
            canRetrieve = false;
        }
    }

    IEnumerator DelayRetrievalTimer() {
        yield return new WaitForSeconds(1);
        canRetrieve = true;
    }

    void updateRotation() {
        Vector3 dir = velocity;
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
    }

    public void Fire(Vector2 dir) {
        velocity = dir * fireSpeed;
        updateRotation();
        StartCoroutine(DelayRetrievalTimer());
    }
}
