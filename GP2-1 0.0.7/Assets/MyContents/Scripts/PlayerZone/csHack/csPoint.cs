using UnityEngine;
using System.Collections;

public class csPoint : MonoBehaviour {
	public GameObject sprite1;
	public GameObject sprite2;
	public GameObject sprite3;

	public float moveSpeed = 1.0f;
	float dir;

	public bool isReady = true;

	Transform middlePos;

	// Use this for initialization
	void Start () {
		middlePos = transform.parent.FindChild("HackPageSprite");

		sprite1.SetActive(true);
		sprite2.SetActive(false);
		sprite3.SetActive(false);
	}
	
	// Update is called once per frame
	void Update () {
		if (isReady)
		{
			dir = Input.GetAxis("Horizontal");
			transform.RotateAround(middlePos.position, transform.forward, Time.deltaTime * moveSpeed * dir);
		}

		if (middlePos.GetComponent<csHackPage>().isSuccess)
		{
			sprite1.SetActive(false);
			sprite2.SetActive(true);
			sprite3.SetActive(false);
		}
		else if (middlePos.GetComponent<csHackPage>().isDcrease)
		{
			sprite1.SetActive(false);
			sprite2.SetActive(false);
			sprite3.SetActive(true);
		}
		else {
			sprite1.SetActive(true);
			sprite2.SetActive(false);
			sprite3.SetActive(false);
		}
	}
}
