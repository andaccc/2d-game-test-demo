using UnityEngine;
using System.Collections;

namespace My
{
    public class PlayerController : MonoBehaviour
    {
        //[HideInInspector]
        public bool facingRight = true;
        [HideInInspector]
        public bool jump = false;
        [HideInInspector]
        public bool canDoubleJump = false;
        [HideInInspector]
        public bool walk = false;

        public LayerMask groundLayer;

        //public GameObject mainCamera;
        public GameObject bullet;
        public GameObject chargeBullet;

        public LifeScript lifeScript;
        public FXManager fxManager;

        public int maxBullet = 3; 

        public float moveForce = 365f;
        public float maxSpeed = 5f;
        public float jumpForce = 10000f;

       

        private Transform groundCheck; // goundcheck object reference in Inspector

        private GamePauseScrpit gamePauseScript;

        private bool grounded = false;

        private Renderer renderer;

        private Animator anim;
        private Rigidbody2D rb2d;

        private int chargeCount = 0;
        private int chargeMeter = 80;

        private float bulletOffsetX = 1f;
        private float bulletOffsetY = 0f;
        private int bulletRotation;

        void Awake()
        {
            // set up reference
            groundCheck = transform.Find("groundCheck"); // reference to groundCheck empty object
            anim = GetComponent<Animator>();
            rb2d = GetComponent<Rigidbody2D>();
            renderer = GetComponent<Renderer>();
            gamePauseScript = GameObject.Find("PauseScreenManager").GetComponent<GamePauseScrpit>();
            
        }

        // Use this for initialization
        void Start()
        {

        }

        // Update is called once per frame

        void Update()
        {
            if (gamePauseScript.paused != true)
            {
                groundLayer = 1 << LayerMask.NameToLayer("Ground");
                grounded = Physics2D.Linecast(transform.position, groundCheck.position, groundLayer);
                anim.SetBool("Grounded", grounded);

                //Debug.Log(this.GetType() + ", Update time :" + Time.deltaTime);
                //Debug.Log(this.GetType() + ", grounded = " + grounded);
                //Debug.Log(this.GetType() + ", CharPos = " + transform.position);
                //Debug.Log(this.GetType() + ", ground check pos = " + groundCheck.position);

                float x = Input.GetAxis("Horizontal");
                float y = Input.GetAxis("Vertical");
                bool walk = Input.GetButton("Walk");
                bool jump = Input.GetButtonDown("Jump");

                Move(x, y, jump, walk);

                //Debug.LogFormat("{0} , {1} , x {2}, y {3} ", this.GetType(), this.name, Input.GetAxis("Horizontal"), Input.GetAxis("Vertical")); 

                // bullet position config
                if (facingRight)
                {
                    bulletOffsetX = 1f;
                    bulletRotation = -90;
                }

                else
                {
                    bulletOffsetX = -1f;
                    bulletRotation = 90;
                }

                if (y == -1)
                    bulletOffsetY = -0.6f;
                else
                {
                    bulletOffsetY = 0f;
                }

                if (Input.GetButton("Fire1"))
                {
                    //Debug.LogFormat("{0} , {1} , bullet charge count {2}", this.GetType(), this.name, chargeCount);
                    if (chargeCount < chargeMeter)
                    {
                        chargeCount += 1;
                    }

                    //if (chargeCount > 20)
                    //{
                    StartCoroutine("ChargeState");
                    //}

                    if (Input.GetButtonDown("Fire1") && GameObject.FindGameObjectsWithTag("Bullet").Length < maxBullet)
                    {
                        chargeCount = 0;
                        fxManager.PlayBulletSound();
                        Instantiate(bullet, transform.position + new Vector3(bulletOffsetX, bulletOffsetY, 0f), Quaternion.Euler(transform.rotation.x, transform.rotation.y, bulletRotation));
                    }
                }

                if (Input.GetButtonUp("Fire1") && chargeCount < chargeMeter)
                {
                    //Debug.Log("chargeBullet shot");
                    renderer.material.color = new Color(1, 1, 1, 1); // back to normal color

                    chargeCount = 0;
                }

                else if (Input.GetButtonUp("Fire1") && chargeCount == chargeMeter)
                {
                    //Debug.Log("chargeBullet shot");
                    fxManager.PlayBulletSound();
                    renderer.material.color = new Color(1, 1, 1, 1); // back to normal color
                    Instantiate(chargeBullet, transform.position + new Vector3(bulletOffsetX, bulletOffsetY, 0f), Quaternion.Euler(transform.rotation.x, transform.rotation.y, bulletRotation));
                    chargeCount = 0;
                }
            }
        }

