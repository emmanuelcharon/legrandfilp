using UnityEngine;
using System.Collections;


public class FlipControl : MonoBehaviour {

	public bool left; // false means its a right flipper

	private Rigidbody2D rb;
	private float torqueForce = 1000*1000*1.5f;

	void Awake () {
		rb = GetComponent<Rigidbody2D> ();
	}
	
	void Update () {
		if (left) {
			if (Input.GetKeyDown (KeyCode.LeftArrow)) {
				rb.AddTorque (torqueForce, ForceMode2D.Impulse);
				GameManager.s.leftFlipperSoundOn.Play ();
			}
			if (Input.GetKeyUp (KeyCode.LeftArrow)) {
				rb.AddTorque (-torqueForce, ForceMode2D.Impulse);
				GameManager.s.leftFlipperSoundOff.Play ();
			}
			if(Input.GetKey (KeyCode.LeftArrow)) {
				rb.AddTorque (torqueForce, ForceMode2D.Force);
			}
		} else {
			if (Input.GetKeyDown (KeyCode.RightArrow)) {
				rb.AddTorque (-torqueForce, ForceMode2D.Impulse);
				GameManager.s.rightFlipperSoundOn.Play ();
			}
			if (Input.GetKeyUp (KeyCode.RightArrow)) {
				rb.AddTorque (torqueForce, ForceMode2D.Impulse);
				GameManager.s.rightFlipperSoundOff.Play ();
			}
			if(Input.GetKey (KeyCode.RightArrow)) {
				rb.AddTorque (-torqueForce, ForceMode2D.Force);
			}
		}

		if(!GameManager.s.touchedInputOnce && (Input.GetKeyDown (KeyCode.LeftArrow)||Input.GetKeyDown (KeyCode.RightArrow))){
			GameManager.s.touchedInputOnce = true;
			GameManager.s.StartGame ();
		}
	}
}
