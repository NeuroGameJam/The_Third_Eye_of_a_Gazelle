using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Loop : MonoBehaviour {

	public static Loop instance;
	public Camera mainCam;
	public GameObject title;
	public GameObject keys;
	public GameObject game;

	public GameObject predictionRight;
	public GameObject predictionLeft;
	public GameObject results;

	void Awake(){
		instance = this;
		Parameters.matchIndex = -1;
	}

	void Start () {		
		Parameters.rightEyeMistakes = new List<int> ();
		Parameters.leftEyeMistakes = new List<int> ();
		Parameters.rightEyeSuccess = new List<int> ();
		Parameters.leftEyeSuccess = new List<int> ();

		Parameters.totalMatches = 3;
		Parameters.roundTime = 1f;
		Parameters.roundPauseTime = 1.5f;
		Parameters.fourInputs = true;

		instance.EnableIntro ();
		instance.Invoke ("EnableKeys", 5f);
		instance.Invoke ("EnableGame", 15f);
	}

	void EnableIntro(){
		Cursor.visible = false;
		mainCam.GetComponent<MultiTextureChroma> ().enabled = false;
		title.SetActive (true);
		keys.SetActive (false);
		game.SetActive (false);
		instance.predictionRight.SetActive (false);
		instance.predictionLeft.SetActive (false);
		instance.results.SetActive (false);
		secondScreen = false;
	}

	void EnableKeys(){
		Cursor.visible = false;
		mainCam.GetComponent<MultiTextureChroma> ().enabled = false;
		title.SetActive (false);
		keys.SetActive (true);
		game.SetActive (false);
		instance.predictionRight.SetActive (false);
		instance.predictionLeft.SetActive (false);
		instance.results.SetActive (false);
		secondScreen = false;
	}

	void EnableGame(){
		Cursor.visible = false;
		mainCam.GetComponent<MultiTextureChroma> ().enabled = true;
		title.SetActive (false);
		keys.SetActive (false);
		game.SetActive (true);

		instance.predictionRight.SetActive (false);
		instance.predictionLeft.SetActive (false);
		instance.results.SetActive (false);

		secondScreen = false;

		Play.StartGame ();
	}

	public void Next(){

		NextPredictionScreen ();
	}

	public static void NextPredictionScreen(){
		if (instance.results.activeSelf) {
			if (Parameters.matchIndex >= Parameters.totalMatches) {
				//save into file
				Debug.Log ("hello");
				SceneManager.LoadScene (0);
			} 
			else {
				instance.EnableGame ();
			}
		}
		else if (secondScreen) {
			EnableResults ();
			return;
		}
		else if (!instance.predictionRight.activeSelf && !instance.predictionLeft.activeSelf)
			EnablePredictionFirst ();
		else
			EnablePredictionSecond ();
	}

	public static void EnablePredictionFirst(){
		Cursor.visible = true;
		secondScreen = false;
		instance.mainCam.GetComponent<MultiTextureChroma> ().enabled = false;
		instance.title.SetActive (false);
		instance.keys.SetActive (false);
		instance.game.SetActive (false);
		int rand = Random.Range (0, 2);
		instance.predictionRight.SetActive (rand == 0);
		instance.predictionLeft.SetActive (rand != 0);
		instance.results.SetActive (false);
	}

	static bool secondScreen = false;
	public static void EnablePredictionSecond(){
		Cursor.visible = true;
		secondScreen = true;
		instance.mainCam.GetComponent<MultiTextureChroma> ().enabled = false;
		instance.title.SetActive (false);
		instance.keys.SetActive (false);
		instance.game.SetActive (false);
		instance.predictionRight.SetActive (!instance.predictionRight.activeSelf);
		instance.predictionLeft.SetActive (!instance.predictionLeft.activeSelf);
		instance.results.SetActive (false);
	}

	public static void EnableResults(){
		Cursor.visible = true;
		secondScreen = true;
		instance.mainCam.GetComponent<MultiTextureChroma> ().enabled = false;
		instance.title.SetActive (false);
		instance.keys.SetActive (false);
		instance.game.SetActive (false);
		instance.predictionRight.SetActive (false);
		instance.predictionLeft.SetActive (false);
		instance.results.SetActive (true);
		instance.GetComponent<Parameters> ().UpdateResults ();
		instance.GetComponent<Parameters> ().ResetSliders ();
	}


	void Update () {
	}
}
