using UnityEngine;
using System.Collections;


namespace My
{
    public class BulletScript : MonoBehaviour
    {

        private GameObject player;

        private int speed = 5;

        void Awake()
        {           
        }

        // Use this for initialization
        void Start()
        {
            player = GameObject.Find("MainCharPlayer");
            //player = GameObject.FindWithTag("Player");
            Rigidbody2D rb2d = GetComponent<Rigidbody2D>();


            /*
            if(GameObject.Find("MainCharPlayer").GetComponent<PlayerController>().facingRight)
                xdir = 1; 
            else 
                xdir = -1;
            */


            float bulletSpeedX = speed * player.transform.localScale.x;
            Debug.Log("bullet dir = " + bulletSpeedX);
            rb2d.velocity = new Vector2(bulletSpeedX, rb2d.velocity.y);



            Vector2 playerDir = transform.localScale;
            playerDir.x = player.transform.localScale.x;
            transform.localScale = playerDir;
            Destroy(gameObject, 1); // destory bullet after x sec
        }

        void OnTriggerEnter2D (Collider2D col)
        {
            if (col.gameObject.tag == "Enemy")
            {
                // Debug.LogFormat("{0} , {1} , Bullet triggered", this.GetType() , this.name);
                Destroy(gameObject);
            }



        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}