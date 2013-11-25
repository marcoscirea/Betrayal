using UnityEngine;
using System.Collections;

public class Monster  {

	public int value;
	public World world;
	public string n;
	int i;



	public Monster(){
		world = GameObject.Find("World").GetComponent<World>();
		i = (int) Random.Range(0,world.monster_sprites.Length);
		world.monster.transform.FindChild("Type").GetComponent<SpriteRenderer>().sprite=world.monster_sprites[i];
		world.monster.transform.FindChild("Might").GetComponent<TextMesh>().text=world.monster_values[i].ToString();
		world.monster.transform.FindChild("Name").GetComponent<TextMesh>().text=world.monster_names[i];
		value = world.monster_values[i];
	}

	public void show(){
		world.monster.SetActive(true);
	}
}
