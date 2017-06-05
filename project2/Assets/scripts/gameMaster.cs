using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class gameMaster : MonoBehaviour {

	public int points;
	public int highScore = 0;
	public Text	pointsText;
	public Text InputText;


	void Start(){
		if(PlayerPrefs.HasKey("Points")){
			if(Application.loadedLevel == 0){
				PlayerPrefs.DeleteKey("Points");
				points = 0;
			}
			else{
				points = PlayerPrefs.GetInt("Points");
			}
		}
		if (PlayerPrefs.HasKey ("HighScore")) {
			highScore = PlayerPrefs.GetInt("HighScore");
		}

	}
	// Update is called once per frame
	void Update () {
		pointsText.text = ("Points: " + points);
	}
}
