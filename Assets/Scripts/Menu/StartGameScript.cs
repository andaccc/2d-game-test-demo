using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

namespace My
{
    public class StartGameScript : MonoBehaviour
    {
        GameObject menuMusic;

        void Awake()
        {
            menuMusic = GameObject.Find("MainThemeMusic");
        }

        // Update is called once per frame
        void Update()
        {

        }

        public void StartGameNormal()
        {
            GameSetting.difficulty = "Normal";
            SceneManager.LoadScene("Stage1");
            Destroy(menuMusic);
        }

        public void StartGameHard()
        {
            GameSetting.difficulty = "Hard";
            SceneManager.LoadScene("Stage1");
            Destroy(menuMusic);
        }

        public void Back()
        {
            SceneManager.LoadScene("MainMenu");
        }


        void DestroyMusic()
        {
            Destroy(menuMusic);
        }


    }

}