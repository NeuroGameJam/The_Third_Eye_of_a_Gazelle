using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Slider_ : MonoBehaviour {

	public Text text;
	public Slider slider;
	public float multiplier = 1;
	public string parameter;
	public bool isTrueFalse;

	void Start () {
		
	}

	void Update () {
		text.text = isTrueFalse? (slider.value == 0? "No" : "Yes") : (multiplier * slider.value).ToString(parameter);
	}
}
