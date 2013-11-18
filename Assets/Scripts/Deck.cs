using UnityEngine;
using System.Collections;

public class Deck : MonoBehaviour {

	public GameObject item;
	public bool locked = false;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnMouseUpAsButton () {
		if (!locked){
			locked=true; //lock is resetted once the player has got the card
			Player p1 = GameObject.Find("P1").GetComponent<Player>();
			if (p1.isActive){
				GameObject new_card = (GameObject) Instantiate(item);
				p1.takeItem(new_card);
			}
			else {
				Player p2 = GameObject.Find("P2").GetComponent<Player>();
				if (p2.isActive){
					GameObject new_card = (GameObject) Instantiate(item);
					p2.takeItem(new_card);
				}
				else{
					Player p3 = GameObject.Find("P3").GetComponent<Player>();
					if (p3.isActive){
						GameObject new_card = (GameObject) Instantiate(item);
						p3.takeItem(new_card);
					}
					else{
						Player p4 = GameObject.Find("P4").GetComponent<Player>();
						if (p4.isActive){
							GameObject new_card = (GameObject) Instantiate(item);
							p4.takeItem(new_card);
						}
					}
				}
			}
		}
	}
}
