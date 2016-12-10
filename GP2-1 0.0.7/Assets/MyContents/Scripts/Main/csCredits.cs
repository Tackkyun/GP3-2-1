using UnityEngine;
using System.Collections;

public class csCredits : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}

	void OnMouseDown() {
		GameObject.Find("Main Camera").GetComponent<csCameraMain>().GotoInit();
		transform.parent.gameObject.SetActive(false);
	}
}
