using UnityEngine;
using System.Collections;

public class csAZDoor : MonoBehaviour {

	bool isHacked = false;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (isHacked)
		{
			transform.localScale = Vector3.Lerp(
				transform.localScale,
				new Vector3(0.0f, transform.localScale.y, transform.localScale.z),
				Time.deltaTime);
			if (transform.localScale.x < 0.03f) {
				transform.localScale = new Vector3(0.0f, transform.localScale.y, transform.localScale.z);
				isHacked = false;
			}
		}
	}

	public void ChangeStateHacked() {
		isHacked = true;
	}
}
