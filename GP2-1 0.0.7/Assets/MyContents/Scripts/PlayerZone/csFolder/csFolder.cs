using UnityEngine;
using System.Collections;

public class csFolder : MonoBehaviour {
	public AudioClip sound;

	public bool isHighFolder;
	public GameObject[] socket;
    public GameObject parentsFolder;

	// Use this for initialization
	void Start () {
        socket = new GameObject[6];
		parentsFolder = GameObject.Find("Monitor2Page").GetComponent<csMonitorLeft>().thisFolder;
		//최상위 폴더가 아닐 경우 0번배열에 /... 추가
		if (!isHighFolder)
        {
			socket[0] = (GameObject)Instantiate(Resources.Load("GotoBack", typeof(GameObject)),
				GameObject.Find("Monitor2Page").GetComponent<csMonitorLeft>().socketPos[0].transform.position,
				Quaternion.Euler(GameObject.Find("Monitor2Page").GetComponent<csMonitorLeft>().socketPos[0].transform.eulerAngles.x,
									GameObject.Find("Monitor2Page").GetComponent<csMonitorLeft>().socketPos[0].transform.eulerAngles.y + 180.0f,
									GameObject.Find("Monitor2Page").GetComponent<csMonitorLeft>().socketPos[0].transform.eulerAngles.z)
				);
			socket[0].transform.parent = GameObject.Find("socket1").transform;
			socket[0].GetComponent<csGoToBack>().highFolder = parentsFolder;
			socket[0].GetComponent<csGoToBack>().parentsFolder = parentsFolder;


			for (int i = 1; i < 6; i++)
            {
                socket[i] = null;
            }
        }
		//최상위 폴더일 경우
        else {
            for (int i = 0; i < 6; i++)
            {
				socket[i] = null;
            }
        }
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnMouseDown(){
		AudioSource.PlayClipAtPoint(sound, transform.position);
		//이 폴더가 갖고있는 데이터를 뿌려주고, 이전 폴더를 감춤.
		GameObject.Find("Monitor2Page").SendMessage("SetFolder", this.transform.gameObject, SendMessageOptions.DontRequireReceiver);
		//GameObject.Find("Monitor2Page").GetComponent<csMonitorLeft>().Rot = true;
		//GameObject.Find("Monitor2Page").GetComponent<csMonitorLeft>().rotData = 60.0f;
	}
}
