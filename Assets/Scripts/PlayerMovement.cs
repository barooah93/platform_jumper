using UnityEngine;
using System.Collections;

public class PlayerMovement : MonoBehaviour {


	private float platformPadding = 0.5f;	// allowed player distance from edge of platform
	
	private float playerSweetSpotX;
	private bool isLanded=false;
	
	GameObject camera;
	GameObject platSpawn;
	CameraMovement cameraScript;
	PlatformSpawner platSpawnScript;
	private Vector2 jumpForce;
	private bool isJumping=false;
	private bool applyForce=false;
	private float forceScalar=1f;
	private float xScale=100f;
	private float xyRatio = 3.5f;
	public float moveTime =1f;
	private float startTime;
	private bool movePlayerToPosition=false;
	private Vector2 playerOriginalPos;
	private bool isDead=false;
	
	// Use this for initialization
	void Start () {
		// Get camera object and script
		camera = GameObject.FindGameObjectWithTag("MainCamera");
		cameraScript = camera.GetComponent<CameraMovement>();
		// Get platform spawner object and script
		platSpawn = GameObject.FindGameObjectWithTag("PlatformSpawner");
		platSpawnScript = platSpawn.GetComponent<PlatformSpawner>();
		
		// set initial jump force
		jumpForce = new Vector2(xScale,xScale*xyRatio);
	}
	
	// Update is called once per frame
	void Update () {
		// Check if user is holding down key and if player is already jumping
		if((Input.GetKey(KeyCode.Space)||Input.GetMouseButton(0)) && !isJumping){
			// While holding down keys, charge up jump
			forceScalar += forceScalar * Time.deltaTime;
			
		}
		// Check if user has released key and if player is already jumping
		if((Input.GetKeyUp(KeyCode.Space) || Input.GetMouseButtonUp(0)) && !isJumping){
			applyForce=true;
			isLanded=false;
			isJumping=true;
		}
		// Check if player is supposed to move to the edge of platform in preparation for next jump
		if(movePlayerToPosition){
			// Check if player is not already at the edge of platform
			if(transform.position.x != playerSweetSpotX){
				// If player is not at edge of platform, move it to the exact location given by playerSweetSpot
				// Duration lasts as specified by moveTime
				transform.position = Vector2.Lerp (playerOriginalPos,new Vector2(playerSweetSpotX,playerOriginalPos.y),(Time.time-startTime)/moveTime);
			}
			else{
				// Player does not need to move so reset bool
				movePlayerToPosition=false;
				
			}
		}
	}
	
	// Physics go here
	void FixedUpdate(){
		// Check if player is jumping
		if(applyForce){
			// Add appropriate force
			this.rigidbody2D.AddForce(jumpForce*forceScalar);
			forceScalar=1f;
			applyForce=false;
		}
	}
	
	// Triggered when the ball has landed correctly on the platform
	void landed(){
		// handle point incrementation here
		Score.AddPoint();
		isLanded=true;
		isJumping=false;
		// set start time to current time for the player moving to the edge of platform timer
		startTime=Time.time;
		// get and set current position of player upon landing
		playerOriginalPos = transform.position;
		movePlayerToPosition=true;
		// spawn the next platform according to player's position
		platSpawnScript.SpawnPlatform();
		// call moveCamera method to center player in appropriated position for next jump
		cameraScript.moveCamera(playerSweetSpotX,startTime,moveTime);
	}
	
	// Player has collided with something
	void OnCollisionEnter2D(Collision2D coll){
		// Check if the collision was a platform
		if (coll.gameObject.tag == "Platform"){
			// Check if player is already moving
			if(!movePlayerToPosition){
				float widthPlatform = coll.transform.localScale.x;
				float heightPlatform = coll.transform.localScale.y;
				float xPlatform = coll.transform.position.x;
				float yPlatform = coll.transform.position.y + (heightPlatform/2); // Interested in only the top of platform
				float leftEdge = (xPlatform-widthPlatform/2)- platformPadding;
				float rightEdge = (xPlatform+widthPlatform/2)+platformPadding;
				float bottomOfPlayer = transform.position.y-(transform.localScale.y/2);
				// Set the location of where the player is to be positioned for next jump if landed on platform correctly
			 	playerSweetSpotX = coll.gameObject.transform.GetChild(0).position.x;
			 	
			 	// Check if player landed correctly on platform
			 	if(transform.position.x >= leftEdge && transform.position.x <= rightEdge){
					if(bottomOfPlayer >= yPlatform-platformPadding){
				 		landed ();
				 	}
			 	}
			}
		}
	}
	
	
}
