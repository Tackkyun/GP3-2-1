using UnityEngine;
using System.Collections;

public class csSetPageMouseOver : MonoBehaviour {

	GameObject normalButton;
	GameObject selectButton;

	bool isActive = false;
	bool isReady = false;

	// Use this for initialization
	void Start () {
		normalButton = transform.parent.FindChild( transform.name + "_" ).gameObject;
		selectButton = transform.parent.FindChild( transform.name + "_select" ).gameObject;

		selectButton.SetActive(false);
	}
	
	// Update is called once per frame
	void Update () {
		if (!isActive)
		{
			isActive = true;
			StartCoroutine(SetReady());
		}
	}

	IEnumerator SetReady()
	{
		yield return new WaitForSeconds(1.0f);
		isReady = true;
	}

	void OnMouseOver() {
		normalButton.SetActive(false);
		selectButton.SetActive(true);
	}

	void OnMouseExit()
	{
		normalButton.SetActive(true);
		selectButton.SetActive(false);
	}

	void OnMouseDown() {
		//미구현 버튼 미작동
		if (transform.name == "laptop_UI_hacking_communication" ||
			transform.name == "laptop_UI_hacking_shield") {
			return;
		}

		//다음페이지 작동. 버튼정보 전달.
		if (transform.parent.parent.GetComponent<csSetHackPage>().isincreased == true && isReady)
		{
			transform.parent.parent.GetComponent<csSetHackPage>().isMinimize = true;
			if (transform.name == "laptop_UI_hacking_cctv")
			{
				transform.parent.parent.parent.FindChild("HackPage").FindChild("HackPageSprite").
					GetComponent<csHackPage>().SetHackTarget("cctv");
			}
			else if (transform.name == "laptop_UI_hacking_communication")
			{
			}
			else if (transform.name == "laptop_UI_hacking_server")
			{
				transform.parent.parent.parent.FindChild("HackPage").FindChild("HackPageSprite").
					GetComponent<csHackPage>().SetHackTarget("door");
			}
			else if (transform.name == "laptop_UI_hacking_shield")
			{
			}
			else {
				Debug.Log("Error");
			}
			transform.parent.parent.parent.FindChild("HackPage").FindChild("HackPageSprite").GetComponent<csHackPage>().isActive = true;
			transform.parent.parent.parent.FindChild("HackPage").FindChild("HackPageSprite").GetComponent<csHackPage>().usedItem += GameObject.Find("sockets").GetComponent<csSockets>().usedItem;

			//transform.parent.parent.parent.FindChild("HackPage").FindChild("HackPageSprite").GetComponent<csHackPage>().Init();
			isActive = false;
			isReady = false;

			GameObject.Find("sockets").GetComponent<csSockets>().usedItem = 0;
			GameObject.Find("sockets").GetComponent<csSockets>().DestroyItem();
		}
	}
}
