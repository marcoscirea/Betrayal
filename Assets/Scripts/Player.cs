using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {

	/*Player can:
	 * have a turn - done
	 * make parties
	 * pick a card - done
	 * use a card - done
	 * interact with world card (scavenge, fight)
	 * 
	 * turn ends when player interacts with world card or attacks monster/player
	 */

	public int survival=3;
	public int exploration=3;
	public int might=3;
	public ArrayList inventory = new ArrayList();
	public bool isActive=false;
	Player next_player;
	Deck deck;
	GameObject spotlight;

	// Use this for initialization
	void Start () {

		spotlight = transform.FindChild("Spotlight").gameObject;

		if(gameObject.name=="P1"){
			isActive=true;
		}

		//set next_player
		if (gameObject.name=="P1"){
			next_player = GameObject.Find("P2").GetComponent<Player>();
		}
		if (gameObject.name=="P2"){
			next_player = GameObject.Find("P3").GetComponent<Player>();
		}
		if (gameObject.name=="P3"){
			next_player = GameObject.Find("P4").GetComponent<Player>();
		}
		if (gameObject.name=="P4"){
			next_player = GameObject.Find("P1").GetComponent<Player>();
		}

		//get deck
		deck=GameObject.Find("Deck").GetComponent<Deck>();
	}
	
	// Update is called once per frame
	void Update () {
		
		spotlight.SetActive(isActive);
	}

	public void takeItem(GameObject item){
		//Debug.Log("taken");
		inventory.Add(item);
		item.GetComponent<Item>().player=gameObject.name;

		refresh();

		endTurn();

		deck.locked= false;
	}

	public void useItem(GameObject item){
		//Debug.Log("remove");
		inventory.Remove(item);

		Destroy(item);

		refresh ();
	}

	void endTurn(){
		//Debug.Log("next turn");
		isActive=false;
		next_player.isActive=true;
	}

	void refresh(){
		//reorder cards 
		for (int i=0; i< inventory.Count; i++){
			GameObject tmp = (GameObject) inventory[i];
			if (gameObject.name=="P1"){
				tmp.transform.position= new Vector3((- (float) inventory.Count/2)+i+0.5f,transform.position.y-1, 0);
			}
			if (gameObject.name=="P2"){
				tmp.transform.position= new Vector3(transform.position.x-1,(- (float) inventory.Count/2)+i+0.5f, 0);
			}
			if (gameObject.name=="P3"){
				tmp.transform.position= new Vector3((- (float) inventory.Count/2)+i+0.5f,transform.position.y+1, 0);
			}
			if (gameObject.name=="P4"){
				tmp.transform.position= new Vector3(transform.position.x+1,(- (float) inventory.Count/2)+i+0.5f, 0);
			}
		}
	}
}
