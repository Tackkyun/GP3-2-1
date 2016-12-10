using UnityEngine;
using System.Collections;

public class csHackPage : MonoBehaviour {

	public bool isActive = false; //시작시 true
	public bool isAtted = false; //끝낼시 true

	float hackPoint;
	public bool isSuccess = false;
	bool isFail = false;
	bool isDcrease = false;
	bool isMouseUp = false;
	bool isincreased = false;
	bool isMinimize = false;

	public float successRange = 10.0f;
	public float semiSuccessRange = 20.0f;
	public float setErrorTime = 1.5f;

	float errorTime;

	TextMesh errorTimeText;

	float marginOfError;

	string hackTarget = null;

	public bool isClickRunButtonUp = false;
	public bool isClickRunButtonDown = false;

	// Use this for initialization
	void Start () {
		EndPage();
		errorTimeText = transform.parent.FindChild("ErrorTime").GetComponent<TextMesh>();
		transform.parent.FindChild("Point").position = transform.parent.parent.FindChild("StartPoint").position;
	}
	
	// Update is called once per frame
	void Update () {
		if (isActive) {
			ActivePage();
		}

		if (isActive && transform.parent.localScale == Vector3.one)
		{
			if (isClickRunButtonDown) //성공여부 조회
			{
				isMouseUp = false;
				CheckSuecces();
			}
			else if (isClickRunButtonUp) //조작종료
			{
				isMouseUp = true;
				isDcrease = false;
			}
			else {
				isClickRunButtonUp = false;
				isClickRunButtonDown = false;
			}

			if (!isMouseUp && isDcrease) //남은시감 감소
			{
				ErrorTimeDecrease();
			}


			//성공여부 및 시간 출력
			if (isSuccess)
			{
				errorTimeText.text = "Success";
				isAtted = true;
			}
			else if (isFail) {
				errorTimeText.text = "Fail";
			}
			else {
				errorTimeText.text = (Mathf.Round(errorTime * 100) / 100).ToString();
			}
		}

		if (isAtted) {
			isActive = false;
			EndPage();
		}

		if (isMinimize) {
			EndPage();
		}
	}

	void Init() {
		//성공포인트 생성
		CreateHackingPoint();
		//errorTime갱신
		errorTime = setErrorTime;
		//point위치 초기화
		transform.parent.FindChild("Point").localEulerAngles = Vector3.zero;

		isSuccess = false;
		isDcrease = false;
		isMouseUp = false;
		isFail = false;
		isMinimize = false;
		isAtted = false;
	}

	//성공포인트 생성
	void CreateHackingPoint() {
		hackPoint = Random.Range(0.0f, 360.0f);
	}

	void CheckSuecces() {
		//point의 현재 각도 구하기. up이 0도
		Vector3 v = transform.position - transform.parent.FindChild("Point").position;
		float pointRot = Mathf.Atan2(-v.x, v.y) * Mathf.Rad2Deg + 180.0f;

		float checkRot = pointRot + (360.0f - hackPoint);

		if (checkRot > 180) {
			checkRot = Mathf.Abs(checkRot - 360);
		}

		marginOfError = checkRot;


		//성공여부
		if (pointRot >= hackPoint - successRange / 2.0f && pointRot <= hackPoint + successRange / 2.0f)
		{
			isSuccess = true;
		}
		else if (pointRot >= hackPoint - semiSuccessRange / 2.0f && pointRot <= hackPoint + semiSuccessRange / 2.0f)
		{
			isSuccess = false;
			StartCoroutine(ErrorTime(2.0f));
		}
		else {
			isSuccess = false;
			StartCoroutine(ErrorTime(0.0f));
		}
	}

	//대기시간
	IEnumerator ErrorTime(float delay) {
		yield return new WaitForSeconds(delay);
		isDcrease = true;
	}

	//시간 감소
	void ErrorTimeDecrease() {
		errorTime -= Time.deltaTime;
		if (errorTime <= 0) Fail();
	}

	void Fail() {
		StartCoroutine(EndDelay(2.0f));
	}

	IEnumerator EndDelay(float delay)
	{
		isFail = true;
		isActive = false;
		yield return new WaitForSeconds(delay);
		isMinimize = true;
	}

	void EndPage() {
		GameObject.Find("FolderPage").GetComponent<csFolderMonitor>().isActive = true;
		transform.parent.localScale = Vector3.Lerp(transform.parent.localScale, Vector3.zero, Time.deltaTime * 3.0f);
		if (transform.parent.localScale.x <= 0.03f)
		{
			transform.parent.localScale = Vector3.zero;
			Init();
			isMinimize = false;
			isincreased = false;
		}
	}

	public void ActivePage() {
		Vector3 v = transform.position - transform.parent.FindChild("Point").position;
		transform.parent.localScale = Vector3.Lerp(transform.parent.localScale, Vector3.one, Time.deltaTime * 3.0f);
		if (transform.parent.localScale.x >= 0.97f) {
			transform.parent.localScale = Vector3.one;
			if (isincreased == false) {
				transform.parent.FindChild("Point").localPosition = new Vector3(0.0f, 0.116f, 0.0f);
				isincreased = true;
			}
		}
	}

	public void SetHackTarget(string _name) {
		hackTarget = _name;
	}

	public string GetHackTarget() {
		return hackTarget;
	}
}
