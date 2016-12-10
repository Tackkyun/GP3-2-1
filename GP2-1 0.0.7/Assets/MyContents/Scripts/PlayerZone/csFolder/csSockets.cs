using UnityEngine;
using System.Collections;

public class csSockets : MonoBehaviour {

	GameObject activeButton;

	public GameObject MainLogo;
	public GameObject MainLogo2;
	public GameObject MainLogo3;
	public GameObject MainLogo4;

	public int usedItem = 0;

	// Use this for initialization
	void Start () {
		CreateActiveButton();
		activeButton.SetActive(false);

		HideActiveButton();
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
		MainLogo3.SetActive(true);
		MainLogo4.SetActive(true);
		activeButton.SetActive(true);
	}

	public void HideActiveButton() {
		MainLogo.SetActive(false);
		MainLogo2.SetActive(false);
		MainLogo3.SetActive(false);
		MainLogo4.SetActive(false);
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
