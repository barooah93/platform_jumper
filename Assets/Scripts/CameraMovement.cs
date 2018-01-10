using UnityEngine;
using System.Collections;

public class CameraMovement : MonoBehaviour {

	GameObject player; 
	private float xOffset;
	private Vector2 originalPosition;
	private Vector2 endPosition;
	private bool isMoving=false;
	private float startMoveTime;
	private float durationMoveTime;
	
	// Use this for initialization
	void Start () {
		player = GameObject.FindGameObjectWithTag("Player");
		
		xOffset = this.transform.position.x - player.transform.position.x;
	}
	
	// Update is called once per frame
	void Update () {
		if(isMoving){
			// move camera
			transform.position = Vector2.Lerp(originalPosition, endPosition, (Time.time-startMoveTime)/durationMoveTime);
		}
		if(transform.position.x == originalPosition.x){
			
			isMoving=false;
		}
	}
	
	public void moveCamera(float ballEndPosX, float startTime, float duration){
		
		originalPosition = transform.position;
		endPosition = new Vector2(ballEndPosX + xOffset, transform.position.y);
		startMoveTime = startTime;
		durationMoveTime = duration;
		isMoving=true;
	}
}
