using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class csPlayer : MonoBehaviour {

	float fadeTime = 2.0f;
	float fadeAlpha = 1.0f;

	enum PlayerState {
		BeforeStart = 1 << 0,
		WalkState = 1 << 1,
		UsingComputerRotState = 1 << 2,
		UsingComputerKMState = 1 << 3
	}

	PlayerState playerState;

	GameObject colMapMonitor;
	GameObject colFolderMonitor;
	GameObject colCommuMonitor;
	GameObject EdgeMapMonitor;
	GameObject EdgeFolderMonitor;
	GameObject EdgeCommuMonitor;

	string hitName = null;

	float moveSpeed = 1.0f;
	GameObject usingComputerPoint;

	RaycastHit hitInfo;

	GameObject ui_ButtonGuide;
	GameObject ui_PlayerMode;

	float WalkPosY;
	Quaternion WalkRot;

	public Image fade;

	bool rotLock = false;

	// Use this for initialization
	void Start () {
		fade.color = new Color(0.0f, 0.0f, 0.0f, fadeAlpha);

		colMapMonitor = GameObject.Find("ChoiseMonitorObject").transform.FindChild("ColMapMonitor").gameObject;
		colFolderMonitor = GameObject.Find("ChoiseMonitorObject").transform.FindChild("ColFolderMonitor").gameObject;
		colCommuMonitor = GameObject.Find("ChoiseMonitorObject").transform.FindChild("ColCommuMonitor").gameObject;

		EdgeMapMonitor = GameObject.Find("EdgeMonitor").transform.FindChild("EdgeMapMonitor").gameObject;
		EdgeFolderMonitor = GameObject.Find("EdgeMonitor").transform.FindChild("EdgeFolderMonitor").gameObject;
		EdgeCommuMonitor = GameObject.Find("EdgeMonitor").transform.FindChild("EdgeCommuMonitor").gameObject;
		EdgeMapMonitor.SetActive(false);
		EdgeFolderMonitor.SetActive(false);
		EdgeCommuMonitor.SetActive(false);

		playerState = PlayerState.BeforeStart;
		usingComputerPoint = GameObject.Find("UsingComputerPoint");
		ui_ButtonGuide = GameObject.Find("UI").transform.FindChild("ButtonGuide").gameObject;
		ui_PlayerMode = GameObject.Find("UI").transform.FindChild("PlayerMode").gameObject;
		WalkPosY = transform.position.y;
		WalkRot = transform.localRotation;
	}

	// Update is called once per frame
	void Update() {

		switch (playerState) {
			case PlayerState.BeforeStart:
				if (fadeAlpha > 0.0f)
				{
					fadeAlpha -= (Time.deltaTime / fadeTime);
					fade.color = new Color(0.0f, 0.0f, 0.0f, fadeAlpha);
				}
				else if (fadeAlpha <= 0.0f)
				{
					playerState = PlayerState.WalkState;
				}
				break;
			case PlayerState.WalkState:
				if(GameObject.Find("UIManager").GetComponent<csUI>().GetUIState() != "ESC")
					Cursor.lockState = CursorLockMode.Locked; //커서 고정
				ui_PlayerMode.GetComponent<Text>().text = "WalkState";
				transform.position = Vector3.MoveTowards(transform.position, new Vector3(transform.position.x, WalkPosY, transform.position.z), moveSpeed * Time.deltaTime);
				transform.FindChild("Verticality").position = Vector3.MoveTowards(transform.FindChild("Verticality").position, transform.FindChild("WalkPoint").position, moveSpeed * Time.deltaTime);
				RotCamera();
				RotCameraVerticality();
				WalkCamera();
				ControlStateWalk();
				EdgeMapMonitor.SetActive(false);
				EdgeFolderMonitor.SetActive(false);
				EdgeCommuMonitor.SetActive(false);
				break;
			case PlayerState.UsingComputerRotState:
				if (GameObject.Find("UIManager").GetComponent<csUI>().GetUIState() != "ESC")
					Cursor.lockState = CursorLockMode.Locked; //커서 고정
				ui_PlayerMode.GetComponent<Text>().text = "UsingComputer_RotState";
				RotCamera();
				//RotCameraVerticality();
				ControlStateUCR();
				break;
			case PlayerState.UsingComputerKMState:
				Cursor.lockState = CursorLockMode.None; //커서 고정해제
				ui_PlayerMode.GetComponent<Text>().text = "UsingComputer_KMState";
				ControlStateUCKM();
				MoveToPos();
				break;
			default:
				Debug.Log("csPlayer playerState error");
				break;
		}
	}

	//Walk상태에서의 상태변화
	void ControlStateWalk() {
		Physics.Raycast(transform.position, transform.FindChild("Verticality").transform.forward, out hitInfo, 1.0f);
		if (hitInfo.transform != null)
		{
			//컴퓨터조작
			if (hitInfo.transform.name == "UsingComputerSpace")
			{
				ui_ButtonGuide.SetActive(true);
				if (Input.GetKeyDown(KeyCode.E))
				{
					playerState = PlayerState.UsingComputerRotState;
					ui_ButtonGuide.SetActive(false);
				}
			}
			//문열기
			else if (hitInfo.transform.name == "Door") {
				ui_ButtonGuide.SetActive(true);
				if (Input.GetKeyDown(KeyCode.E)) {
					GameObject.Find("hacker room").transform.FindChild("DoorObject").FindChild("Door").GetComponent<csDoor>().ChangeDoorState();
					ui_ButtonGuide.SetActive(false);
				}
			}
		}
		else {
			ui_ButtonGuide.SetActive(false);
		}
	}

	//UCR상태에서의 상태변화.
	void ControlStateUCR() {
		if (Input.GetKeyDown(KeyCode.E))
		{
			playerState = PlayerState.WalkState;
			transform.localRotation = WalkRot;
			hitName = null;
		}
		else if (Input.GetKeyDown(KeyCode.Tab) && hitName != null)
		{
			playerState = PlayerState.UsingComputerKMState;
		}

		Ray ray = new Ray(transform.FindChild("Verticality").transform.position, transform.FindChild("Verticality").transform.forward);
		RaycastHit hit;
		int layerMask = (1 << LayerMask.NameToLayer("ChoiseMonitor"));
		Physics.Raycast(ray, out hit, 10.0f, layerMask);

		if (hit.transform != null)
		{
			switch (hit.transform.name)
			{
				case "ColMapMonitor":
					EdgeMapMonitor.SetActive(true);
					EdgeFolderMonitor.SetActive(false);
					EdgeCommuMonitor.SetActive(false);
					break;
				case "ColFolderMonitor":
					EdgeMapMonitor.SetActive(false);
					EdgeFolderMonitor.SetActive(true);
					EdgeCommuMonitor.SetActive(false);
					break;
				case "ColCommuMonitor":
					EdgeMapMonitor.SetActive(false);
					EdgeFolderMonitor.SetActive(false);
					EdgeCommuMonitor.SetActive(true);
					break;
			};

			if (Input.GetMouseButtonUp(0))
			{
				hitName = hit.transform.name;
				rotLock = true;
				playerState = PlayerState.UsingComputerKMState;
			}
		}
		else {
			EdgeMapMonitor.SetActive(false);
			EdgeFolderMonitor.SetActive(false);
			EdgeCommuMonitor.SetActive(false);
		}

		MoveToPos();
	}

	//컴퓨터 조작중 카메라 위치 변화
	void MoveToPos() {
		Vector3 preCamPos = transform.FindChild("Verticality").position;

		switch (hitName)
		{
			case "ColMapMonitor":
				if (rotLock) transform.rotation = Quaternion.Slerp(transform.rotation,
					GameObject.Find("TaskSpace").transform.FindChild("CenterMonitor").FindChild("LookPosition").rotation,
					Time.deltaTime * 3.0f);
				transform.FindChild("Verticality").position = preCamPos;

				transform.FindChild("Verticality").position =
					Vector3.MoveTowards(
						transform.FindChild("Verticality").position,
						GameObject.Find("TaskSpace").transform.FindChild("CenterMonitor").FindChild("LookPosition").position,
						moveSpeed * Time.deltaTime);
				transform.FindChild("Verticality").localEulerAngles = Vector3.zero;
				colMapMonitor.SetActive(false);
				colFolderMonitor.SetActive(true);
				colCommuMonitor.SetActive(true);
				break;
			case "ColFolderMonitor":
				if (rotLock) transform.rotation = Quaternion.Slerp(transform.rotation,
					GameObject.Find("TaskSpace").transform.FindChild("FolderMonitor").FindChild("LookPosition").rotation,
					Time.deltaTime * 3.0f);
				transform.FindChild("Verticality").position = preCamPos;

				transform.FindChild("Verticality").position =
					Vector3.MoveTowards(
						transform.FindChild("Verticality").position,
						GameObject.Find("TaskSpace").transform.FindChild("FolderMonitor").FindChild("LookPosition").position,
						moveSpeed * Time.deltaTime);
				transform.FindChild("Verticality").localEulerAngles = Vector3.zero;
				colMapMonitor.SetActive(true);
				colFolderMonitor.SetActive(false);
				colCommuMonitor.SetActive(true);
				
				break;
			case "ColCommuMonitor":
				if (rotLock) transform.rotation = Quaternion.Slerp(transform.rotation,
					GameObject.Find("TaskSpace").transform.FindChild("CommuMonitor").FindChild("LookPosition").rotation,
					Time.deltaTime * 3.0f);
				transform.FindChild("Verticality").position = preCamPos;

				transform.FindChild("Verticality").position =
					Vector3.MoveTowards(
						transform.FindChild("Verticality").position,
						GameObject.Find("TaskSpace").transform.FindChild("CommuMonitor").FindChild("LookPosition").position,
						moveSpeed * Time.deltaTime);
				transform.FindChild("Verticality").localEulerAngles = Vector3.zero;
				colMapMonitor.SetActive(true);
				colFolderMonitor.SetActive(true);
				colCommuMonitor.SetActive(false);
				
				break;
			case null:
				transform.FindChild("Verticality").position = Vector3.MoveTowards(transform.FindChild("Verticality").position, usingComputerPoint.transform.position, moveSpeed * Time.deltaTime);
				transform.FindChild("Verticality").localEulerAngles = Vector3.zero;
				colMapMonitor.SetActive(true);
				colFolderMonitor.SetActive(true);
				colCommuMonitor.SetActive(true);
				rotLock = false;
				break;
		}
	}

	void ControlStateUCKM() {
		EdgeMapMonitor.SetActive(false);
		EdgeFolderMonitor.SetActive(false);
		EdgeCommuMonitor.SetActive(false);

		if (Input.GetKeyDown(KeyCode.Tab))
		{
			rotLock = false;
			playerState = PlayerState.UsingComputerRotState;
		}
	}

	void RotCameraVerticality() {
		transform.FindChild("Verticality").Rotate(-Input.GetAxis("Mouse Y") * Time.deltaTime * 50.0f, 0, 0);
	}

	void RotCamera() {
		//-------------------------------
		//카메라 회전
		//-------------------------------
		//이전 z축 회전을 저장
		float preRotZ = transform.localRotation.z;
		Vector3 preVerPos = transform.FindChild("Verticality").position;
		//마우스 입력을 기준으로 회전
		transform.Rotate(0, Input.GetAxis("Mouse X") * Time.deltaTime * 50.0f, 0);
		//카메라 고정
		transform.FindChild("Verticality").position = preVerPos;
		//이전 z축 회전을 불러와 z축 고정
		transform.localRotation = Quaternion.Euler(transform.eulerAngles.x, transform.eulerAngles.y, preRotZ);
	}

	void WalkCamera() {
		//-------------------------------
		//카메라 이동
		//-------------------------------
		transform.Translate(Vector3.right * Time.deltaTime * Input.GetAxis("Horizontal") * 2);
		transform.Translate(Vector3.forward * Time.deltaTime * Input.GetAxis("Vertical") * 2);
	}
}
