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

	public  int survival=3;
	public  int exploration=3;
	public  int might=3;
	public int max_cards=6;
	int moves;
	public ArrayList inventory = new ArrayList();
	public bool isActive=false;
	Player next_player;
	Deck deck;
	GameObject spotlight;
	World world = null;
	public int x;
	public int y;
	GameObject status;
	public bool wait =false;
	public GameObject equipped;
	public int food = 2;
	bool hungry = false;

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

		world=GameObject.Find("World").GetComponent<World>();

		status=GameObject.Find("Status");

		moves= exploration;
	}
	
	// Update is called once per frame
	void Update () {
		
		spotlight.SetActive(isActive);

		if (equipped!=null){
			equipped.transform.position= status.transform.position + new Vector3(1.5f,0,0);
			if (isActive)
				equipped.SetActive(true);
			else
				equipped.SetActive(false);
		}

		isHungry();

		if (isActive){
			world.food_gui.text = "Food: "+food;
		}
	}

	public void takeItem(GameObject item){
		//Debug.Log("taken");
		inventory.Add(item);
		item.GetComponent<Item>().player=gameObject.name;

		refresh();

		//endTurn();

		deck.locked= false;
	}

	public void useItem(GameObject item){
		//Debug.Log("remove");

		if (item.GetComponent<Item>().type=="equip"){
			if (equipped!= null){
				Destroy(equipped);
				equipped=null;
			}
			equipped=item;

			inventory.Remove(item);

			refresh ();
		}
		else {
			food += item.GetComponent<Item>().value;

			inventory.Remove(item);

			Destroy(item);

			refresh ();
		}
	}

	public void endTurn(){
		//Debug.Log("next turn");
		isActive=false;
		next_player.isActive=true;
		//next_player.isHungry();
		next_player.updateStatus();
		moves=exploration;
		wait=false;
		world.monster.SetActive(false);
		world.options.SetActive(false);
		food--;
	}

	 public void refresh(){
		//reorder cards 
		for (int i=0; i< inventory.Count; i++){
			GameObject tmp = (GameObject) inventory[i];
			//old version
//			if (gameObject.name=="P1"){
//				tmp.transform.position= new Vector3((- (float) inventory.Count/2)+i+0.5f,transform.position.y-1, 0);
//			}
//			if (gameObject.name=="P2"){
//				tmp.transform.position= new Vector3(transform.position.x-1,(- (float) inventory.Count/2)+i+0.5f, 0);
//			}
//			if (gameObject.name=="P3"){
//				tmp.transform.position= new Vector3((- (float) inventory.Count/2)+i+0.5f,transform.position.y+1, 0);
//			}
//			if (gameObject.name=="P4"){
//				tmp.transform.position= new Vector3(transform.position.x+1,(- (float) inventory.Count/2)+i+0.5f, 0);
//			}
			if (gameObject.name=="P1"){
				tmp.transform.position= new Vector3(world.cards[(world.side-1)/2, 0].transform.position.x+(- (float) inventory.Count/2)+i+0.5f,world.cards[(world.side-1)/2, 0].transform.position.y-1, 0);
			}
			if (gameObject.name=="P2"){
				tmp.transform.position= new Vector3(world.cards[0, (world.side-1)/2].transform.position.x-1,world.cards[0, (world.side-1)/2].transform.position.y+(- (float) inventory.Count/2)+i+0.5f, 0);
			}
			if (gameObject.name=="P3"){
				tmp.transform.position= new Vector3(world.cards[(world.side-1)/2, world.side-1].transform.position.x+(- (float) inventory.Count/2)+i+0.5f,world.cards[(world.side-1)/2, world.side-1].transform.position.y+1, 0);
			}
			if (gameObject.name=="P4"){
				tmp.transform.position= new Vector3(world.cards[world.side-1, (world.side-1)/2].transform.position.x+1,world.cards[world.side-1, (world.side-1)/2].transform.position.y+(- (float) inventory.Count/2)+i+0.5f, 0);
			}
		}
	}

	public void moveTo(Transform tr, float nx, float ny){
		moveTo(tr, (int)nx, (int)ny);
	}

	public void moveTo(Transform tr, int nx, int ny){

		x=nx;
		y=ny;

		if (gameObject.name=="P1"){
			transform.position = tr.position + new Vector3(-0.25f, 0.25f,-1f);
		}
		if (gameObject.name=="P2"){
			transform.position = tr.position + new Vector3(0.25f, 0.25f,-1f);
		}
		if (gameObject.name=="P3"){
			transform.position = tr.position + new Vector3(0.25f, -0.25f,-1f);
		}
		if (gameObject.name=="P4"){
			transform.position = tr.position + new Vector3(-0.25f, -0.25f,-1f);
		}

		moves--;
		if(moves==0){
			moves=exploration;
			endTurn();
		}
	}

	public void updateStatus(){
		status.renderer.material=renderer.material;
		status.transform.FindChild("Name").GetComponent<TextMesh>().text=name;
		status.transform.FindChild("Exploration").GetComponent<TextMesh>().text="Expl: "+exploration;
		status.transform.FindChild("Survival").GetComponent<TextMesh>().text="Surv: "+survival;
		status.transform.FindChild("Might").GetComponent<TextMesh>().text="Might: "+might;

		
		world.options.transform.FindChild("Opt1").renderer.material=world.activePlayer().renderer.material;
		world.options.transform.FindChild("Opt2").renderer.material=world.activePlayer().renderer.material;
		world.options.transform.FindChild("Opt3").renderer.material=world.activePlayer().renderer.material;
	}

	public void emptyInventory(){
		foreach (GameObject c in inventory){
			Destroy(c);
		}
		inventory = new ArrayList();
	}

	public void isHungry(){
		if (food<=0){
			wait=true;
			hungry = true;
			//Debug.Log(name + " is hungry");
			if (inventory.Count == 0 && Random.value < 0.5f)
				food = 2;
		}
		else {
			if (hungry){
				wait=false;
				hungry= false;
			}
		}
	}
}
