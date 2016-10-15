using UnityEngine;
using System.Collections;

public class Screenshake : MonoBehaviour {

	private Hashtable ht = new Hashtable();

	// Use this for initialization
	void Start () 
	{
		ht.Add ("time", 10f);
		iTween.ShakePosition (gameObject, ht);
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void Shake ()
	{
		
	}
}
