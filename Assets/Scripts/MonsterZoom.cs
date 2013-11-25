using UnityEngine;
using System.Collections;

public class MonsterZoom : MonoBehaviour {

	Vector3 original_position;
	Vector3 original_scale;
	bool scale=true;
	// Use this for initialization
	void Start () {
		original_scale=transform.localScale;
		original_position=transform.position;
	}
	
	// Update is called once per frame
	void Update () {
		scaleDown();
	}

	void OnMouseEnter() {
			transform.position += new Vector3(0,0,-3);
			scale=false;
			//transform.localScale += new Vector3(1,1,0);
	}
	
	void OnMouseExit() {
		transform.position=original_position;
		//transform.localScale = original_scale;
		scale=true;
	}
	
	void OnMouseOver(){
			if (transform.position==original_position){
				transform.position += new Vector3(0,0,-3);
				scale=false;
			}
			if (transform.localScale.x <= original_scale.x + 0.5f)
				transform.localScale += new Vector3(1,1,0)*Time.deltaTime;
	}
	
	void scaleDown(){
		if (scale){
			if (transform.localScale.x > original_scale.x){
				transform.localScale -= new Vector3(1,1,0)*Time.deltaTime;
			}
			else
				transform.localScale=original_scale;
		}
	}
}
