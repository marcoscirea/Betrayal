using UnityEngine;
using System.Collections;

public class Options1 : MonoBehaviour {

	World world;

	// Use this for initialization
	void Start () {
		world= GameObject.Find("World").GetComponent<World>();
	}
	
	void OnMouseUpAsButton () {
		//Debug.Log("button1");
		switch (this.transform.FindChild("New Text").GetComponent<TextMesh>().text){
		case "scavenge":
			world.deck.giveCards(world.activePlayer(), (int) world.activePlayer().survival/2);
			world.activePlayer().endTurn();
			break;
		case "fight":
			world.activePlayer().endTurn();
			break;
		case "activate":
			world.activePlayer().endTurn();
			break;
		}
		world.options.SetActive(false);
	}
}
