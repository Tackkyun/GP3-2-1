using UnityEngine;
using System.Collections;

public class csGoToBack : MonoBehaviour {

	public GameObject parentsFolder;
    public GameObject highFolder;

	// Use this for initialization
	void Start () {
		this.transform.gameObject.SetActive(false);
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnMouseDown()
    {
		//이 폴더가 갖고있는 데이터를 뿌려주고, 이전 폴더를 감춤.
		GameObject.Find("Monitor2Page").SendMessage("SetFolder", parentsFolder, SendMessageOptions.DontRequireReceiver);
		//GameObject.Find("Monitor2Page").GetComponent<csMonitorLeft>().Rot = true;
		//GameObject.Find("Monitor2Page").GetComponent<csMonitorLeft>().rotData = -60.0f;
	}
}
