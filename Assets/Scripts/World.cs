using UnityEngine;
using System.Collections;

public class World : MonoBehaviour {

	public GameObject[,] cards;
	public GameObject card;
	public GameObject options;
	public int side = 3;
	public Player p1;
	public Player p2;
	public Player p3;
	public Player p4;
	public Deck deck;
	bool startingCards=true;
	public Transform center;

	public bool machinePresent = false;

	// Use this for initialization
	void Start () {
		cards = new GameObject[side,side];
		for (int i =0; i<side; i++){
			for (int j=0; j<side; j++){
				cards[i,j]=newCard(i,j);
			}
		}
//		cards = new GameObject[,]{
//			{newCard (-1f,-1f), newCard(0,-1f), newCard(1f,-1f)},
//			{newCard (-1f,0f), newCard(0,0f), newCard(1f,0f)},
//			{newCard (-1f,1f), newCard(0,1f), newCard(1f,1f)}
//		};

		cards[(int) side/2, (int) side/2].GetComponent<Card>().isHome();

		GameObject.Find("Main Camera").transform.position= new Vector3(side/2, side/2, -10f);
		deck = GameObject.Find("Deck").GetComponent<Deck>();

		//put players on home
		p1= GameObject.Find("P1").GetComponent<Player>();
		p1.moveTo(cards[(int)side/2, (int)side/2].transform, (int)side/2, (int)side/2);
		p2= GameObject.Find("P2").GetComponent<Player>();
		p2.moveTo(cards[(int)side/2, (int)side/2].transform, (int)side/2, (int)side/2);
		p3= GameObject.Find("P3").GetComponent<Player>();
		p3.moveTo(cards[(int)side/2, (int)side/2].transform, (int)side/2, (int)side/2);
		p4= GameObject.Find("P4").GetComponent<Player>();
		p4.moveTo(cards[(int)side/2, (int)side/2].transform, (int)side/2, (int)side/2);


		//define center
		center = cards[(int)side/2, (int)side/2].transform;

		//center Options
		options = GameObject.Find("Options");
		options.transform.position=GameObject.Find("Status").transform.position + new Vector3(0,2,-2);
		options.SetActive(false);
	}
	
	// Update is called once per frame
	void Update () {
		if (activePlayer()==null){
			//activate player 1
			p1.isActive=true;
			p1.updateStatus();
		}

		if (startingCards) {
			startingCards=false;
			deck.giveCards(p1, 3);
			deck.giveCards(p2, 3);
			deck.giveCards(p3, 3);
			deck.giveCards(p4, 3);
		}
	}

	GameObject newCard(float x, float y){
		GameObject ret = (GameObject) Instantiate(card, new Vector3(x,y,0), new Quaternion(0,0,0,0));
		ret.GetComponent<Card>().x=x;
		ret.GetComponent<Card>().y=y;
		return ret;
	}

	public string accessible(float x, float y){
		if (y<side-1 && cards[(int) x,(int)y+1].GetComponent<Card>().on && cards[(int) x,(int)y+1].GetComponent<Card>().down
		    && activePlayer().x==x && activePlayer().y == y+1)
			return "up";
		if (x<side-1 && cards[(int)x+1,(int)y].GetComponent<Card>().on && cards[(int) x+1,(int)y].GetComponent<Card>().left
		    && activePlayer().x==x+1 && activePlayer().y == y)
			return "right";
		if (y>0 && cards[(int)x,(int)y-1].GetComponent<Card>().on && cards[(int) x,(int)y-1].GetComponent<Card>().up
		    && activePlayer().x==x && activePlayer().y == y-1)
			return "down";
		if (x>0 && cards[(int)x-1,(int)y].GetComponent<Card>().on && cards[(int) x-1,(int)y].GetComponent<Card>().right
		    && activePlayer().x==x-1 && activePlayer().y == y)
			return "left";
		return "none";
	}

	public Player activePlayer(){
		if (p1.isActive)
			return p1;
		if (p2.isActive)
			return p2;
		if (p3.isActive)
			return p3;
		if (p4.isActive)
			return p4;
		return null;
	}
}
