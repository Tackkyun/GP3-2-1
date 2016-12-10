using UnityEngine;
using System.Collections;

public class csSockets : MonoBehaviour {

	GameObject activeButton;

	// Use this for initialization
	void Start () {
		CreateActiveButton();
		activeButton.SetActive(false);
	}
	
	// Update is called once per frame
	void Update () {
	}

	public void CreateActiveButton() {
		activeButton = (GameObject)Instantiate(
					Resources.Load("ActivateButton"),
					transform.position,
					transform.rotation
					);
		activeButton.transform.parent = transform;
	}

	public void SetActiveButton() {
		activeButton.SetActive(true);
	}

	public void HideActiveButton() {
		activeButton.SetActive(false);
	}
}
