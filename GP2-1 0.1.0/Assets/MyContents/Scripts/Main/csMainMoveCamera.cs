using UnityEngine;
using System.Collections;

public class csMainMoveCamera : MonoBehaviour {
	Transform thisPos;
	Transform childPos;

	public float moveSpeed = 1.0f;

	// Use this for initialization
	void Start () {
		thisPos = transform.FindChild("Main Camera").GetComponent<csCameraMain>().initCameraPoint;
		childPos = transform.FindChild("Main Camera");
		Cursor.lockState = CursorLockMode.Locked; //커서 고정
	}
	
	// Update is called once per frame
	void Update () {
		if (!(transform.position.x < thisPos.position.x + 0.001f &&
			transform.position.x > thisPos.position.x - 0.001f &&
			transform.position.y < thisPos.position.y + 0.001f &&
			transform.position.y > thisPos.position.y - 0.001f &&
			transform.position.z < thisPos.position.z + 0.001f &&
			transform.position.z > thisPos.position.z - 0.001f)) return;

		RotCameraVerticality();
		RotCamera();
	}

	void RotCameraVerticality()
	{
		transform.FindChild("Main Camera").Rotate(-Input.GetAxis("Mouse Y") * Time.deltaTime * 50.0f * moveSpeed, 0, 0);
	}

	void RotCamera()
	{
		//-------------------------------
		//카메라 회전
		//-------------------------------
		//이전 z축 회전을 저장
		float preRotZ = transform.localRotation.z;
		//마우스 입력을 기준으로 회전
		transform.Rotate(0, Input.GetAxis("Mouse X") * Time.deltaTime * 50.0f * moveSpeed, 0);
		//이전 z축 회전을 불러와 z축 고정
		transform.localRotation = Quaternion.Euler(transform.eulerAngles.x, transform.eulerAngles.y, preRotZ);
	}
}
