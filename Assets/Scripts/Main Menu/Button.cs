using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Button : MonoBehaviour {

    public AudioClip buttonClip;
    public AudioSource buttonSource;

    private void Start()
    {
        buttonSource.clip = buttonClip;
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            buttonSource.Play();
        }
    }

    public void NewGameBtn(string newGameLevel)
    {
        SceneManager.LoadScene(newGameLevel);
    }

    public void ExitGameBtn()
    {
        Application.Quit();
    }



}
