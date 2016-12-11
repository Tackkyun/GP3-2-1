using UnityEngine;
using System.Collections;

public class csAudio : MonoBehaviour {

	public AudioSource audio2;
	public AudioClip sound2;

	// Use this for initialization
	void Start () {
		audio2 = gameObject.AddComponent<AudioSource>();
		audio2.clip = sound2;
		audio2.loop = true;
		audio2.volume = 0.4f;

		audio2.Play();
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
