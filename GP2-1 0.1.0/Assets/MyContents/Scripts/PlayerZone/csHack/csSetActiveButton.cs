using UnityEngine;
using System.Collections;

public class csSetActiveButton : MonoBehaviour {

	bool isActive = false;
	bool isReady = false;

	bool isMouseExit = false;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (!isActive) {
			isActive = true;
			StartCoroutine(SetReady());
		}
	}

	IEnumerator SetReady()
	{
		yield return new WaitForSeconds(1.0f);
		isReady = true;
	}

	void OnMouseDown() {
		if (transform.parent.GetComponent<csSetHackPage>().isincreased == true && isReady)
		{
			transform.parent.parent.FindChild("MainLogo").GetComponent<csButtonAction>().OnMouseDown();
			transform.parent.parent.FindChild("MainLogoNomal").GetComponent<csButtonAction>().OnMouseDown();
		}
	}

	public void OnMouseUp()
	{
		if (transform.parent.GetComponent<csSetHackPage>().isincreased == true && isReady && !isMouseExit)
		{
			GameObject.Find("MainLogo").GetComponent<SpriteRenderer>().sortingOrder = 9;
			transform.parent.GetComponent<csSetHackPage>().isMinimize = true;
			//transform.parent.parent.FindChild("HackPage").FindChild("HackPageSprite").GetComponent<csHackPage>().isActive = true; //hackpage활성화
			GameObject.Find("FolderPage").GetComponent<csFolderMonitor>().isActive = true;
			isActive = false;
			isReady = false;

			transform.parent.parent.FindChild("MainLogo").GetComponent<csButtonAction>().OnMouseUp();
			transform.parent.parent.FindChild("MainLogoNomal").GetComponent<csButtonAction>().OnMouseUp();
		}

		isMouseExit = false;
	}

	void OnMouseOver() {
		transform.parent.parent.FindChild("MainLogoNomal").GetComponent<csButtonAction>().OnMouseOver();
	}

	void OnMouseExit() {
		if (transform.parent.GetComponent<csSetHackPage>().isincreased == true && isReady)
		{
			transform.parent.parent.FindChild("MainLogo").GetComponent<csButtonAction>().OnMouseExit();
			transform.parent.parent.FindChild("MainLogoNomal").GetComponent<csButtonAction>().OnMouseExit();

			isMouseExit = true;
		}
	}
}
