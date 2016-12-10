using UnityEngine;
using System.Collections;

public class csSpriteRotLock : MonoBehaviour {

	Transform parentRot;

	// Use this for initialization
	void Start () {
		parentRot = transform.parent.parent;
	}
	
	// Update is called once per frame
	void Update () {
		transform.eulerAngles = new Vector3(parentRot.eulerAngles.x + 90.0f, parentRot.eulerAngles.y, parentRot.eulerAngles.z);
	}
}
