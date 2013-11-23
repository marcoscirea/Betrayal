using UnityEngine;
using System.Collections;

public class Item : MonoBehaviour {

	public string player;
	public string type;
	public int value;

	// Use this for initialization
	void Start () {
		if (Random.value <=0.7){
			//card is food
			type= "food";
			value = 1;
		}
		else {
			//card is equipment
			type= "eqip";
			value = 1;
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnMouseUpAsButton () {
		Player p = GameObject.Find(player).GetComponent<Player>();
		if (p.isActive)
			p.useItem(gameObject);
	}
}
