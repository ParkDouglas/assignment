using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D), typeof(Animator), typeof(SpriteRenderer))]

public class PlayerController : MonoBehaviour
{
    //components 
    public Rigidbody2D rb;
    Animator anim;
    SpriteRenderer sr;

    //movement var
    public float speed;
    public float jumpForce;

    //groundcheck stuff 
    public bool stealth;
    public bool isGrounded;
   
    public Transform groundCheck;
    public LayerMask isGroundlayer;
    public float groundCheckRadius;

    Coroutine jumpForceChange;

    Coroutine speedChange;

    public int maxLives = 5;
    public int _Lives = 3;

    public int Lives
    {
        get { return _Lives; }
        set
        {
            _Lives = value;

            if (_Lives > maxLives)
                _Lives = maxLives;
           // if (_Lives < 0)
              //  gameover
                
              Debug.Log("lives have been set to: " + _Lives.ToString());
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        sr = GetComponent<SpriteRenderer>();

        if (speed <= 0)
        {
            speed = 6.0f;

            Debug.Log("speed was set incorrect, defaulting to" + speed.ToString());

        }

        if (jumpForce <= 0)
        {
            jumpForce = 375;

            Debug.Log("Jump force was set incorrect, defaulting to" + jumpForce.ToString());
        }

        if (groundCheckRadius <= 0)
        {
            groundCheckRadius = 0.2f;

            Debug.Log("Ground check radius was set incorrect, defaulting to" + groundCheckRadius.ToString());
        }

        if (!groundCheck)
        {
            groundCheck = GameObject.FindGameObjectWithTag("GroundCheck").transform;

            Debug.Log("Ground check not set, finding it manually!");
        }
    }

    // Update is called once per frame
    void Update()
    {
        AnimatorClipInfo[] curPlayingClip = anim.GetCurrentAnimatorClipInfo(0);
        float hinput = Input.GetAxis("Horizontal");

        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, isGroundlayer);
        bool stealth = false;
       
        if( curPlayingClip.Length> 0 ) 
        {
            if (Input.GetButtonDown("Fire1") && curPlayingClip[0].clip.name != "BarrilA")
            {
                anim.SetTrigger("BarrilA");
            }
            else if (curPlayingClip[0].clip.name == "BarrilA")
            {
                rb.velocity = Vector2.zero;
            }
            else
            {
                Vector2 moveDirection = new Vector2(hinput * speed, rb.velocity.y);
                rb.velocity = moveDirection;
            }
        }

        if (isGrounded && Input.GetButtonDown("Jump"))
        {
            rb.velocity = Vector2.zero;
            rb.AddForce(Vector2.up * jumpForce);
          
        }

        if(!isGrounded && Input.GetButtonDown("Jump"))
        {
            anim.SetTrigger("JumpAttack");
        }


        if (Input.GetKey(KeyCode.LeftControl))
        {
            anim.SetTrigger("New Trigger");
        }

        if (Input.GetKey(KeyCode.LeftShift))
            stealth = true;

        if (Input.GetKeyUp(KeyCode.LeftShift))
            stealth = false;

        anim.SetFloat("hinput", Mathf.Abs(hinput));
        anim.SetBool("isGrounded", isGrounded);
        anim.SetBool("stealth", stealth);
        
        if (hinput != 0)
            sr.flipX = (hinput < 0);

    }
    public void StartJumpForceChange()
    {
        if (jumpForceChange == null)
        {
            jumpForceChange = StartCoroutine(JumpForceChange());
        }
        else
        {
            StopCoroutine(jumpForceChange);
            jumpForceChange = null;
            jumpForce /= 2;
            jumpForceChange = StartCoroutine(JumpForceChange());
        }
    }
    IEnumerator JumpForceChange()
    {
        jumpForce *= 2;

        yield return new WaitForSeconds(5.0f);

        jumpForce /= 2;

        jumpForceChange= null;
    }

    public void StartSpeedChange()
    {
        if (speedChange == null)
        {
            speedChange = StartCoroutine(SpeedChange());
        }
        else
        {
            StopCoroutine(speedChange);
            speedChange = null;
            speed /= 2;
            speedChange = StartCoroutine(SpeedChange());
        }
    }
    IEnumerator SpeedChange()
    {
        speed *= 2;

        yield return new WaitForSeconds(5.0f);

        speed /= 2;

        speedChange = null;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Squish"))
        {
            EnemyWalker enemy = collision.gameObject.transform.parent.GetComponent<EnemyWalker>();
            enemy.Squish();
            rb.AddForce(Vector2.up * 500);
        }
    }
    public virtual void TakeDamage(int damage)
    {
        maxLives -= damage;
    }
}
