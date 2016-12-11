using UnityEngine;
using System.Collections;

public class csDoor : MonoBehaviour {

	public float speed = 1.0f;

	Vector3 closedRotate;
	Vector3 toRotate;

	bool isOpened = false;

	// Use this for initialization
	void Start () {
		closedRotate = transform.localEulerAngles;
		toRotate = closedRotate;
	}
	
	// Update is called once per frame
	void Update () {
		if (isOpened)
		{
			toRotate = new Vector3(transform.localRotation.x, transform.localRotation.y, transform.localRotation.z + 90.0f);
		}
		else {
			toRotate = closedRotate;
		}

		transform.localEulerAngles = Vector3.Lerp(transform.localEulerAngles, toRotate, speed * Time.deltaTime);
	}

	public void ChangeDoorState() {
		isOpened = !isOpened;
	}
}
