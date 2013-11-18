using UnityEngine;
using System.Collections;

public class Item : MonoBehaviour {

	public string player;

	// Use this for initialization
	void Start () {
	
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
