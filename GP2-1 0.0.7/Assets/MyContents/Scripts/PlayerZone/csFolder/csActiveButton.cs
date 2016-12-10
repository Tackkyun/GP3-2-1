using UnityEngine;
using System.Collections;

public class csActiveButton : MonoBehaviour
{


	// Use this for initialization
	void Start()
	{

	}

	// Update is called once per frame
	void Update()
	{
	}

	void OnMouseDown()
	{
		if (transform.parent.parent.parent.GetComponent<csFolderMonitor>().isincreased == true)
		{
			GameObject.Find("MainLogo").GetComponent<SpriteRenderer>().sortingOrder = 11;
			transform.parent.parent.parent.GetComponent<csFolderMonitor>().isMinimize = true;
			transform.parent.parent.parent.parent.FindChild("SetPage").GetComponent<csSetHackPage>().isActive = true;
		}
	}
}
