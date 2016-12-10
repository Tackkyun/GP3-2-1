using UnityEngine;
using System.Collections;

public class csComputer : MonoBehaviour {
	public GameObject[] AZobject;
	bool isHacked = false;

	// Use this for initialization
	void Start () {
		AZobject = transform.parent.GetComponent<csComputerObject>().AZObject;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnTriggerStay(Collider col)
	{
		if (col.transform.tag != "AgentZoneUnit") return;
		if (isHacked) return;
		if (col.gameObject.name == "Agent")
		{
			GameObject.Find("sockets").GetComponent<csSockets>().SetActiveButton();
			if (GameObject.Find("HackPageSprite").GetComponent<csHackPage>().isSuccess) {
				isHacked = true;
				GameObject.Find("sockets").GetComponent<csSockets>().HideActiveButton();
				for (int i = 0; i < AZobject.Length; i++)
				{
					if (AZobject[i] != null)
					{
						if (AZobject[i].tag == "CCTV" && GameObject.Find("HackPageSprite").GetComponent<csHackPage>().GetHackTarget() == "cctv")
						{
							AZobject[i].GetComponent<csCCTV>().ishacked = true;
						}
						else if (AZobject[i].tag == "Door" && GameObject.Find("HackPageSprite").GetComponent<csHackPage>().GetHackTarget() == "door") {
							AZobject[i].GetComponent<csAZDoor>().ChangeStateHacked();
						}
					}
				}
			}
		}
	}

	void OnTriggerExit(Collider col)
	{
		if (col.transform.tag != "AgentZoneUnit") return;
		if (col.gameObject.name == "Agent")
		{
			GameObject.Find("sockets").GetComponent<csSockets>().HideActiveButton();
			GameObject.Find("HackPageSprite").GetComponent<csHackPage>().isAtted = true;
		}
	}
}
