using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class HUD : MonoBehaviour {
	public Sprite[] BatterySprites; 
	public Image BatteryUI;
	GameObject battery;
	private player PLAYER;
	// Use this for initialization
	void Start () {
		PLAYER = GameObject.FindGameObjectWithTag ("Player").GetComponent<player> ();
		//PLAYER = GameObject.FindGameObjectWithTag ("Player").GetComponent<player> ();
		battery = GameObject.Find("Battery");
		//AssetDatabase.LoadAssetAtPath ("Assets/sprites/Battery/bat2.png", typeof(Sprite)) as Sprite;

		battery.GetComponent<Image>().sprite = BatterySprites[player.curHealth]; //AssetDatabase.LoadAssetAtPath ("Assets/sprites/Battery/bat1" + ".png", typeof(Sprite)) as Sprite;;
	}
	
	// Update is called once per frame
	void Update () {
		print (player.curHealth);
		battery.GetComponent<Image>().sprite = BatterySprites[player.curHealth];

	}
}
