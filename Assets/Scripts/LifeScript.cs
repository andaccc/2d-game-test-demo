using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace My
{
    public class LifeScript : MonoBehaviour {

        RectTransform rt;

        public GameObject explosion;
        public Text gameOverText;

        public FXManager fxManager;


        private GameObject player;



        private bool gameOver = false;
        private bool fall = false;
        private bool gameOverFx = false;

    // Use this for initialization
    void Start() {
        rt = GetComponent<RectTransform>();
        player = GameObject.FindGameObjectWithTag("Player");
        }

    public void LifeDown(int ap)
    {
        // change life sprite size referring to attack point
        rt.sizeDelta -= new Vector2(0, ap);
        Debug.LogFormat("{0} , {1} , LifeDown  HP = {2}", this.GetType(), this.name, rt.sizeDelta.y);
    }

    public void LifeUp(int hp)
        {
            if (rt.sizeDelta.y < 240f)
            {
                rt.sizeDelta += new Vector2(0, hp);
            } else
                //rt.sizeDelta += new Vector2(0, 0);

            Debug.LogFormat("{0} , {1} , LifeUp  HP = {2}", this.GetType(), this.name, rt.sizeDelta.y);
        }




    // Update is called once per frame
    void Update() {
            if (rt.sizeDelta.y <= 0)
            {
                if (gameOver == false)
                {

                    Instantiate(explosion, player.transform.position + new Vector3(0, 1, 0), player.transform.rotation);
                }

                if (fall != true)
                {
                    GameOver(fall);
                }
            }

        if (gameOver)
        {
            gameOverText.enabled = true;
            {
                    
                    Invoke("LoadScene", 1f);
            }
        }
}
     public void GameOver(bool fall)
        {
            if (fall == true)
            {
                rt.sizeDelta = new Vector2(0, 0);
                Debug.LogFormat("{0} , {1} , Fall Call GameOver ", this.GetType(), this.name);
                Instantiate(explosion, player.transform.position + new Vector3(0, 1, 0), player.transform.rotation);
            }
            if (!gameOverFx)
            {
                fxManager.PlayGameOverSound();
                gameOverFx = true;
            }

            gameOver = true;
            Camera.main.transform.parent = null; // prevent camera getting destroy
            Destroy(player);
        }

        void LoadScene()
        {

            if (Input.GetMouseButton(0) || Input.GetButtonDown("Jump"))
            {
                SceneManager.LoadScene("MainMenu");
            }
        }
    }
}