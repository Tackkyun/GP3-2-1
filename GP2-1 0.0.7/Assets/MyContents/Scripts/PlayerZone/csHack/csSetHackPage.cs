using UnityEngine;
using System.Collections;

public class csSetHackPage : MonoBehaviour {

	public bool isActive = false; //시작시 true
	public bool isAtted = false; //끝낼시 true

	public bool isMinimize = false;
	public bool isincreased = false;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (isActive)
		{
			ActivePage();
		}


		if (isMinimize)
		{
			EndPage();
		}
	}

	void EndPage()
	{
		transform.localScale = Vector3.Lerp(transform.localScale, Vector3.zero, Time.deltaTime * 3.0f);
		if (transform.localScale.x <= 0.03f)
		{
			transform.localScale = Vector3.zero;
			Init();
			isincreased = false;
		}
	}

	void Init()
	{
		isMinimize = false;
		isAtted = false;
	}

	public void ActivePage()
	{
		transform.localScale = Vector3.Lerp(transform.localScale, Vector3.one, Time.deltaTime * 3.0f);
		if (transform.localScale.x >= 0.97f)
		{
			transform.localScale = Vector3.one;
			if (isincreased == false)
			{
				isActive = false;
				isincreased = true;
			}
		}
	}
}
