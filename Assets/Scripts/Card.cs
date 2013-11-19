using UnityEngine;
using System.Collections;

public class Card : MonoBehaviour {

	//bool home=false;
	public Material mat_home;
	public bool on = false;
	public World world;
	public float x;
	public float y;
	public bool up;
	public bool down;
	public bool right;
	public bool left;
	public float connectionPercentage=0.6f;

	GameObject text;
	string type;


	// Use this for initialization
	void Start () {
		world = GameObject.Find("World").GetComponent<World>();
		text = gameObject.transform.FindChild("Text").gameObject;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnMouseUpAsButton () {
		//for testing
		if (on==false && isClose(world.activePlayer())){
		//if (on==false){
			string dir = world.accessible(x,y);
			//Debug.Log(dir);
			if ( dir != "none"){
				randomConnections();
				if (dir == "up")
					up=true;
				if (dir == "down")
					down=true;
				if (dir == "right")
					right=true;
				if (dir == "left")
					left=true;
				renderer.enabled=true;
				if (!up || y==world.side-1)
					gameObject.transform.FindChild("Up").renderer.enabled=true;
				if (!down || y==0)
					gameObject.transform.FindChild("Down").renderer.enabled=true;
				if (!right || x==world.side-1)
					gameObject.transform.FindChild("Right").renderer.enabled=true;
				if (!left || x==0)
					gameObject.transform.FindChild("Left").renderer.enabled=true;
				on=true;

				setType();

				world.activePlayer().endTurn();
			}
		}
		else {
			if(isClose(world.activePlayer()))
				world.activePlayer().moveTo(transform, (int)x, (int)y);
		}
	}

	public void isHome(){
		//home=true;
		renderer.material=mat_home;
		renderer.enabled=true;
		on = true;
		up = true;
		down = true;
		right= true;
		left = true;
	}

	void randomConnections(){
		up=randomBool();
		down=randomBool();
		right=randomBool();
		left=randomBool();
	}

	bool randomBool(){
				if (Random.value<connectionPercentage)
					return true;
				else 
					return false;
			}

	bool isClose(Player p){
		if (p.x == x){
			if (p.y == y-1 || p.y == y+1)
				return true;
		}
		if (p.y == y){
			if (p.x == x-1 || p.x == x+1)
				return true;
		}
		return false;
	}

	void setType(){
		float v = Random.value;
		if (v <=0.6){
			if (v<=0.3){
				//empty terrain
				type="empty";
				//text.GetComponent<TextMesh>().text="M";
				//text.SetActive(true);
			}
			else {
				//scavenging terrain
				type="scav";
				text.GetComponent<TextMesh>().text="S";
				text.SetActive(true);
			}
		}
		else {
			if (v<=0.9){
				//trouble terrain
				type="trouble";
				text.GetComponent<TextMesh>().text="T";
				text.SetActive(true);
			}
			else {
				//machine
				if (world.machinePresent)
					setType();
				else {
					world.machinePresent=true;
					type="machine";
					text.GetComponent<TextMesh>().text="M";
					text.SetActive(true);
				}
			}
		}
	}
}
