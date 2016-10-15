using UnityEngine;
using System.Collections;


public class FlipControl : MonoBehaviour {

	public bool left; // false means its a right flipper

	private Rigidbody2D rb;
	private float torqueForce = 1000*1000*1.5f;

	void Start () {
		rb = GetComponent<Rigidbody2D> ();
	}
	
	void Update () {
		if (left) {
			if (Input.GetKeyDown (KeyCode.LeftArrow)) {
				rb.AddTorque (torqueForce, ForceMode2D.Impulse);
			}
			if(Input.GetKey (KeyCode.LeftArrow)) {
				rb.AddTorque (torqueForce, ForceMode2D.Force);
			}
		} else {
			if (Input.GetKeyDown (KeyCode.RightArrow)) {
				rb.AddTorque (-torqueForce, ForceMode2D.Impulse);
			}
			if(Input.GetKey (KeyCode.RightArrow)) {
				rb.AddTorque (-torqueForce, ForceMode2D.Force);
			}
		}
	}
}
