using UnityEngine;
using System.Collections;

public class Card : MonoBehaviour {

	bool ishome=false;
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

	GameObject type_obj;
	string type;
	bool type_on = false;

	public bool expired=false;

	public Sprite home;
	public Sprite badlands;
	public Sprite church;
	public Sprite hospital;
	public Sprite house;
	public Sprite machine;
	public Monster monster = null;

	// Use this for initialization
	void Start () {
		world = GameObject.Find("World").GetComponent<World>();
		type_obj = gameObject.transform.FindChild("Type").gameObject;
	}
	
	// Update is called once per frame
	void Update () {
		if (ishome){
			
			type_obj.GetComponent<SpriteRenderer>().sprite=home;
			type_obj.SetActive(true);
		}

	}

	void OnMouseUpAsButton () {
		//for testing
		if (!world.activePlayer().wait && on==false && isClose(world.activePlayer())){
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

				world.activePlayer().moveTo(transform, x,y);

				world.options.transform.FindChild("Opt1").GetComponent<Options1>().new_card=true;
				world.options.transform.FindChild("Opt2").GetComponent<Options2>().new_card=true;

				//Srop player from moving when on new tile if there are options
				if (type!="empty")
					world.activePlayer().wait=true;
				else
					world.activePlayer().endTurn();
			}
		}
		else {
			if(!world.activePlayer().wait && isClose(world.activePlayer()) && connection( world.activePlayer().x,  world.activePlayer().y))
				world.activePlayer().moveTo(transform, (int)x, (int)y);
		}

		//activate interface if there is need
		if (on && type_on && !expired){
			activateOptions(type);
		}

		if (monster!=null){
			monster.show();
		}
		
	}

	public void isHome(){
		ishome=true;
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
				//text.GetComponent<TextMesh>().text="S";
				//text.SetActive(true);
				float tmp = Random.value;
				if (tmp<=0.33){
					type_obj.GetComponent<SpriteRenderer>().sprite=house;
					type_obj.SetActive(true);
				}
				else {
					if (tmp<=0.66){
						type_obj.GetComponent<SpriteRenderer>().sprite=hospital;
						type_obj.SetActive(true);
					}
					else {
						type_obj.GetComponent<SpriteRenderer>().sprite=church;
						type_obj.SetActive(true);
					}
				}
			}
		}
		else {
			if (v<=0.9){
				//trouble terrain
				type="trouble";
				//text.GetComponent<TextMesh>().text="T";
				//text.SetActive(true);
				type_obj.GetComponent<SpriteRenderer>().sprite=badlands;
				type_obj.SetActive(true);
				monster= new Monster();
			}
			else {
				//machine
				if (world.machinePresent)
					setType();
				else {
					world.machinePresent=true;
					type="machine";
					//text.GetComponent<TextMesh>().text="M";
					//text.SetActive(true);
					type_obj.GetComponent<SpriteRenderer>().sprite=machine;
					type_obj.SetActive(true);
				}
			}
		}
		if (type!="empty")
			type_on=true;
	}

	bool connection(float x1, float y1){
		if (x1 == x+1F && right && world.cards[(int)x1,(int)y1].GetComponent<Card>().left){
			//player arrives from the right
			return true;
		}
		if (x1 == x-1f && left && world.cards[(int)x1,(int)y1].GetComponent<Card>().right){
			//player arrives from the left
			return true;
		}
		if (y1 == y+1f && up && world.cards[(int)x1,(int)y1].GetComponent<Card>().down){
			//player arrives from up
			return true;
		}
		if (y1 == y-1f && down && world.cards[(int)x1,(int)y1].GetComponent<Card>().up){
			//player arrives from down
			return true;
		}
		return false;
	}

	public void activateOptions(string type){
		world.options.SetActive(true);
		switch (type){
		case "scav":
			world.options.transform.FindChild("Opt1").transform.FindChild("New Text").GetComponent<TextMesh>().text="scavenge";
			break;
		case "trouble":
			world.options.transform.FindChild("Opt1").transform.FindChild("New Text").GetComponent<TextMesh>().text="fight";
			world.options.transform.FindChild("Opt2").transform.FindChild("New Text").GetComponent<TextMesh>().text="flee";
			break;
		case "machine":
			world.options.transform.FindChild("Opt1").transform.FindChild("New Text").GetComponent<TextMesh>().text="activate";
			break;
		}
	}
}
