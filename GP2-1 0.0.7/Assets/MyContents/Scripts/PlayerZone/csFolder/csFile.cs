using UnityEngine;
using System.Collections;

public class csFile : MonoBehaviour {

	public GameObject filePage;

	public bool isClicked = false;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

	}

	void OnMouseEnter() {
		transform.GetComponent<SpriteRenderer>().color = new Color(0.566f, 0.677f, 1.0f, 1.0f);
	}

	void OnMouseExit() {
		if (!isClicked)
		{
			transform.GetComponent<SpriteRenderer>().color = new Color(1.0f, 1.0f, 1.0f, 1.0f);
		}
	}

    void OnMouseDown() {
		isClicked = !isClicked;
		if (isClicked)
		{
			transform.parent.parent.GetComponent<csSockets>().usedItem += 1;
		}
		else {
			transform.parent.parent.GetComponent<csSockets>().usedItem -= 1;
		}
	}

	public void DestroyThis() {
		GameObject.Destroy(this.gameObject);
	}
}
