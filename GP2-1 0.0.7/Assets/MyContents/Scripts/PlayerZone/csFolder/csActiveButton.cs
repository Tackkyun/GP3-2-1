using UnityEngine;
using System.Collections;

public class csActiveButton : MonoBehaviour
{
	bool isMouseExit = false;

	// Use this for initialization
	void Start()
	{

	}

	// Update is called once per frame
	void Update()
	{
	}

	void OnMouseDown() {
		if (transform.parent.parent.parent.GetComponent<csFolderMonitor>().isincreased == true)
		{
			transform.parent.parent.parent.parent.FindChild("MainLogo").GetComponent<csButtonAction>().OnMouseDown();
			transform.parent.parent.parent.parent.FindChild("MainLogoNomal").GetComponent<csButtonAction>().OnMouseDown();
		}
	}

	void OnMouseUp()
	{
		if (transform.parent.parent.parent.GetComponent<csFolderMonitor>().isincreased == true && !isMouseExit)
		{
			GameObject.Find("MainLogo").GetComponent<SpriteRenderer>().sortingOrder = 11;
			transform.parent.parent.parent.GetComponent<csFolderMonitor>().isMinimize = true;
			transform.parent.parent.parent.parent.FindChild("SetPage").GetComponent<csSetHackPage>().isActive = true;
		}

		transform.parent.parent.parent.parent.FindChild("MainLogo").GetComponent<csButtonAction>().OnMouseUp();
		transform.parent.parent.parent.parent.FindChild("MainLogoNomal").GetComponent<csButtonAction>().OnMouseUp();

		isMouseExit = false;
	}

	void OnMouseOver()
	{
		if (transform.parent.parent.parent.GetComponent<csFolderMonitor>().isincreased == true)
		{
			transform.parent.parent.parent.parent.FindChild("MainLogoNomal").GetComponent<csButtonAction>().OnMouseOver();
		}
	}

	void OnMouseExit() {
		if (transform.parent.parent.parent.GetComponent<csFolderMonitor>().isincreased == true)
		{
			transform.parent.parent.parent.parent.FindChild("MainLogo").GetComponent<csButtonAction>().OnMouseExit();
			transform.parent.parent.parent.parent.FindChild("MainLogoNomal").GetComponent<csButtonAction>().OnMouseExit();

			isMouseExit = true;
		}
	}
}
