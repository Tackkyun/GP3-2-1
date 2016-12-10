using UnityEngine;
using System.Collections;

public class csAgentCamera : MonoBehaviour {
	//GameObject Agent;

	float distance = 500;
	float zoomSpeed = 500;

	// Use this for initialization
	void Start () {
		//Agent = GameObject.Find("Agent");
	}
	
	// Update is called once per frame
	void Update () {
		/*transform.localPosition = Vector3.Lerp(transform.localPosition,
				(Agent.transform.position + new Vector3(0, distance, 0)), 3.0f * Time.deltaTime);*/
		if (distance < 300.0f)
		{
			distance = 300.0f;
		}
		else if (distance > 1200.0f) {
			distance = 1200.0f;
		}
		transform.GetComponent<Camera>().orthographicSize = distance;
	}

	void ZoomOut(float f_ZO) {
		distance += f_ZO * zoomSpeed;
	}
}
