using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class activator_script : MonoBehaviour {

    public KeyCode key;
    bool active = false;
    GameObject note;
    SpriteRenderer sr;
    Color old;

    void Awake()
    {
        sr = GetComponent<SpriteRenderer>();
    }

    void Start () {
        old = sr.color;
	}
	

	void Update () {

        if (Input.GetKeyDown(key))
        {
            StartCoroutine(Pressed());
        }

		if(Input.GetKeyDown(key) && active)
        {
            Destroy(note);
            StartCoroutine(Pressed());
        }
	}

    void OnTriggerEnter2D(Collider2D col)
    {
        active = true;
        if(col.gameObject.tag == "Note")
        {
            note = col.gameObject;
        }
    }

    void OnTriggerExit2D(Collider2D col)
    {
        active = false;
    }

    IEnumerator Pressed()
    {
        sr.color = new Color(0, 0, 0);
        yield return new WaitForSeconds(0.05f);
        sr.color = old;
    }
}
