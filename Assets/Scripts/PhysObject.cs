using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

[RequireComponent(typeof(Controller2D))]
public class PhysObject : MonoBehaviour {
    protected float effectiveGravity = -50f;
    protected Controller2D controller;
    public Vector3 velocity;
    protected int dir = 1;

    protected bool hasLogged = false;

    public virtual void Start() {
        controller = GetComponent<Controller2D>();
    }

    public virtual void Update() {
        if(velocity.x < 0) {
            dir = -1;
        } else if(velocity.x > 0) {
            dir = 1;
        }
        controller.Move(velocity * Time.deltaTime);

        if (controller.collisions.above || controller.collisions.below) {
            velocity.y = 0;
        }
    }

    public virtual void CalculateVelocity() {
        velocity.y += effectiveGravity * Time.deltaTime;
    }
}
