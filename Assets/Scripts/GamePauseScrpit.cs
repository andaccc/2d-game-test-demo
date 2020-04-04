using UnityEngine;
using System;
using System.Collections;
using UnityEngine.SceneManagement;

public class GamePauseScrpit : MonoBehaviour {

    public GameObject pauseMenu;
    public bool paused = false;

	// Use this for initialization
	void Start () {
        pauseMenu.SetActive(false);
    }
	
	// Update is called once per frame
	void Update () {
        if (Input.GetButtonDown("Pause"))
        {
            if (paused)
            {
                pauseMenu.SetActive(false);
            }
            else
            {
                pauseMenu.SetActive(true);
            }
            paused = togglePause();
        }
	}

    public void Back()
    {
        if (paused)
        {
            pauseMenu.SetActive(false);
            paused = togglePause();
        }
    }

    public void BackToMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    bool togglePause()
    {
        if (Time.timeScale == 0f)
        {
            Debug.LogFormat("{0} , {1} , pause false", this.GetType(), this.name);
            Time.timeScale = 1f;
            return (false);
        }
        else
        {
            Debug.LogFormat("{0} , {1} , pause true", this.GetType(), this.name);
            Time.timeScale = 0f;
            return (true);
        }
    }

}
