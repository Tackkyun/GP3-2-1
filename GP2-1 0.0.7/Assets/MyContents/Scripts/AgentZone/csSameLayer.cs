using UnityEngine;
using System.Collections;

public class csSameLayer : MonoBehaviour {

	GameObject sameLayerTarget;

	// Use this for initialization
	void Start () {
		sameLayerTarget = transform.parent.FindChild("Sprite").gameObject;
	}
	
	// Update is called once per frame
	void Update () {
		gameObject.layer = sameLayerTarget.layer;
	}
}
