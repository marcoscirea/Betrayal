﻿using UnityEngine;
using System.Collections;

public class Card : MonoBehaviour {

	//bool home=false;
	public Material mat_home;
	public bool on = false;
	public World world;
	public float x;
	public float y;
	public bool up;
	public bool down;
	public bool right;
	public bool left;
	public float connectionPercentage=0.6f;

	// Use this for initialization
	void Start () {
		world = GameObject.Find("World").GetComponent<World>();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnMouseUpAsButton () {
		string dir = world.accessible(x,y);
		//Debug.Log(dir);
		if ( dir != "none"){
			randomConnections();
			if (dir == "up")
				up=true;
			if (dir == "down")
				down=true;
			if (dir == "right")
				right=true;
			if (dir == "left")
				left=true;
			renderer.enabled=true;
			if (!up || y==world.side-1)
				gameObject.transform.FindChild("Up").renderer.enabled=true;
			if (!down || y==0)
				gameObject.transform.FindChild("Down").renderer.enabled=true;
			if (!right || x==world.side-1)
				gameObject.transform.FindChild("Right").renderer.enabled=true;
			if (!left || x==0)
				gameObject.transform.FindChild("Left").renderer.enabled=true;
			on=true;
		}
	}

	public void isHome(){
		//home=true;
		renderer.material=mat_home;
		renderer.enabled=true;
		on = true;
		up = true;
		down = true;
		right= true;
		left = true;
	}

	void randomConnections(){
		up=randomBool();
		down=randomBool();
		right=randomBool();
		left=randomBool();
	}

	bool randomBool(){
				if (Random.value<connectionPercentage)
					return true;
				else 
					return false;
			}
}