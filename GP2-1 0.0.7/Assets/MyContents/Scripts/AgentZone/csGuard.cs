using UnityEngine;
using System.Collections;

public class csGuard : MonoBehaviour
{
	GameObject guardView;

	Transform startPos;
	Transform returnPos;
	Transform playerAgent;

	Transform targetPos;

	private NavMeshAgent agent;

	GameObject GameEnd;

	// Use this for initialization
	void Awake()
	{
		GameEnd = GameObject.Find("GameEnd");

		transform.GetComponent<NavMeshAgent>().ActivateCurrentOffMeshLink(false);

		startPos = transform.parent.FindChild("StartPoint");
		returnPos = transform.parent.FindChild("ReturnPoint");
		playerAgent = GameObject.Find("Agent").transform;

		agent = GetComponent<NavMeshAgent>();

		transform.position = startPos.position;
		targetPos = startPos;

		transform.GetComponent<NavMeshAgent>().ActivateCurrentOffMeshLink(true);

		guardView = transform.FindChild("agent_view").gameObject;
	}

	// Update is called once per frame
	void FixedUpdate()
	{
		if (playerAgent == null) return;

		Ray ray = new Ray(transform.position, playerAgent.position - transform.position);
		RaycastHit hit;
		RaycastHit hit2;

		int layerMask = (-1) - ((1 << LayerMask.NameToLayer("AttRange")));

		Physics.Raycast(ray, out hit, 500.0f, layerMask);
		Physics.Raycast(ray, out hit2, 1500.0f, layerMask);

		if (hit.transform == playerAgent)
		{
			//플레이어에게 보이기
			gameObject.layer = 9;
			transform.FindChild("Sprite").gameObject.layer = 9;

			agent.speed = playerAgent.GetComponent<NavMeshAgent>().speed;
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

		if (hit2.transform == playerAgent)
		{
			//플레이어에게 보이기
			gameObject.layer = 9;
			transform.FindChild("Sprite").gameObject.layer = 9;
		}

		if (GameEnd.GetComponent<csGameEnd>().isEnd)
		{
			agent.speed = 400;
			targetPos = playerAgent;
		}

		agent.destination = targetPos.position;
	}
}