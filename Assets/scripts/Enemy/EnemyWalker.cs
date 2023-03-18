using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]

public class EnemyWalker : Enemy
{
    Rigidbody2D rb;
    public float speed;
    // Start is called before the first frame update
    public override void Start()
    {
        base.Start();
        rb = GetComponent<Rigidbody2D>();

        if (speed <= 0)
            speed = 1.5f;

    }

    // Update is called once per frame
    void Update()
    {
        AnimatorClipInfo[] curClips = anim.GetCurrentAnimatorClipInfo(0);

        if (curClips[0].clip.name == "Walk")
        {
            if (sr.flipX)
            {
                rb.velocity = new Vector2(-speed, rb.velocity.y)
;            }
            else
            {
                rb.velocity = new Vector2(speed, rb.velocity.y);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Barrier"))
        {
            sr.flipX = !sr.flipX;
        }
    }

    public void Squish()
    {
       
        anim.SetTrigger("Squish");
    }
    public void DestroyMyself()
    {

        AudioManager.Instance.Play("deathBeaver");
        Destroy(gameObject.transform.parent.gameObject.transform.parent.gameObject);
    }
}
