using UnityEngine;
using System.Collections;

public class Options3 : MonoBehaviour {
	
	World world;
	
	// Use this for initialization
	void Start () {
		world= GameObject.Find("World").GetComponent<World>();
	}
	
	void OnMouseUpAsButton () {

		world.activePlayer().endTurn();
		world.options.SetActive(false);
	}
}