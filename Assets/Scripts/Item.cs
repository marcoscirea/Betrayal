using UnityEngine;
using System.Collections;

public class Item : MonoBehaviour {

	public string player;
	public string type;
	public int value;
	public int uses;
	public Material[] food = new Material[5];
	int[] food_data = {1,1,1,1,1};
	public Material[] equipment = new Material[2];
	int[,] equipment_data = { {2,2}, {3,2} };

	// Use this for initialization
	void Start () {
		if (Random.value <=0.7){
			//card is food
			type= "food";
			randomFood();
		}
		else {
			//card is equipment
			type= "equip";
			randomEquipment();
		}
	}
	
	// Update is called once per frame
	void Update () {
		if (value==0) {
			Player p = GameObject.Find(player).GetComponent<Player>();
			if (p.isActive)
				p.equipped=null;

			Destroy(this.gameObject);
		}
	}

	void OnMouseUpAsButton () {
		Player p = GameObject.Find(player).GetComponent<Player>();
		if (p.isActive)
			p.useItem(gameObject);
	}

	void randomFood(){
		float r = Random.value;
		//	Debug.Log("r "+r); 
		float percentage = 1f/food.Length;
		int i = 0;
		while (r > percentage*(i+1)){
			//Debug.Log(percentage*(i+1));
			i++;
		}
		renderer.material=food[i];

		value=food_data[i];
		uses=1;
	}

	void randomEquipment() {
		float r = Random.value;
		//	Debug.Log("r "+r); 
		float percentage = 1f/equipment.Length;
		int i = 0;
		while (r > percentage*(i+1)){
			//Debug.Log(percentage*(i+1));
			i++;
		}
		renderer.material=equipment[i];
		
		value = equipment_data[i,0];
		uses=equipment_data[i,1];
	}
}
