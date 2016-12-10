using UnityEngine;
using System.Collections;

public class csPoint : MonoBehaviour {

	public float moveSpeed = 1.0f;
	float dir;

	Transform middlePos;

	// Use this for initialization
	void Start () {
		middlePos = transform.parent.FindChild("HackPageSprite");
	}
	
	// Update is called once per frame
	void Update () {
		dir = Input.GetAxis("Horizontal");
		transform.RotateAround(middlePos.position, transform.forward, Time.deltaTime * moveSpeed * dir);
	}
}
