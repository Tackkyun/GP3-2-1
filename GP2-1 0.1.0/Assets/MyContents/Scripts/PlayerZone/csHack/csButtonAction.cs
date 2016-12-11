using UnityEngine;
using System.Collections;

public class csButtonAction : MonoBehaviour {

	Vector3 preSize;

	public GameObject mouseOver;

	// Use this for initialization
	void Start () {
		preSize = transform.localScale;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void OnMouseDown() {
		transform.localScale = transform.localScale * 0.9f;
		mouseOver.transform.localScale = transform.localScale;
		mouseOver.GetComponent<SpriteRenderer>().sortingOrder = 5;
	}

	public void OnMouseUp() {
		transform.localScale = preSize;
		mouseOver.transform.localScale = preSize;
	}

	public void OnMouseExit() {
		OnMouseUp();
		mouseOver.GetComponent<SpriteRenderer>().sortingOrder = 5;
	}

	public void OnMouseOver() {
		mouseOver.GetComponent<SpriteRenderer>().sortingOrder = 13;
	}
}
