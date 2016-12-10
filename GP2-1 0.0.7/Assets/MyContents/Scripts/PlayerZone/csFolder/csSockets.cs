using UnityEngine;
using System.Collections;

public class csSockets : MonoBehaviour {

	GameObject activeButton;

	public GameObject MainLogo;
	public GameObject MainLogo2;

	public int usedItem = 0;

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
		MainLogo.SetActive(true);
		MainLogo2.SetActive(true);
		activeButton.SetActive(true);
	}

	public void HideActiveButton() {
		MainLogo.SetActive(false);
		MainLogo2.SetActive(false);
		activeButton.SetActive(false);
	}

	public void DestroyItem() {
		for (int i = 0; i < 6; i++) {
			if (transform.FindChild("socket" + (i+1).ToString()).FindChild("File(Clone)") == null) continue;
				if (transform.FindChild("socket" + (i+1).ToString()).FindChild("File(Clone)").GetComponent<csFile>().isClicked) {
					transform.FindChild("socket" + (i+1).ToString()).FindChild("File(Clone)").GetComponent<csFile>().DestroyThis();
				}
		}
	}
}
