using UnityEngine;
using System.Collections;

public class csControlMap : MonoBehaviour {

	public float moveSpeed = 1.0f;
	public GameObject MapCamera;

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnMouseOver() {
		if (Cursor.lockState != CursorLockMode.Locked)
		{
			//휠 줌인아웃
			if (MapCamera.GetComponent<Camera>().orthographicSize >= 300.0f)
			{
				GameObject.Find("MapCamera").GetComponent<csAgentCamera>()
					.SendMessage("ZoomOut", -Input.GetAxis("Mouse ScrollWheel"), SendMessageOptions.DontRequireReceiver);
			}

			//카메라 이동
			MapCamera.transform.Translate(Vector3.right * Time.deltaTime * Input.GetAxis("Horizontal") * Mathf.Pow(MapCamera.GetComponent<Camera>().orthographicSize, 1.5f) / 30.0f * moveSpeed);
			MapCamera.transform.Translate(Vector3.up * Time.deltaTime * Input.GetAxis("Vertical") * Mathf.Pow(MapCamera.GetComponent<Camera>().orthographicSize, 1.5f) / 30.0f * moveSpeed);
		}
	}
}
