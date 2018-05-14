using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Play : MonoBehaviour {

	public static Play instance;

	void Start(){
		instance = this;
	}

	public enum Direction {closed, up, down, left, upLeft, right, upRight, downRight, downLeft, semi};
	public static Direction rightEyeDir;
	public static Direction leftEyeDir;
	public static Direction queue;
	public bool freezeInput;

	public static void StartGame(){
		
		instance.StopAllCoroutines ();

		Parameters.rightEyeMistakes.Add (0);
		Parameters.leftEyeMistakes.Add (0);
		Parameters.rightEyeSuccess.Add (0);
		Parameters.leftEyeSuccess.Add (0);
		Parameters.roundIndex = 0;
		Parameters.matchIndex++;

		instance.StartCoroutine (instance.ChangeQueue ());
	}

	public static void EndMatch(){
		//not this
		instance.StopAllCoroutines ();
		Loop.NextPredictionScreen ();
	}

	IEnumerator ChangeQueue(){
		freezeInput = true;
		int rounds = Parameters.totalRounds;
		float roundTime;
		float pauseTime = Parameters.roundPauseTime;
		bool fourInputs = Parameters.fourInputs;
		int rand;

		yield return new WaitForSeconds (3f);
		
		while (Parameters.roundIndex < 30
			|| (Parameters.leftEyeMistakes[Parameters.matchIndex] + Parameters.rightEyeMistakes[Parameters.matchIndex]) < 10
			//|| (Parameters.leftEyeSuccess[Parameters.matchIndex] + Parameters.rightEyeSuccess[Parameters.matchIndex]) < 5
			) {
			//close player eyes
			roundTime = Parameters.roundTime;
			leftEyeDir = Direction.closed;
			rightEyeDir = Direction.closed;
			freezeInput = false;

			//set rand queue direction
			rand = Random.Range (0, 4);
			if (rand == 0)
				queue = Direction.up;
			else if (rand == 1)
				queue = Direction.down;
			else if (rand == 2)
				queue = Direction.left;
			else if (rand == 3)
				queue = Direction.right;
			else if (rand == 4)
				queue = Direction.upRight;
			else if (rand == 5)
				queue = Direction.downRight;
			else if (rand == 6)
				queue = Direction.downLeft;
			else if (rand == 7)
				queue = Direction.upLeft;

			//wait time
			yield return new WaitForSeconds (roundTime);

			if (leftEyeDir != Direction.closed && rightEyeDir != Direction.closed)
				yield return new WaitForSeconds (Mathf.Min(1f, Parameters.roundTime * 3f));

			if (leftEyeDir == queue)
				Parameters.AddLeftSuccess ();
			
			if (leftEyeDir == Direction.closed)
				Parameters.AddLeftMistake ();
			
			leftEyeDir = Direction.closed;

			if (rightEyeDir == queue)
				Parameters.AddRightSuccess ();

			if (rightEyeDir == Direction.closed)
				Parameters.AddRightMistake ();

			rightEyeDir = Direction.closed;

			queue = Direction.closed;

			freezeInput = true;

			if (Parameters.roundIndex > 4)
				Parameters.roundTime *= 0.95f;
			
			Parameters.roundIndex++;
			Parameters.roundTime = Mathf.Max (Parameters.roundTime, 0.6f);

			yield return new WaitForSeconds (pauseTime);
		}
		EndMatch ();
	}

	void CheckPlayerInput(Direction l_, Direction r_){
		if (queue != l_)
			Parameters.AddLeftMistake ();
		if (queue != r_)
			Parameters.AddRightMistake ();
		
		if (queue == l_ && queue == r_) {
			leftEyeDir = queue;
			rightEyeDir = queue;
		} 
		else {
			Direction temp = queue;
			int rand;
			while (temp == queue) {
				rand = Random.Range (4, 8);
				if (rand == 0)
					temp = Direction.up;
				else if (rand == 1)
					temp = Direction.down;
				else if (rand == 2)
					temp = Direction.left;
				else if (rand == 3)
					temp = Direction.right;
				else if (rand == 4)
					temp = Direction.upRight;
				else if (rand == 5)
					temp = Direction.downRight;
				else if (rand == 6)
					temp = Direction.downLeft;
				else if (rand == 7)
					temp = Direction.upLeft;
			}
			leftEyeDir = temp;
			temp = queue;
			while (temp == queue || temp == leftEyeDir) {
				rand = Random.Range (4, 8);
				if (rand == 0)
					temp = Direction.up;
				else if (rand == 1)
					temp = Direction.down;
				else if (rand == 2)
					temp = Direction.left;
				else if (rand == 3)
					temp = Direction.right;
				else if (rand == 4)
					temp = Direction.upRight;
				else if (rand == 5)
					temp = Direction.downRight;
				else if (rand == 6)
					temp = Direction.downLeft;
				else if (rand == 7)
					temp = Direction.upLeft;
			}
			rightEyeDir = temp;
		}
	}

	void GetInput(){
		//left
		Direction l_ = Direction.closed, r_ = Direction.closed;

		if (leftEyeDir == Direction.closed || leftEyeDir == Direction.semi) {
			/*
			if (Input.GetKey (KeyCode.Q)) {
				l_ = Direction.upLeft;
				leftEyeDir = Direction.semi;
			} else if (Input.GetKey (KeyCode.A)) {
				l_ = Direction.downLeft;
				leftEyeDir = Direction.semi;
			} else if (Input.GetKey (KeyCode.E)) {
				l_ = Direction.upRight;
				leftEyeDir = Direction.semi;
			} else if (Input.GetKey (KeyCode.S) && Input.GetKey (KeyCode.D)) {
				l_ = Direction.downRight;
				leftEyeDir = Direction.semi;
			} 
			*/
			if (Input.GetKey (KeyCode.W)) {
				l_ = Direction.up;
				leftEyeDir = Direction.semi;
			} else if (Input.GetKey (KeyCode.S)) {
				l_ = Direction.down;
				leftEyeDir = Direction.semi;
			} else if (Input.GetKey (KeyCode.D)) {
				l_ = Direction.right;
				leftEyeDir = Direction.semi;
			} else if (Input.GetKey (KeyCode.A)) {
				l_ = Direction.left;
				leftEyeDir = Direction.semi;
			}
			else {
				l_ = Direction.closed;
				leftEyeDir = Direction.closed;
			}
			//
		}

		//right
		if (rightEyeDir == Direction.closed || rightEyeDir == Direction.semi) {
			/*
			if (Input.GetKey (KeyCode.UpArrow) && Input.GetKey (KeyCode.LeftArrow)) {
				r_ = Direction.upLeft;
				rightEyeDir = Direction.semi;
			} else if (Input.GetKey (KeyCode.UpArrow) && Input.GetKey (KeyCode.RightArrow)) {
				r_ = Direction.upRight;
				rightEyeDir = Direction.semi;
			} else if (Input.GetKey (KeyCode.DownArrow) && Input.GetKey (KeyCode.RightArrow)) {
				r_ = Direction.downRight;
				rightEyeDir = Direction.semi;
			} else if (Input.GetKey (KeyCode.DownArrow) && Input.GetKey (KeyCode.LeftArrow)) {
				r_ = Direction.downLeft;
				rightEyeDir = Direction.semi;
			}
			*/
			if (Input.GetKey (KeyCode.UpArrow)) {
				r_ = Direction.up;
				rightEyeDir = Direction.semi;
			} else if (Input.GetKey (KeyCode.DownArrow)) {
				r_ = Direction.down;
				rightEyeDir = Direction.semi;
			} else if (Input.GetKey (KeyCode.RightArrow)) {
				r_ = Direction.right;
				rightEyeDir = Direction.semi;
			} else if (Input.GetKey (KeyCode.LeftArrow)) {
				r_ = Direction.left;
				rightEyeDir = Direction.semi;
			} 
			//
			else {
				r_ = Direction.closed;
				rightEyeDir = Direction.closed;
			}
			//
		}

		if (rightEyeDir == Direction.semi && leftEyeDir == Direction.semi && !freezeInput)
			CheckPlayerInput (l_, r_);
	}

	void Update(){
		GetInput ();
	}
}
