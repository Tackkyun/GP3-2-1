using UnityEngine;
using System.Collections;

public class csCCTVcol : MonoBehaviour {

	float hackedTimer = 0.0f;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (transform.parent.GetComponent<csCCTV>().ishacked) {
			HackedTimer();
		}
	}

	void OnTriggerStay(Collider col)
	{
		if (!transform.parent.GetComponent<csCCTV>().ishacked) return; //미해킹이면 리턴.
		if (col.transform.tag != "AgentZoneUnit") return;
		if (col.gameObject.name == "Guard")
		{
			Ray ray = new Ray(transform.parent.position, col.transform.position - transform.parent.position);
			RaycastHit hit;
			int layerMask = (-1) - ((1 << LayerMask.NameToLayer("AttRange")));

			Physics.Raycast(ray, out hit, 1000.0f, layerMask);

			if (hit.transform == col.transform)
			{
				//플레이어에게 보이기
				col.gameObject.layer = 9;
				col.transform.FindChild("Sprite").gameObject.layer = 9;
			}
		}
	}

	void HackedTimer() {
		hackedTimer += Time.deltaTime;

		if (hackedTimer >= 60.0f) {
			hackedTimer = 0.0f;
			transform.parent.GetComponent<csCCTV>().ishacked = false;
		}
	}

	void OnTriggerExit(Collider col)
	{
		if (col.transform.tag != "AgentZoneUnit") return;
		if (col.gameObject.name == "Guard")
		{
			Ray ray = new Ray(transform.parent.position, col.transform.position - transform.parent.position);
			RaycastHit hit;
			int layerMask = (-1) - ((1 << LayerMask.NameToLayer("AttRange")));

			Physics.Raycast(ray, out hit, 1000.0f, layerMask);

			if (hit.transform == col.transform)
			{
				//플레이어에게 숨기기
				col.gameObject.layer = 11;
				col.transform.FindChild("Sprite").gameObject.layer = 11;
			}
		}
	}
}
