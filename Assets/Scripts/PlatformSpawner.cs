using UnityEngine;
using System.Collections;

public class PlatformSpawner : MonoBehaviour {

	public GameObject platformPrefab;
	public GameObject player;
	public float minHeight;
	public float maxHeight;
	public float minWidth;
	public float maxWidth;
	private float minPlayerXOffset = 3.1f;
	private float maxPlayerXOffset = 5.1f;
	
	private float firstPlatX = -2.62f;
	
	// Use this for initialization
	void Start () {
	
		// randomize the height of the first platform
		Vector3 platformPos = new Vector3(firstPlatX, Random.Range(minHeight,maxHeight),1);
		GameObject firstPlatform = (GameObject)Instantiate(platformPrefab,platformPos, Quaternion.identity);
		firstPlatform.tag = "Untagged";
		
		SpawnPlatform();
		
	}
	
	public void SpawnPlatform(){
		float leftSide = player.transform.position.x + minPlayerXOffset;
		float rightSide = player.transform.position.x + maxPlayerXOffset;
		
		Vector3 platformPos= new Vector3(Random.Range (leftSide,rightSide),Random.Range(minHeight,maxHeight),1);
		GameObject platform = (GameObject)Instantiate(platformPrefab,platformPos,Quaternion.identity);
		platform.transform.localScale = new Vector3(Random.Range (minWidth,maxWidth), platform.transform.localScale.y, 1f);
		
	}
}
