using UnityEngine;
using System.Collections;

public class csAgentCamera : MonoBehaviour {
	//GameObject Agent;

	float distance = 500;
	float zoomSpeed = 500;

	GameObject endPos;

	// Use this for initialization
	void Start () {
		endPos = GameObject.Find("EndCam").transform.FindChild("Pos").gameObject;
		//Agent = GameObject.Find("Agent");
	}
	
	// Update is called once per frame
	void Update () {
		if (GameObject.Find("GameEnd").GetComponent<csGameEnd>().isEnd == true)
		{
			transform.position = Vector3.MoveTowards(transform.position, 
				new Vector3(endPos.transform.position.x, transform.position.y, endPos.transform.position.z), 
				Time.deltaTime * 500.0f);

			if (distance <= 1500)
			{
				distance += Time.deltaTime * 300;
			}
			transform.GetComponent<Camera>().orthographicSize = distance;
		}
		else {

			/*transform.localPosition = Vector3.Lerp(transform.localPosition,
					(Agent.transform.position + new Vector3(0, distance, 0)), 3.0f * Time.deltaTime);*/
			if (distance < 300.0f)
			{
				distance = 300.0f;
			}
			else if (distance > 1200.0f)
			{
				distance = 1200.0f;
			}
			transform.GetComponent<Camera>().orthographicSize = distance;
		}
	}

	void ZoomOut(float f_ZO) {
		distance += f_ZO * zoomSpeed;
	}
}
