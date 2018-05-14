using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Parameters : MonoBehaviour {
	//cutomizable game
	public static float roundTime;
	public static float roundPauseTime;
	public static int totalRounds;
	public static int totalMatches;
	public static bool fourInputs;
	public static bool showRounds;
	public static bool showMatches;
	public static bool showTimer;

	//game stats
	public static int roundIndex;
	public static int matchIndex;
	public static List<int> rightEyeMistakes;
	public static List<int> rightEyeSuccess;
	public static List<int> leftEyeMistakes;
	public static List<int> leftEyeSuccess;

	//points
	public static int leftPoints;
	public static int rightPoints;

	//ui
	public Text leftResult, rightResult;
	public Slider leftMe;
	public Slider leftHim;
	public Slider rightMe;
	public Slider rightHim;

	public static void AddRightSuccess(){
		rightEyeSuccess [rightEyeSuccess.Count - 1]++;
	}

	public static void AddLeftSuccess(){
		leftEyeSuccess [leftEyeSuccess.Count - 1]++;
	}

	public static void AddRightMistake(){
		rightEyeMistakes [rightEyeMistakes.Count - 1]++;
	}

	public static void AddLeftMistake(){
		leftEyeMistakes [leftEyeMistakes.Count - 1]++;
	}

	public void ResetSliders(){
		leftMe.value = 15;
		leftHim.value = 15;
		rightMe.value = 15;
		rightHim.value = 15;
	}

	public void UpdateResults(){
		int positiveL = leftEyeSuccess [leftEyeSuccess.Count - 1];
		int negativeL = leftEyeMistakes [leftEyeMistakes.Count - 1];
		int positiveR = rightEyeSuccess [rightEyeSuccess.Count - 1];
		int negativeR = rightEyeMistakes [rightEyeMistakes.Count - 1];
		float total = (Mathf.Max ((positiveL + negativeL), (positiveR + negativeR)) * 1f);
		float accL = ((Mathf.Abs (leftMe.value - negativeL) + Mathf.Abs (leftHim.value - negativeR)) * 0.5f) / 30f;
		accL = 1 - accL;
		float accR = ((Mathf.Abs (rightMe.value - negativeR) + Mathf.Abs (rightHim.value - negativeL)) * 0.5f) / 30f;
		accR = 1 - accR;
		int ptsL = Mathf.Max (0, positiveL - negativeL);
		int ptsR = Mathf.Max (0, positiveR - negativeR);
		int rlptsL = Mathf.FloorToInt (accL * ptsL);
		int rlptsR = Mathf.FloorToInt (accR * ptsR);
		leftPoints += rlptsL;
		rightPoints += rlptsR;

		leftResult.text = 
			positiveL + " Successes\n"
			+ negativeL + " Mistakes\n"
			+ ptsL + " Points\n"
			+ "\n"
			+ "Mistake Prediction: " +
			(accL * 100f).ToString (".00")
			+"% Accurate\n"
			+"-"+(ptsL - rlptsL)+" Points Penality\n"
			+"\n"
			+"Gained Points: "+rlptsL+"\n"
			+"Previous Points: "+(leftPoints - rlptsL)+"\n"
			+"Total Points: "+leftPoints;
		
		rightResult.text = 
			positiveR+" Successes\n"
			+negativeR+" Mistakes\n"
			+ptsR+" Points\n"
			+"\n"
			+"Mistake Prediction: "+
			(accR*100f).ToString (".00") 
			+"% Accurate\n"
			+"-"+(ptsR - rlptsR)+" Points Penality\n"
			+"\n"
			+"Gained Points: "+rlptsR+"\n"
			+"Previous Points: "+(rightPoints - rlptsR)+"\n"
			+"Total Points: "+rightPoints;
	}

}
