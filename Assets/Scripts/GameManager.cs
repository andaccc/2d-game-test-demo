using UnityEngine;
using System.Collections;
using UnityEngine.UI;

namespace My
{

    public class GameManager : MonoBehaviour
    {

        public GameObject enemy;
        public GameObject enemySpawn;
        
        public Text scoreText;
        private int score = 0;

        private float spawnRange;

        //public GameObject SpawnPos;



        // Use this for initialization
        void Start()
        {
            if (GameSetting.difficulty == "Normal")
            {
                spawnRange = 5f;
            }
            else if (GameSetting.difficulty == "Hard")
            {
                spawnRange = 1f;
            }

            StartCoroutine("Spawn");
            //InvokeRepeating("Spawn", 3, 0.5f);

            scoreText.text = "Kill Count: 0";

        }


        /*
        void Spawn()
        {
            StartCoroutine("waitRandomSec", (10f));
            //WaitForSeconds(Random.Range(0.5f, 5f));
            Instantiate(enemy, transform.position, transform.rotation);


        }*/

        IEnumerator Spawn()
        {
            while (true) { 
            yield return new WaitForSeconds(Random.Range(0.1f, spawnRange));
            Instantiate(enemy, enemySpawn.transform.position, enemySpawn.transform.rotation);
        }
        }
                       
        /*
        IEnumerator waitRandomSec(float waitRange)
        {
            Debug.LogFormat("{0} , {1} , Wait Spawn Sec : {2}", this.GetType(), this.name, waitRange);
            yield return new WaitForSeconds(10);
        }*/


        public void ScoreUp(int scoreVal)
        {
            score += scoreVal;
        }

        void Update()
        {
            scoreText.text = "Kill Count : " + score.ToString();
        }
    }
}