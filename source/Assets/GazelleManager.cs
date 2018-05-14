using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GazelleManager : MonoBehaviour {

	void Start () {
		StartCoroutine (AnimationRightEar());
		StartCoroutine (AnimationLeftEar());
		StartCoroutine (AnimationNose ());
		StartCoroutine (AnimationLeftEye ());
		StartCoroutine (AnimationRightEye ());
	}

	public Camera mainCam;
	public SpriteRenderer rightEyeOpen, rightEyeClosed, leftEyeOpen, leftEyeClosed, mouthClosed, mouthOpen;
	public GameObject leftEar, rightEar;
	private float leftEarRot, rightEarRot;
	public GameObject rightEye, rightEyePupil, leftEye, leftEyePupil, mouthEye, mouthEyePupil;
	public GameObject rightSemi, leftSemi;
	public Animator nose;
	public Animator leftEyeAnim;
	public Animator rightEyeAnim;
	public AudioSource scream;
	public AudioSource music;
	private float radius = 1.13f; //hardcoded
	private float lerpSpeed = 10f;

	IEnumerator AnimationRightEar(){
		while (true) {
			rightEarRot = Random.Range (-6f, 3.24f);
			yield return new WaitForSeconds (Random.Range (1f, 2f));
		}
	}

	IEnumerator AnimationLeftEar(){
		while (true) {
			leftEarRot = Random.Range (-3.11f, 6.69f);
			yield return new WaitForSeconds (Random.Range (1f, 2f));
		}
	}

	IEnumerator AnimationNose(){
		int rand;
		while (true) {
			rand = Random.Range (0, 6);//hardcoded
			nose.SetInteger ("variation", Random.Range (0, 3));
			nose.SetBool ("special", rand == 0);
			yield return new WaitForSeconds (0.9f);
		}
	}

	IEnumerator AnimationLeftEye(){
		int rand;
		while (true) {
			rand = Random.Range (0, 6);//hardcoded
			leftEyeAnim.SetInteger("t",rand != 0? 0 : Random.Range(1,5));
			yield return new WaitForSeconds (Random.Range(0.5f,1f));
		}
	}

	IEnumerator AnimationRightEye(){
		int rand;
		while (true) {
			rand = Random.Range (0, 6);//hardcoded
			rightEyeAnim.SetInteger("t",rand != 0? 0 : Random.Range(1,5));
			yield return new WaitForSeconds (Random.Range(0.5f,1f));
		}
	}

	void Update () {
		float x = Mathf.Lerp(-0.1f, 0.1f, (Mathf.Cos(Time.time * 2)+1f)*0.5f);
		float y = Mathf.Lerp(0, -0.2f, (Mathf.Cos(Time.time * 3)+1f)*0.5f);
		transform.localPosition = new Vector3 (x, y, 0);
		float lerpClock = Time.deltaTime * lerpSpeed;

		rightEyeOpen.enabled = Play.rightEyeDir != Play.Direction.closed;
		rightEyeClosed.enabled = Play.rightEyeDir == Play.Direction.closed;
		leftEyeOpen.enabled = Play.leftEyeDir != Play.Direction.closed;
		leftEyeClosed.enabled = Play.leftEyeDir == Play.Direction.closed;
		mouthClosed.enabled = Play.queue == Play.Direction.closed;
		mouthOpen.enabled = Play.queue != Play.Direction.closed;
		mouthOpen.transform.GetChild (0).gameObject.SetActive (mouthOpen.enabled);

		Vector3 temp;
		if (Play.rightEyeDir == Play.Direction.down)
			temp = new Vector3 (0, -1f, 0);
		else if (Play.rightEyeDir == Play.Direction.left)
			temp = new Vector3 (-1, 0, 0);
		else if (Play.rightEyeDir == Play.Direction.right)
			temp = new Vector3 (1, 0, 0);
		else if (Play.rightEyeDir == Play.Direction.up)
			temp = new Vector3 (0, 1, 0);
		//
		else if (Play.rightEyeDir == Play.Direction.downRight)
			temp = new Vector3 (0.7f, -0.7f, 0);
		else if (Play.rightEyeDir == Play.Direction.upRight)
			temp = new Vector3 (0.7f, 0.7f, 0);
		else if (Play.rightEyeDir == Play.Direction.downLeft)
			temp = new Vector3 (-0.7f, -0.7f, 0);
		else if (Play.rightEyeDir == Play.Direction.upLeft)
			temp = new Vector3 (-0.7f, 0.7f, 0);
		else
			temp = Vector3.zero;
		temp *= radius;
		rightEyePupil.transform.localPosition = Vector3.Lerp (rightEyePupil.transform.localPosition, temp, lerpClock);

		if (Play.leftEyeDir == Play.Direction.down)
			temp = new Vector3 (0, -1f, 0);
		else if (Play.leftEyeDir == Play.Direction.left)
			temp = new Vector3 (-1, 0, 0);
		else if (Play.leftEyeDir == Play.Direction.right)
			temp = new Vector3 (1, 0, 0);
		else if (Play.leftEyeDir == Play.Direction.up)
			temp = new Vector3 (0, 1, 0);
		//
		else if (Play.leftEyeDir == Play.Direction.downRight)
			temp = new Vector3 (0.7f, -0.7f, 0);
		else if (Play.leftEyeDir == Play.Direction.upRight)
			temp = new Vector3 (0.7f, 0.7f, 0);
		else if (Play.leftEyeDir == Play.Direction.downLeft)
			temp = new Vector3 (-0.7f, -0.7f, 0);
		else if (Play.leftEyeDir == Play.Direction.upLeft)
			temp = new Vector3 (-0.7f, 0.7f, 0);
		else
			temp = Vector3.zero;
		temp *= radius;
		leftEyePupil.transform.localPosition = Vector3.Lerp (leftEyePupil.transform.localPosition, temp, lerpClock);

		if (Play.queue == Play.Direction.down)
			temp = new Vector3 (0, -1f, 0);
		else if (Play.queue == Play.Direction.left)
			temp = new Vector3 (-1, 0, 0);
		else if (Play.queue == Play.Direction.right)
			temp = new Vector3 (1, 0, 0);
		else if (Play.queue == Play.Direction.up)
			temp = new Vector3 (0, 1, 0);
		//
		else if (Play.queue == Play.Direction.downRight)
			temp = new Vector3 (0.7f, -0.7f, 0);
		else if (Play.queue == Play.Direction.upRight)
			temp = new Vector3 (0.7f, 0.7f, 0);
		else if (Play.queue == Play.Direction.downLeft)
			temp = new Vector3 (-0.7f, -0.7f, 0);
		else if (Play.queue == Play.Direction.upLeft)
			temp = new Vector3 (-0.7f, 0.7f, 0);
		else
			temp = Vector3.zero;
		temp *= radius;
		mouthEyePupil.transform.localPosition = Vector3.Lerp (mouthEyePupil.transform.localPosition, temp, lerpClock);

		leftEar.transform.localRotation = Quaternion.Slerp (leftEar.transform.localRotation, Quaternion.Euler (new Vector3 (0, 0, leftEarRot)), Time.deltaTime);
		rightEar.transform.localRotation = Quaternion.Slerp (rightEar.transform.localRotation, Quaternion.Euler (new Vector3 (0, 0, rightEarRot)), Time.deltaTime);

		if (Play.rightEyeDir != Play.Direction.closed && Play.leftEyeDir != Play.Direction.closed && Play.queue != Play.leftEyeDir && Play.queue != Play.rightEyeDir) {
			int rand = Random.Range (0, 2);
			rightEye.transform.localScale = Vector3.one * (rand == 0 ? 0.23f : 0.2f);
			rand = Random.Range (0, 2);
			leftEye.transform.localScale = Vector3.one * (rand == 0 ? 0.23f : 0.2f);
			rand = Random.Range (0, 2);
			mouthEye.transform.localScale = Vector3.one * (rand == 0 ? 0.23f : 0.2f);
		}

		if (Play.queue == Play.rightEyeDir && Play.queue == Play.leftEyeDir && Play.queue != Play.Direction.closed) {
			mainCam.GetComponent<MultiTextureChroma> ().invert += Time.deltaTime * 2f;// Mathf.CeilToInt (Time.time * 8f) % 2 == 0;
			//scream.pitch = -1.5f;
			scream.pitch = 1f;
			//music.pitch = Mathf.Lerp (music.pitch, 2f, Time.deltaTime);
		} else {
			mainCam.GetComponent<MultiTextureChroma> ().invert = 0f;
			//scream.pitch = 1f;
			scream.pitch = 0.46f;
			//music.pitch = 1f;
		}

		rightEye.SetActive (Play.rightEyeDir != Play.Direction.closed && Play.rightEyeDir != Play.Direction.semi);
		rightSemi.SetActive (Play.rightEyeDir == Play.Direction.semi);
		leftEye.SetActive (Play.rightEyeDir != Play.Direction.closed && Play.rightEyeDir != Play.Direction.semi);
		leftSemi.SetActive (Play.leftEyeDir == Play.Direction.semi);
		mouthEye.SetActive (mouthOpen.enabled);

		mainCam.orthographicSize = Mathf.Lerp (mainCam.orthographicSize, mouthOpen.enabled ? 5.5f : 7f, Time.deltaTime * 1f);
		//mainCam.transform.position = Vector3.Lerp (mainCam.transform.position, scream.volume < 1 ? new Vector3 (0, -0.35f, -10f) : new Vector3 (0, 0, -10f), Time.deltaTime * 1f);
	}
}
