using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

namespace My
{
    public class MenuScript : MonoBehaviour
    {

        void Awake()
        {
        }

        public void NewGame()
        {
            
            SceneManager.LoadScene("StartGame");
        }

        public void LoadGame()
        {
            SceneManager.LoadScene("LoadGame");
        }

        public void Setting()
        {
            SceneManager.LoadScene("Setting");
        }

    }
}
