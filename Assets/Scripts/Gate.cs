using UnityEngine;
using System.Collections;

public class Gate : MonoBehaviour {

	World world;
	TextMesh text;
	GameObject child1;
	GameObject child2;
	GameObject child3;
	Player next_player;
	Transform original_pos;
	
	// Use this for initialization
	void Start () {
		world = GameObject.Find("World").GetComponent<World>();
		original_pos = transform;


		text = transform.FindChild("Text2").GetComponent<TextMesh>();

		child1=transform.FindChild("Text1").gameObject;
		child2=transform.FindChild("Text2").gameObject;
		child3=transform.FindChild("Text3").gameObject;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void activateGate(Player p) {
		Debug.Log("gate active");
		next_player = p;

		renderer.material=next_player.renderer.material;
		renderer.enabled=true;
		child1.renderer.enabled=true;
		child2.renderer.enabled=true;
		child3.renderer.enabled=true;

		updateText(p.name);

		//center
		transform.position = new Vector3(world.center.position.x, world.center.position.y,-4);
	} 	

	void OnMouseUpAsButton () {
		Debug.Log("gate deactivated");
		next_player.isActive=true;
		next_player.updateStatus();

		transform.position= new Vector3 (-10f, -2f, -3);

		renderer.enabled=false;
		child1.renderer.enabled=false;
		child2.renderer.enabled=false;
		child3.renderer.enabled=false;
	}

	public void updateText(string p){
		text.text="player "+p+" sit down";
	}
}
