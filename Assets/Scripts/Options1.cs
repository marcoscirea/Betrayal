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
			int might = p.might;
			if (p.equipped!=null)
				might += p.equipped.GetComponent<Item>().value--;
			if (might >  c.monster.value){
				Debug.Log("Player wins");
				world.deck.giveCards(p, c.monster.value);
				c.monster=null;
			}
			else{
				Debug.Log("player loses");
				p.emptyInventory();
				Destroy(p.equipped);
				p.equipped=null;
			}
			break;
		case "activate":
			//world.activePlayer().endTurn();
			break;
		}
		Player p1 = world.activePlayer();
		Card c1 = world.cards[p1.x,p1.y].GetComponent<Card>();
		c1.expired=true;
		world.activePlayer().endTurn();
		world.options.SetActive(false);
	}
}
