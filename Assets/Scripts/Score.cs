using UnityEngine;
using System.Collections;

public class Score : MonoBehaviour {

	private static int score=0;
	private static int highScore=0;
	
	public static void AddPoint(){
		score++;
		if(score>highScore){
			highScore=score;
		}
	}

	// Use this for initialization
	void Start () {
		score=0;
		// initialize saved high score
		highScore= PlayerPrefs.GetInt("HighScore",0);
		
	}
	
	void OnDestroy(){
		// when game shuts down, set highscore
		PlayerPrefs.SetInt ("HighScore",highScore);
	}
	
	void Update(){
		guiText.text = "Score: " + score + "\nHigh Score: " + highScore;
	}

}
