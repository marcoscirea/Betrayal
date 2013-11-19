using UnityEngine;
using System.Collections;

public class PlayerLight : MonoBehaviour {

	bool up = true;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (up && transform.position.z>=-3f){
			transform.position -= new Vector3(0,0,Time.deltaTime);
		}
		else{
			if (transform.position.z <-3f) 
				up = false;
			transform.position += new Vector3(0,0,Time.deltaTime);
			if (transform.position.z>=-2f)
				up=true;
		}
	}
}
