using UnityEngine;
using System.Collections;

public class csAgentNav : MonoBehaviour {

	public Camera mainCamera; //주 카메라
	public Camera secondCamera; //화면을 찍고있는, render texture가 들어간 카메라.

	public GameObject theTVScreen; //render texture가 적용된 object

	private NavMeshAgent agent;

    bool isClicked = false;
    bool isMoved = false;
    Vector2 preMousePos;

	// Use this for initialization
	void Awake()
	{
		agent = GetComponent<NavMeshAgent>();
	}

	// Update is called once per frame
	// Input과 관련되어 있기 때문에 Update함수를 사용해 주어야 함.
	void Update()
	{
        Ray ray = ray = mainCamera.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit) && hit.transform == theTVScreen.transform) // theTVScreen에 ray가 충돌 시 작동
        {
			//요원 이동
			if (Input.GetMouseButtonDown(0) && Cursor.lockState != CursorLockMode.Locked) {
				preMousePos = Input.mousePosition;
				isClicked = true;
            }
			//맵 이동. WASD로 바뀌어 미사용.
			/*
            if(Input.GetMouseButton(0) && isClicked) {
                if (Vector2.Distance(preMousePos, Input.mousePosition) > 10.0f){
                    secondCamera.transform.position = Vector3.MoveTowards(secondCamera.transform.position,
                                                                            (secondCamera.transform.position + ( new Vector3( preMousePos.x, 0, preMousePos.y) - new Vector3(Input.mousePosition.x, 0, Input.mousePosition.y) ) * secondCamera.orthographicSize/50.0f), 
                                                                            Time.deltaTime * Mathf.Pow( secondCamera.orthographicSize, 2.0f ));
                    preMousePos = Input.mousePosition;
                    isMoved = true;
                }
            }*/

            if (isClicked) {
				if (Input.GetMouseButtonUp(0)) {
					if (!isMoved) {
                        MoveAgnet();
                    }
                    preMousePos = Input.mousePosition;
                    isMoved = false;
                    isClicked = false;
                }
            }
        }

	}

    void MoveAgnet() {
        Ray ray = ray = mainCamera.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit) && hit.transform == theTVScreen.transform) // theTVScreen에 ray가 충돌 시 작동
        {
            ray = secondCamera.ViewportPointToRay(hit.textureCoord);

            if (Physics.Raycast(ray, out hit))
            {
                //render texture가 적용된 카메라로 부터 raycasthit
                agent.SetDestination(new Vector3(hit.point.x, transform.position.y, hit.point.z));
            }
        }
    }
}
