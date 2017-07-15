using UnityEngine;
using System.Collections;

public class SimpleFollow : MonoBehaviour {

    public string tagToFollow = Constants.PLAYER;

    Vector2 directionalInput;

    public Vector2 GetDirectionalInput(Vector2 myPos, Controller2D.CollisionInfo collisionInfo) {
        if(collisionInfo.left) {
            return new Vector2(1f, 0f);
        }
        if(collisionInfo.right) {
            return new Vector2(-1f, 0f);
        }
        Vector2 targetPos = GameObject.FindGameObjectWithTag(tagToFollow).transform.position;
        return new Vector2(Mathf.Sign(targetPos.x - myPos.x), 0);
    }
}
