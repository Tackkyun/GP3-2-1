using UnityEngine;
using System.Collections;

public class csMonitorLeft : MonoBehaviour {

	public GameObject[] socketPos;		//파일/폴더 소켓의 위치
    public GameObject thisFolder;       //현재 표시되는 폴더

	GameObject pos;
	public GameObject pos1;
	public GameObject pos2;

	float moveSpeed = 0.12f;

	public bool Rot;
	public float rotData = 60.0f;

	// Use this for initialization
	void Start () {
		socketPos = new GameObject[6];
		thisFolder = (GameObject)Instantiate(Resources.Load("Folder"));
		thisFolder.name = "TopFolder";
		thisFolder.transform.parent = GameObject.Find("FolderMonitor").transform;
        thisFolder.transform.position = new Vector3(0, -10000, 0);
        thisFolder.GetComponent<csFolder>().isHighFolder = true;

		//위치설정
		for (int i = 0; i < 6; i++) {
			string s = "socket" + (i + 1).ToString();
			socketPos[i] = transform.parent.FindChild("sockets").FindChild(s).gameObject;
		}

		Rot = false;
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown(KeyCode.P)) {
			createFolder(null);
		}
        if (Input.GetKeyDown(KeyCode.O)){
            createFile(null);
        }

		if(Rot == true) { Rot = RotationRight(); };
	}

	//폴더 생성
	[ContextMenu("CreateFodler")]
	public GameObject createFolder(GameObject thisInFolder) {
		if (thisInFolder == null)
		{
			thisInFolder = thisFolder;
		}

		for (int i = 0; i < 6; i++) {

			//해당 소켓이 비어있으면 폴더 삽입 후 루프종료
			if (thisInFolder.GetComponent<csFolder>().socket[i] == null)
			{
				thisInFolder.GetComponent<csFolder>().socket[i] = (GameObject)Instantiate(
					Resources.Load("Folder"),
					socketPos[i].transform.position,
					Quaternion.Euler(socketPos[i].transform.eulerAngles.x,
									socketPos[i].transform.eulerAngles.y + 180.0f,
									socketPos[i].transform.eulerAngles.z)
					);
				thisInFolder.GetComponent<csFolder>().socket[i].transform.parent = socketPos[i].transform;

				thisInFolder.GetComponent<csFolder>().socket[i].GetComponent<csFolder>().isHighFolder = false;

				return thisInFolder.GetComponent<csFolder>().socket[i];
			}
			else {
				//Debug.Log("Folder is full");
			}
		}

		return null;
	}

	public GameObject test;

    //파일 생성
    [ContextMenu("CreateFile")]
	public GameObject createFile(GameObject thisInFolder) {
		if (thisInFolder == null) {
			thisInFolder = thisFolder;
		}

		test = thisInFolder;

		for (int i = 0; i < 6; i++)
        {
			//해당 소켓이 비어있으면 파일 삽입 후 루프종료
			if (thisInFolder.GetComponent<csFolder>().socket[i] == null)
			{
				thisInFolder.GetComponent<csFolder>().socket[i] = (GameObject)Instantiate(
					Resources.Load("File"),
					socketPos[i].transform.position,
					Quaternion.Euler(socketPos[i].transform.eulerAngles.x,
									socketPos[i].transform.eulerAngles.y + 180.0f,
									socketPos[i].transform.eulerAngles.z)
					);
				return thisInFolder.GetComponent<csFolder>().socket[i];
			}
			else if(i >= 6){
				Debug.Log("File is full");
			}
        }

		return null;
    }

	[ContextMenu("MoveSide")]
	public void MoveSide() {
		pos = pos2;
	}

	[ContextMenu("MoveMain")]
	public void MoveMain() {
		pos = pos1;
	}

	int stateRot = 1;
	float curRotZ;

	bool RotationRight() {
		if (stateRot == 1){
			curRotZ = Mathf.Round(transform.localEulerAngles.z);

            stateRot++;
		}
		else {
			transform.localEulerAngles = Vector3.Lerp(transform.localEulerAngles, new Vector3(transform.localEulerAngles.x, transform.localEulerAngles.y, transform.localEulerAngles.z + rotData), 0.05f);

            if (rotData == 60.0f)
			{
				if (Mathf.Round(transform.localEulerAngles.z) + 1000 >= ((curRotZ + rotData) % 360) + 1000)
				{
					transform.localEulerAngles = new Vector3(0, transform.localEulerAngles.y, curRotZ + rotData);
					stateRot = 1;
					return false;
				}
			}
			else {
				if (Mathf.Round(transform.localEulerAngles.z) + 1000 <= ((curRotZ + rotData) % 360) + 1000)
				{
					transform.localEulerAngles = new Vector3(0, transform.localEulerAngles.y, curRotZ + rotData);
					stateRot = 1;
					return false;
				}
			}
		}
		return true;
		
	}

    //폴더 변경
    void SetFolder(GameObject folder) {
		
		//기존 폴더 감추기
		for (int i = 0; i < 6; i++){
			if(thisFolder.GetComponent<csFolder>().socket[i] != null)
				thisFolder.GetComponent<csFolder>().socket[i].SetActive(false);
		}

		//현재 폺더를 띄울 것으로 변경
		thisFolder = folder;

		for (int i = 0; i < 6; i++)
		{
			if (thisFolder.GetComponent<csFolder>().socket[i] != null)
				thisFolder.GetComponent<csFolder>().socket[i].SetActive(true);
		}
	}

	void OnMouseDown() {
		if (Input.GetMouseButtonDown(1)) {
			Debug.Log("hi");
		}
		Debug.Log("hi2");
	}

}
