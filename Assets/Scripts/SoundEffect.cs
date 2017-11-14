using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundEffect : MonoBehaviour {

    public AudioClip punchClip;
    public AudioClip kickClip;


    public AudioSource punchSource;
    public AudioSource kickSource;

	// Use this for initialization
	void Start () {
        punchSource.clip = punchClip;
        kickSource.clip = kickClip;

	}
	
	// Update is called once per frame
	void Update () {

        if (Input.GetKeyDown(KeyCode.Z))
        {
            punchSource.Play();
        }		
		if (Input.GetKeyDown(KeyCode.X))
        {
            kickSource.Play();
        }
	}
}
