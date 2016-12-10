using UnityEngine;
using System.Collections;

public class csGuard : MonoBehaviour
{

	Transform startPos;
	Transform returnPos;
	Transform playerAgent;

	Transform targetPos;

	private NavMeshAgent agent;

	// Use this for initialization
	void Awake()
	{
		transform.GetComponent<NavMeshAgent>().ActivateCurrentOffMeshLink(false);

		startPos = transform.parent.FindChild("StartPoint");
		returnPos = transform.parent.FindChild("ReturnPoint");
		playerAgent = GameObject.Find("Agent").transform;

		agent = GetComponent<NavMeshAgent>();

		transform.position = startPos.position;
		targetPos = startPos;

		transform.GetComponent<NavMeshAgent>().ActivateCurrentOffMeshLink(true);
	}

	// Update is called once per frame
	void FixedUpdate()
	{
		Ray ray = new Ray(transform.position, playerAgent.position - transform.position);
		RaycastHit hit;

		int layerMask = (-1) - ((1 << LayerMask.NameToLayer("AttRange")));

		Physics.Raycast(ray, out hit, 1000.0f, layerMask);

		if (hit.transform == playerAgent)
		{
			//플레이어에게 보이기
			gameObject.layer = 9;
			transform.FindChild("Sprite").gameObject.layer = 9;

			agent.speed = 250;
			targetPos = playerAgent;
			if (Vector3.Distance(targetPos.position, transform.position) < transform.GetComponent<csStatus>().range)
			{
				agent.speed = 0;
			}
		}
		else
		{
			//플레이어에게 숨기기
			gameObject.layer = 11;
			transform.FindChild("Sprite").gameObject.layer = 11;

			agent.speed = 100;

			if (targetPos == playerAgent) targetPos = startPos;

			if (Vector3.Distance(
					new Vector3(targetPos.position.x, 0, targetPos.position.z),
					new Vector3(transform.position.x, 0, transform.position.z)) < 1.0f)
			{

				if (targetPos == returnPos) targetPos = startPos;
				else if (targetPos == startPos) targetPos = returnPos;
				else Debug.Log("csGuard targetPos Error");
			}
		}

		agent.destination = targetPos.position;

	}
}