        IEnumerator ChargeState()
        {
                //Debug.Log("In charge coroutine " + chargeCount);
                if (chargeCount < chargeMeter && chargeCount > 10)
                {
                    renderer.material.color = new Color(0.5f, 1, 1, 1);
                    //  yield return new WaitForSeconds(0.5f);
                    //  renderer.material.color = new Color(1 , 1, 1, 1);
                    yield return new WaitForSeconds(0.5f);
                }

                else if (chargeCount == chargeMeter)
                {
                    renderer.material.color = new Color(1, 0.5f, 1, 1);
                    //yield return new WaitForSeconds(0.5f);
                    //renderer.material.color = new Color(1, 1, 1, 1);
                    yield return new WaitForSeconds(0.5f);
                }

                else if (chargeCount <= 10)
                {
                    renderer.material.color = new Color(1, 1, 1, 1);
                }

            } 
        

        void FixedUpdate()
        {
 
        }

        void Move(float move, float y , bool jump, bool walk)
        {
            // for crouch
            anim.SetFloat("Crouch", y);
            if (y < -Mathf.Epsilon &&  grounded)
            {
                //Debug.Log(this.GetType() + ", y = " + y);
                rb2d.velocity = new Vector2(0, rb2d.velocity.y);

            }

            // for flipping
            if (move < 0 && facingRight)
            {
                Flip();
            }
            else if (move > 0 && !facingRight)
            {
                Flip();
            }
 
            if (walk)  // for walk toggle
            {
                //Debug.Log(this.GetType() + ", walk = " + walk);
                move = move * 0.5f;
                //Debug.Log(this.GetType() + ", move walk = " + move);
            }

            anim.SetFloat("moveSpeed", Mathf.Abs(move));

            if (y > -0.1)
            {
                // move x dir
                rb2d.velocity = new Vector2(move * maxSpeed, rb2d.velocity.y);

                if (jump)
                {
                    if (grounded)
                    {
                        fxManager.PlayJumpSound();
                        anim.SetTrigger("Jump");
                        rb2d.velocity = new Vector2(rb2d.velocity.x, 0);
                        rb2d.AddForce(new Vector2(0f, jumpForce));
                        canDoubleJump = true;
                    }
                    else
                    {
                        if (canDoubleJump)
                        {
                            fxManager.PlayJumpSound();
                            canDoubleJump = false;
                            anim.SetTrigger("Jump");
                            rb2d.velocity = new Vector2(rb2d.velocity.x, 0);
                            rb2d.AddForce(new Vector2(0f, jumpForce));
                            jump = false;
                        }
                    }
                }
            }
        }

        void Flip()
        {
            // Switch the way the player is labelled as facing.
            //anim.SetTrigger("Turn");
            facingRight = !facingRight;

            // Multiply the player's x local scale by -1.
            Vector3 theScale = transform.localScale;
            theScale.x *= -1;
            transform.localScale = theScale;
        }


        void OnCollisionEnter2D (Collision2D col)
        {
            if (col.gameObject.tag == "Enemy")
            {
                Debug.Log(this.GetType() + ", start coroutine damage");
                StartCoroutine("Damage");
            }
        }

        IEnumerator Damage()
        {
            // change player layer to playerDamage
            gameObject.layer = LayerMask.NameToLayer("PlayerDamage");

            // player Damaged effect
            int count = 10;
            while (count > 0)
            {
                // sprite transparant
                renderer.material.color = new Color(1, 1, 1, 0);
                yield return new WaitForSeconds(0.05f);
                renderer.material.color = new Color(1, 1, 1, 1);                                    
                yield return new WaitForSeconds(0.05f);

                count--;

            }

            gameObject.layer = LayerMask.NameToLayer("Player");

        }

        void OnTriggerEnter2D(Collider2D col)
        {
            if (col.tag == "DeadZone")
            {
                //Debug.Log(this.GetType() + ", entered dead zone");
                bool fall = true;
                lifeScript.GameOver(fall);
            }

        }

    }
}