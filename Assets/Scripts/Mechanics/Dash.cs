using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Dash : MonoBehaviour {
    public float Force = 15f;
    public float Duration = .1f;
    public float Cooldown = .5f;

    [HideInInspector]
    public bool IsDashing = false;
    [HideInInspector]
    public bool CanDash = true;

    public void BeginDash(Vector2 directionalInput, ref Vector3 velocity) {
        if (!IsDashing && CanDash) {
            velocity.x = directionalInput.x * Force;
            velocity.y = directionalInput.y * Force;
            IsDashing = true;
            CanDash = false;
            StartCoroutine(IsDashingTimer());
            StartCoroutine(CanDashTimer());
        }
    }

    IEnumerator IsDashingTimer() {
        yield return new WaitForSeconds(Duration);
        IsDashing = false;
    }

    IEnumerator CanDashTimer() {
        yield return new WaitForSeconds(Cooldown);
        CanDash = true;
    }
}
