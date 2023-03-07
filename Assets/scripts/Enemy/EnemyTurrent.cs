using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using Unity.VisualScripting;
using UnityEditor.U2D.Path;
using UnityEngine;

[RequireComponent(typeof(EnemyShoot))]
public class EnemyTurrent : Enemy
{
    //for detecting range
 //  [SerializeField] private BoxCollider2D boxCollider;
  // [SerializeField] private LayerMask playerLayer;
  // [SerializeField] private float range;
  // [SerializeField] private float colliderDistance;


    //everything else
    public float projectileFireRate;
    float timeSinceLastFire;
    EnemyShoot shootScript;
    public float minAttackDistance = 0.5f;
   
    public float force;
    public Transform Player2;
    public Transform rangeLeft;
    public Transform rangeRight;
    private void Awake()
    {
        anim = GetComponent<Animator>();
    }



    // Update is called once per frame
    void Update()
    {
        AnimatorClipInfo[] curClips = anim.GetCurrentAnimatorClipInfo(0);

        //float distance = (transform.position.x - Player2.position.x);
        // Debug.Log(distance);
       // if (PlayerInSight() && gameObject.CompareTag("Player"))
        {
            if (GameManager.instance.playerInstance.transform.position.x - rangeLeft.position.x < 0)
            {
                anim.SetBool("OutOfRange", true); 
            }
            else if (GameManager.instance.playerInstance.transform.position.x - rangeRight.position.x > 0)
            {
                anim.SetBool("OutOfRange", true);
            }
            else
            {
                anim.SetBool("OutOfRange", false);
            }

            if (curClips[0].clip.name != "Shoot")
            {
                if (Time.time >= timeSinceLastFire + projectileFireRate)
                {
                    anim.SetTrigger("Shoot");
                }
            }
            if (transform.position.x > GameManager.instance.playerInstance.transform.position.x)
            {
                sr.flipX = true;
            }
            else
            {
                sr.flipX = false;
            }

        }

    }

    public override void Start()
    {
        base.Start();

        shootScript = GetComponent<EnemyShoot>();
        shootScript.OnProjectileSpawned.AddListener(UpdateTimeSinceLastFire);

    }

    public override void Death()
    {
        Destroy(gameObject);
    }

    private void OnDisable()
    {
        shootScript.OnProjectileSpawned.RemoveListener(UpdateTimeSinceLastFire);
    }

    void UpdateTimeSinceLastFire()
    {
        timeSinceLastFire = Time.time;
    }

   // private bool PlayerInSight()
   // {
        

     //   RaycastHit2D hit = Physics2D.BoxCast(boxCollider.bounds.center + transform.right * range * transform.localScale.x * colliderDistance,
    //    new Vector3(boxCollider.bounds.size.x * range, boxCollider.bounds.size.y,boxCollider.bounds.size.z),
     //   0, Vector2.left, 0, playerLayer );

        

     //   return hit.collider != null;//
     // /  
       
  //  }

  //  private void OnDrawGizmos()
  ////  {
     //   Gizmos.color = Color.red;
    //    Gizmos.DrawWireCube(boxCollider.bounds.center + transform.right * range * transform.localScale.x * colliderDistance,
    //    new Vector3(boxCollider.bounds.size.x * range, boxCollider.bounds.size.y, boxCollider.bounds.size.z));
  //  }
    //public void playerCheck()
    //{
    //    rb= GetComponent<Rigidbody2D>();
    //   Player = GameObject.FindGameObjectWithTag("Player");

    //  Vector3 direction = Player.transform.position - transform.position;
    //   rb.velocity = new Vector2(direction.x, direction.y).normalized * force;
    //   
    // }


}
