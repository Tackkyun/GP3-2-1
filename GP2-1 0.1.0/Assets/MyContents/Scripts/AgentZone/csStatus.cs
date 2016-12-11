using UnityEngine;
using System.Collections;

public class csStatus : MonoBehaviour {
	float DelayTime = 0.0f;

	public bool isDead = false;
	bool canAtt = false; //공격 가능여부
	bool attWait = true; //딜레이 요청후 코루틴 재사용 방지

	public float hp = 1.0f;
	public float att = 1.0f;
	public float attDelay = 1.0f;
	public float range = 200.0f;

	// Use this for initialization
	void Awake() {
		transform.FindChild("Range").GetComponent<SphereCollider>().radius = range;
	}
	
	// Update is called once per frame
	void FixedUpdate() {
		
	}

	void OnTriggerStay(Collider col) {
		if (col.transform.tag != "AgentZoneUnit") return;
		if (col.transform.name == transform.name) return;  //우군일 경우 제외

		DelayTime = Time.deltaTime;

		if (col.transform.GetComponent<csStatus>().isActiveAndEnabled) {
			if (canAtt)
			{
				col.transform.SendMessage("ApplyDamage", att, SendMessageOptions.DontRequireReceiver); //공격
				canAtt = false; // 재조준 상태
			}
			else if (!canAtt && attWait) {
				StartCoroutine(SendAtt()); // 공격 딜레이
			}
			if (col.GetComponent<csStatus>().isDead) { //사망체크
				OnTriggerExit(col);
			}
		}
	}

	void OnTriggerExit(Collider col) {
		if (col.transform.tag != "AgentZoneUnit") return;
		if (col.transform.name == transform.name) return;  //우군일 경우 제외

	}

	IEnumerator SendAtt() {
		attWait = false;
		yield return new WaitForSeconds(attDelay);
		attWait = true;
		canAtt = true;
	}

	void ApplyDamage(int Damage)
	{
		hp -= Damage;

		if (hp <= 0)
		{
			if(transform.name == "Agent") GameObject.Find("GameEnd").GetComponent<csGameEnd>().isDead = true;
			DeadThis();
		}
	}

	void DeadThis() {
		isDead = true;
		Destroy(transform.parent.gameObject);
	}
}
