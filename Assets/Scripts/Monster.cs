using UnityEngine;
using System.Collections;

public class Monster  {

	public int value;
	public World world;
	public string n;



	public Monster(){
		value = Random.Range(1,4);
		world = GameObject.Find("World").GetComponent<World>();
	}

	public void show(){
		world.monster.SetActive(true);
	}
}
