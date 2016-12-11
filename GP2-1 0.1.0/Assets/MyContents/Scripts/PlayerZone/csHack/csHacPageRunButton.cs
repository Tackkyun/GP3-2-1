﻿using UnityEngine;
using System.Collections;

public class csHacPageRunButton : MonoBehaviour {

	Transform hackPageObject;

	// Use this for initialization
	void Start () {
		hackPageObject = transform.parent.FindChild("HackPageSprite");
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnMouseEnter() {
		hackPageObject.GetComponent<csHackPage>().isClickRunButtonDown = false;
		hackPageObject.GetComponent<csHackPage>().isClickRunButtonUp = false;
	}

	void OnMouseOver() {
		if (hackPageObject.GetComponent<csHackPage>().isActive == true &&
			transform.parent.localScale == Vector3.one)
		{
			if (Input.GetMouseButtonDown(0))
			{
				hackPageObject.GetComponent<csHackPage>().isClickRunButtonDown = true;
				hackPageObject.GetComponent<csHackPage>().isClickRunButtonUp = false;
			}
			else if (Input.GetMouseButtonUp(0))
			{
				hackPageObject.GetComponent<csHackPage>().isClickRunButtonDown = false;
				hackPageObject.GetComponent<csHackPage>().isClickRunButtonUp = true;
			}
		}

		transform.parent.parent.FindChild("MainLogo").GetComponent<csButtonAction>().OnMouseOver();
	}

	void OnMouseExit()
	{
		if (hackPageObject.GetComponent<csHackPage>().isActive == true &&
			transform.parent.localScale == Vector3.one)
		{
			hackPageObject.GetComponent<csHackPage>().isClickRunButtonDown = false;
			hackPageObject.GetComponent<csHackPage>().isClickRunButtonUp = true;
		}

		transform.parent.parent.FindChild("MainLogo").GetComponent<csButtonAction>().OnMouseExit();
		transform.parent.parent.FindChild("MainLogoNomal").GetComponent<csButtonAction>().OnMouseExit();
	}

	void OnMouseDown() {
		transform.parent.parent.FindChild("MainLogo").GetComponent<csButtonAction>().OnMouseDown();
		transform.parent.parent.FindChild("MainLogoNomal").GetComponent<csButtonAction>().OnMouseDown();
	}

	void OnMouseUp() {
		transform.parent.parent.FindChild("MainLogo").GetComponent<csButtonAction>().OnMouseUp();
		transform.parent.parent.FindChild("MainLogoNomal").GetComponent<csButtonAction>().OnMouseUp();
	}
}