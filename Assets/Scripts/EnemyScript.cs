using UnityEngine;
using System.Collections;
using UnityEngine.UI; 


namespace My
{
    public class EnemyScript : MonoBehaviour
    {

        Rigidbody2D rigidbody2D;
        public float speed = 3f;

        public GameObject explosion;
        public GameObject item;

        public int attackPoint = 10;
        public int enemyHP = 10;

        private LifeScript lifeScript;
        private FXManager fxManager;
        private GameManager gameManager;

        private const string MAIN_CAMERA_TAG_NAME = "MainCamera";
        private bool _isRendered = false;
        private Renderer renderer;
        private Transform target;

        // Use this for initialization
        void Start()
        {
            rigidbody2D = GetComponent<Rigidbody2D>();
            lifeScript = GameObject.FindGameObjectWithTag("HP").GetComponent<LifeScript>();
            fxManager = GameObject.Find("SoundEffect").GetComponent<FXManager>();
            gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
            target = GameObject.FindGameObjectWithTag("Player").transform;
            renderer = GetComponent<Renderer>();
        }

        // Update is called once per frame
        void Update()
        {
            if (enemyHP <= 0)
            {
                EnemyDead();
                gameManager.ScoreUp(1);
            }


            if (target != null)
            {
                //Debug.LogFormat("{0} , {1} , Enemy target  : {2}", this.GetType(), this.name, target.position);

                //transform.LookAt(target);

                
                float facingDistance = transform.position.x - target.position.x;
                float faceDir = facingDistance > 0 ? 0 : 180; 

                transform.rotation = new Quaternion(0, faceDir, transform.rotation.z, transform.rotation.w);
                

                //transform.rotation = new Quaternion(transform.rotation.x, transform.rotation.y, transform.rotation.z, transform.rotation.w);

                /*
                Quaternion rotation = Quaternion.LookRotation(target.position - transform.position);
                transform.rotation = new Quaternion(0, rotation.y, transform.rotation.z, transform.rotation.w);
                */

                // Chasing player
                transform.position = Vector2.MoveTowards(transform.position, new Vector2(target.position.x, transform.position.y), speed * Time.deltaTime * -1);

                //rigidbody2D.velocity = new Vector2(speed, rigidbody2D.velocity.y);

            }

            if (gameObject.transform.position.y < Camera.main.transform.position.y - 8){
                Destroy(gameObject);
            }

        }


   
        // if enemy hitted by bullet
        void OnTriggerEnter2D(Collider2D col)
        {

            if (_isRendered) {
                if (col.tag == "Bullet")
                {
                    //Debug.LogFormat("{0} , {1} , Enemy -HP  : {2}", this.GetType(), this.name, enemyHP);
                    enemyHP -= 1; 
                    StartCoroutine("EnemyDamage");
                }

                if (col.tag == "ChargeBullet")
                {
                    //Debug.LogFormat("{0} , {1} , Enemy -HP  : {2}", this.GetType(), this.name, enemyHP);
                    enemyHP -= 10;
                    StartCoroutine("EnemyDamage");

                }
            }
        }

        IEnumerator EnemyDamage()
        {
            // change player layer to playerDamage
            gameObject.layer = LayerMask.NameToLayer("EnemyDamage");

            // player Damaged effect
            int count = 2;
            while (count > 0)
            {
                // sprite transparant
                renderer.material.color = new Color(1, 1, 1, 0);
                yield return new WaitForSeconds(0.1f);

                renderer.material.color = new Color(1, 1, 1, 1);


                yield return new WaitForSeconds(0.1f);

                count--;

            }

            gameObject.layer = LayerMask.NameToLayer("Enemy");

        }



        void OnCollisionEnter2D (Collision2D col)
        {
            if (col.gameObject.tag == "Player")
            {
                Debug.LogFormat("{0} , {1} , hit Player",  this.GetType(), this.name);
                lifeScript.LifeDown(attackPoint);
            }
        }

        void OnWillRenderObject() // when object is visible
        {
            
            if(Camera.current.tag == MAIN_CAMERA_TAG_NAME)
            {
                _isRendered = true;

            }

        }

        void EnemyDead()
        {
            if (_isRendered)
            {
                    Debug.LogFormat("{0} , {1} , Bullet hit enemy", this.GetType(), this.name);
                    Destroy(gameObject);
                    Instantiate(explosion, transform.position, transform.rotation);
                    fxManager.PlayExplodeSound();
                    if (Random.Range(0, 10) == 0) // chance to drop item
                    {
                        Instantiate(item, transform.position, transform.rotation);
                    }
                }
            }            

    }
}

