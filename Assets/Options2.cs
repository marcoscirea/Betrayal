using UnityEngine;
using System.Collections;

public class Options2 : MonoBehaviour {
	
	World world;
	public bool new_card=false;
	
	// Use this for initialization
	void Start () {
		world= GameObject.Find("World").GetComponent<World>();
	}
	
	void OnMouseUpAsButton () {
		if (new_card){
			world.activePlayer().endTurn();
			new_card=false;
		}
		else
			world.options.SetActive(false);
	}
}