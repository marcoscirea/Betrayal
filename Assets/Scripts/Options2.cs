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
		switch (this.transform.FindChild("New Text").GetComponent<TextMesh>().text){
		case "flee":
			// theplayer tries to flee
			Player p = world.activePlayer();
			if (Random.value >  1f/p.exploration){
				Debug.Log("Player flees");
			}
			else{
				Debug.Log("player loses");
				p.emptyInventory();
				Destroy(p.equipped);
				p.equipped=null;
			}
			p.endTurn();
			break;

		default:
			if (new_card){
				world.activePlayer().endTurn();
				new_card=false;
			}
			else
				world.options.SetActive(false);
			break;
		}
	}
}