using UnityEngine;
using System.Collections;

[RequireComponent (typeof (PlayerCharacter))]
public class PlayerInput : MonoBehaviour {

	public PlayerCharacter player;

	void Update () {
		Vector2 directionalInput = new Vector2 (Input.GetAxisRaw ("Horizontal"), Input.GetAxisRaw ("Vertical"));
		player.SetDirectionalInput (directionalInput);
        

		if (Input.GetButtonDown("Jump")) {
			player.OnJumpInputDown ();
		}
		if (Input.GetButtonUp("Jump")) {
			player.OnJumpInputUp ();
		}

        if (Input.GetButtonDown("Fire2")) {
            player.OnDashInputDown();
        }
        if (Input.GetButtonUp("Fire2")) {
            player.OnDashInputUp();
        }

        if (Input.GetButtonDown("Fire1")) {
            player.OnFireInputDown();
        }

        if(Input.GetButtonUp("Fire1")) {
            player.OnFireInputUp();
        }
	}
}
