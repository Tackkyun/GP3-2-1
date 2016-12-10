using UnityEngine;
using System.Collections;

public class csFolderMonitor : MonoBehaviour {

	public bool isMinimize = false;
	public bool isActive = true;
	public bool isincreased = true;
	public float zoomSpeed = 0.000001f;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		if (isMinimize)
		{
			Minimize();
		}
		else if (isActive) {
			Active();
		}
	}

	void Minimize() {
		isActive = false;
		transform.localScale = Vector3.Lerp(transform.localScale, Vector3.zero, Time.deltaTime * 3.0f);
		if (transform.localScale.x <= 0.3f) {
			isMinimize = false;
			isincreased = false;
			transform.localScale = Vector3.zero;
		}
	}

	void Active() {
		transform.localScale = Vector3.Lerp(transform.localScale, Vector3.one, Time.deltaTime * 3.0f);
		if (transform.localScale.x >= 0.97f)
		{
			isincreased = true;
			transform.localScale = Vector3.one;
		}
	}
}
