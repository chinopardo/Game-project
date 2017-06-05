using UnityEngine;
using System.Collections;

public class Water : MonoBehaviour {

	private player PLAYER;
	void Start(){
		PLAYER = GameObject.FindGameObjectWithTag ("Player").GetComponent<player> ();

	}
	void OnTriggerEnter2D(Collider2D col){
		if (col.gameObject.tag == "Player") {
			player.Damage(15);

			//StartCoroutine(PLAYER.Knockback(0.02f, 250, PLAYER.transform.position));
		}
	}
}
