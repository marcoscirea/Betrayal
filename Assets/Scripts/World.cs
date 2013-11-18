﻿using UnityEngine;
using System.Collections;

public class World : MonoBehaviour {

	public GameObject[,] cards;
	public GameObject card;
	public int side = 3;
	Player p1;
	Player p2;
	Player p3;
	Player p4;

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

		//put players on home
		p1= GameObject.Find("P1").GetComponent<Player>();
		p1.moveTo(cards[(int)side/2, (int)side/2].transform);
		p2= GameObject.Find("P2").GetComponent<Player>();
		p2.moveTo(cards[(int)side/2, (int)side/2].transform);
		p3= GameObject.Find("P3").GetComponent<Player>();
		p3.moveTo(cards[(int)side/2, (int)side/2].transform);
		p4= GameObject.Find("P4").GetComponent<Player>();
		p4.moveTo(cards[(int)side/2, (int)side/2].transform);

	}
	
	// Update is called once per frame
	void Update () {
	
	}

	GameObject newCard(float x, float y){
		GameObject ret = (GameObject) Instantiate(card, new Vector3(x,y,0), new Quaternion(0,0,0,0));
		ret.GetComponent<Card>().x=x;
		ret.GetComponent<Card>().y=y;
		return ret;
	}

	public string accessible(float x, float y){
		if (y<side-1 && cards[(int) x,(int)y+1].GetComponent<Card>().on && cards[(int) x,(int)y+1].GetComponent<Card>().down)
			return "up";
		if (x<side-1 && cards[(int)x+1,(int)y].GetComponent<Card>().on && cards[(int) x+1,(int)y].GetComponent<Card>().left)
			return "right";
		if (y>0 && cards[(int)x,(int)y-1].GetComponent<Card>().on && cards[(int) x,(int)y-1].GetComponent<Card>().up)
			return "down";
		if (x>0 && cards[(int)x-1,(int)y].GetComponent<Card>().on && cards[(int) x-1,(int)y].GetComponent<Card>().right)
			return "left";
		return "none";
	}
}
