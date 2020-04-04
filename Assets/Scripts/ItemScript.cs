using UnityEngine;
using System.Collections;


namespace My
{
    public class ItemScript : MonoBehaviour
    {

        public int healPoint = 20;
        private LifeScript lifeScript;

        void OnCollisionEnter2D (Collision2D col)
        {
            if (col.gameObject.tag == "Player")
            {
                lifeScript.LifeUp(healPoint);
                Destroy(gameObject);
            }



        }


        // Use this for initialization
        void Start()
        {
            lifeScript = GameObject.FindGameObjectWithTag("HP").GetComponent<LifeScript>();
        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}