using UnityEngine;
using System.Collections;

public class csActiveMainBackGround : MonoBehaviour {

	public GameObject backGround;

	// Use this for initialization
	void Start () {
		
	}

	void OnMouseOver() {
		backGround.SetActive(true);
	}

	void OnMouseExit() {
		backGround.SetActive(false);
	}

	void OnMouseDown() {
		switch (transform.parent.name) {
			case "StartText":
				GameObject.Find("Main Camera").GetComponent<csCameraMain>().StartGame();
				break;
			case "CreditsText":
				GameObject.Find("Main Camera").GetComponent<csCameraMain>().GotoCredit();
				break;
			case "ExitText":
				Application.Quit();
				break;
		}
	}
}
