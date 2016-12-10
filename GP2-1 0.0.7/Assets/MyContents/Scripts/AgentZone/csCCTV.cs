using UnityEngine;
using System.Collections;

public class csCCTV : MonoBehaviour {

	bool isReturn = false;
	public float maxRot = 30.0f;
	public float rotSpeed = 10.0f;

	float curRotY;

	public bool ishacked = false;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (!isReturn)
		{
			//회전 범위를 넘는다면
			if (transform.localRotation.eulerAngles.y >= maxRot)
			{
				isReturn = true;
			}
			else {
				//회전
				transform.Rotate(0, Time.deltaTime * rotSpeed, 0);
			}
		}
		else if (isReturn) {
			//회전 범위를 넘는다면
			if (transform.localRotation.eulerAngles.y - (Time.deltaTime * rotSpeed) <= 0)
			{
				isReturn = false;
			}
			else {
				//회전
				transform.Rotate(0, -Time.deltaTime * rotSpeed, 0);
			}
		}
	}
}
