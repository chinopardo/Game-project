using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class door : MonoBehaviour {
	public int LevelToLoad;
	private gameMaster gm;


	void Start(){
		gm = GameObject.FindGameObjectWithTag ("GameMaster").GetComponent<gameMaster> ();
	}
	void OnTriggerEnter2D(Collider2D col){
		if(col.CompareTag("Player")){
			gm.InputText.text = ("[E] to Enter");
			if(Input.GetKeyDown("e")){
				Application.LoadLevel(2);
			}
		}
	}
	void OnTriggerStay2D(Collider2D col){
		if(col.CompareTag("Player")){
			if(Input.GetKeyDown("e")){
				SaveScore();
				Application.LoadLevel(2);
			}
		}
	}
	void OnTriggerExit2D(Collider2D col){
		if(col.CompareTag("Player")){
			gm.InputText.text = (" ");
		}
	}
	void SaveScore(){
		PlayerPrefs.SetInt ("Points", gm.points);
	}

}
