using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseScreenManager : MonoBehaviour
{
    public GameObject pauseMenu;
    private bool isShowing = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Back()
    {
        //menu
    }

    public void BackToMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

}
