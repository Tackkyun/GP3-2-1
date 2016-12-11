using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class csGameEnd : MonoBehaviour {

	public bool isEnd = false;
	public bool isDead = false;

	bool once = true;

	float fadeTime = 3.0f;
	float fadeAlpha = 0.0f;

	public Image fade;

	GameObject CheckHack;

	// Use this for initialization
	void Start () {
		CheckHack = GameObject.Find("HackPageSprite");
	}
	
	// Update is called once per frame
	void Update () {
		if (isEnd == true && once)
		{
			GameObject.Find("AgentObject").transform.FindChild("Agent").GetComponent<csStatus>().hp = 8.0f;
			GameObject.Find("AgentObject").transform.FindChild("Agent").GetComponent<csStatus>().attDelay = 0.5f;
			once = false;
		}

		if (CheckHack.GetComponent<csHackPage>().failCount >= 1) {
			isEnd = true;
		}

		if (isDead)
		{
			if (fadeAlpha < 1.0f)
			{
				fadeAlpha += (Time.deltaTime / fadeTime);
				fade.color = new Color(0.0f, 0.0f, 0.0f, fadeAlpha);
			}
			else if (fadeAlpha >= 1.0f)
			{
				SceneManager.LoadScene("Main");
			}
		}
	}
}
