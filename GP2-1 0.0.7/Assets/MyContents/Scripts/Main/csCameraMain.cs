using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class csCameraMain : MonoBehaviour {

	Transform thisPos;

	public Transform initCameraPoint;
	public Transform CreditsCameraPoint;

	public GameObject creditText;

	bool isStart = false;

	public UnityEngine.UI.Image fade;
	float fadeTime = 2.0f;
	float fadeAlpha = 0.0f;

	// Use this for initialization
	void Start () {
		thisPos = initCameraPoint;
	}
	
	// Update is called once per frame
	void Update () {
		transform.position = Vector3.MoveTowards(transform.position, thisPos.position, Time.deltaTime * 1.0f);
		transform.rotation = Quaternion.Slerp(transform.rotation, thisPos.rotation, Time.deltaTime * 4.0f);

		if (isStart) {
			StartGame();
		}
	}

	public void GotoCredit() {
		creditText.SetActive(true);
		thisPos = CreditsCameraPoint;
	}

	public void GotoInit() {
		thisPos = initCameraPoint;
	}

	public void StartGame() {
		isStart = true;

		if (fadeAlpha < 1.0f)
		{
			fadeAlpha += (Time.deltaTime / fadeTime);
			fade.color = new Color(0.0f, 0.0f, 0.0f, fadeAlpha);
		}
		else if (fadeAlpha >= 1.0f) {
			SceneManager.LoadScene("Game");
		}
	}
}
