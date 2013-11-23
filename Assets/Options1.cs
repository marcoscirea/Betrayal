using UnityEngine;
using System.Collections;

public class Options1 : MonoBehaviour {

	World world;
	public bool new_card= false;

	// Use this for initialization
	void Start () {
		world= GameObject.Find("World").GetComponent<World>();
	}
	
	void OnMouseUpAsButton () {
		//Debug.Log("button1");
		switch (this.transform.FindChild("New Text").GetComponent<TextMesh>().text){
		case "scavenge":
			world.deck.giveCards(world.activePlayer(), (int) world.activePlayer().survival/2);
			//world.activePlayer().endTurn();
			break;
		case "fight":
			// if the player is stronger it wins
			Player p = world.activePlayer();
			Card c = world.cards[p.x,p.y].GetComponent<Card>();
			if (p.might >  c.monster.value){
				Debug.Log("Player wins");
				world.deck.giveCards(p, c.monster.value);
				c.monster=null;
			}
			else{
				Debug.Log("player loses");
				p.emptyInventory();
			}
			break;
		case "activate":
			//world.activePlayer().endTurn();
			break;
		}
		world.activePlayer().endTurn();
		world.options.SetActive(false);
	}
}
