using UnityEngine;
using System.Collections;

public class DestroyPlayer : MonoBehaviour {

	private float counter;
	private float deathCooldown=1.5f;
	private bool restart;
	
	void OnStart(){
		restart=false;
	}
	
	// Use this for initialization
	void OnTriggerEnter2D(Collider2D coll){
		if(coll.CompareTag("Player")){
			Destroy (coll.gameObject);
			restart=true;
		}
	}
	
	void Update(){
		if(restart){
			deathCooldown-=Time.deltaTime;
			if(deathCooldown<=0){
				restartGame ();
			}
			
		}
	}
	
	public void restartGame(){
		Application.LoadLevel(0);
	}
}
