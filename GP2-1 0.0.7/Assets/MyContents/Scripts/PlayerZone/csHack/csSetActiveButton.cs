using UnityEngine;
using System.Collections;

public class csSetActiveButton : MonoBehaviour {

	bool isActive = false;
	bool isReady = false;

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

	void OnMouseDown()
	{
		if (transform.parent.GetComponent<csSetHackPage>().isincreased == true && isReady)
		{
			transform.parent.GetComponent<csSetHackPage>().isMinimize = true;
			//transform.parent.parent.FindChild("HackPage").FindChild("HackPageSprite").GetComponent<csHackPage>().isActive = true; //hackpage활성화
			GameObject.Find("FolderPage").GetComponent<csFolderMonitor>().isActive = true;
			isActive = false;
			isReady = false;
		}
	}
}
