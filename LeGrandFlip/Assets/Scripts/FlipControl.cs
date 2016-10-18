using UnityEngine;
using System.Collections;


public class FlipControl : MonoBehaviour {

	public bool left; // false means its a right flipper (and true it is a left flipper

	private Rigidbody2D rb;
	private float torqueForce = 1000*1000*1.5f;

	void Awake () {
		rb = GetComponent<Rigidbody2D> ();
	}
	
	void Update () {
		if (left) {
			bool leftKeyDown = Input.GetKeyDown (KeyCode.LeftArrow) ||
			                   Input.GetKeyDown (KeyCode.Q) ||
			                   Input.GetKeyDown (KeyCode.LeftShift);

			if (leftKeyDown) {
				rb.AddTorque (torqueForce, ForceMode2D.Impulse);
				GameManager.s.leftFlipperSoundOn.Play ();
			}

			bool leftKeyUp = Input.GetKeyUp (KeyCode.LeftArrow) ||
				Input.GetKeyUp (KeyCode.Q) ||
				Input.GetKeyUp (KeyCode.LeftShift);

			if (leftKeyUp) { // add force that puts the flipper down, faster than gravity
				rb.AddTorque (-torqueForce, ForceMode2D.Impulse);
				GameManager.s.leftFlipperSoundOff.Play ();
			}

			bool leftKey = Input.GetKey (KeyCode.LeftArrow) ||
				Input.GetKey (KeyCode.Q) ||
				Input.GetKey (KeyCode.LeftShift);

			if(leftKey) {
				rb.AddTorque (torqueForce, ForceMode2D.Force);
			}

		} else {

			bool rightKeyDown = Input.GetKeyDown (KeyCode.RightArrow) ||
				Input.GetKeyDown (KeyCode.D) ||
				Input.GetKeyDown (KeyCode.RightShift);

			if (rightKeyDown) {
				rb.AddTorque (-torqueForce, ForceMode2D.Impulse);
				GameManager.s.rightFlipperSoundOn.Play ();
			}

			bool rightKeyUp = Input.GetKeyUp (KeyCode.RightArrow) ||
				Input.GetKeyUp (KeyCode.D) ||
				Input.GetKeyUp (KeyCode.RightShift);

			if (rightKeyUp) { // add force that puts the flipper down, faster than gravity
				rb.AddTorque (torqueForce, ForceMode2D.Impulse);
				GameManager.s.rightFlipperSoundOff.Play ();
			}

			bool rightKey = Input.GetKey (KeyCode.RightArrow) ||
				Input.GetKey (KeyCode.D) ||
				Input.GetKey (KeyCode.RightShift);

			if(rightKey) {
				rb.AddTorque (-torqueForce, ForceMode2D.Force);
			}
		}

		if(!GameManager.s.touchedInputOnce && (Input.GetKeyDown (KeyCode.LeftArrow)||Input.GetKeyDown (KeyCode.RightArrow))){
			GameManager.s.touchedInputOnce = true;
			GameManager.s.StartGame ();
		}
	}
}
