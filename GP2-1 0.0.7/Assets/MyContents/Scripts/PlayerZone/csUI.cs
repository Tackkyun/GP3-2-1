using UnityEngine;
using System.Collections;

public class csUI : MonoBehaviour {

	enum UIState
	{
		Idle = 1 << 0,
		ESC = 1 << 1
	}

	UIState uiState;

	public GameObject escUI;
	public GameObject clearTextUi;

	// Use this for initialization
	void Start () {
		escUI.SetActive(false);

		uiState = UIState.Idle;
	}
	
	// Update is called once per frame
	void Update () {
		switch (uiState) {
			case UIState.Idle:
				if (Input.GetKeyDown(KeyCode.Z)) uiState = UIState.ESC;
				escUI.SetActive(false);
				Time.timeScale = 1;
				break;
			case UIState.ESC:
				if (Input.GetKeyDown(KeyCode.Z)) uiState = UIState.Idle;
				Cursor.lockState = CursorLockMode.None;
				escUI.SetActive(true);
				Time.timeScale = 0;
				break;
		}
	}

	public void ChangeStateIdle() {
		uiState = UIState.Idle;
	}

	public void ChangeStateESC() {
		uiState = UIState.ESC;
	}

	public void EXIT() {
		Application.Quit();
	}

	public string GetUIState() {
		return uiState.ToString();
	}

	public void OutputTextMesh(TextMesh _tm, string _st) {
		_tm.GetComponent<TextMesh>().text = _st;
	}

	public void OutputClearText() {
		clearTextUi.GetComponent<TextMesh>().text = "Clear";
	}

	public void OutputFailText()
	{
		clearTextUi.GetComponent<TextMesh>().text = "Fail";
	}
}
