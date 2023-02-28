using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer), typeof(Animator))]
public class Enemy : MonoBehaviour
{
    protected SpriteRenderer sr;
    protected Animator anim;

    protected int _health;
    public int maxHeath;

    public int heath
    {
        get => _health;
        set
        {
            _health = value;

            if (_health > maxHeath)
                _health = maxHeath;

            if (_health <= 0)
                Death();
        }
    }

    public virtual void Death()
    {
        anim.SetTrigger("Death");
    }


    // Start is called before the first frame update
    public virtual void Start()
    {
        anim = GetComponent<Animator>();
        sr = GetComponent<SpriteRenderer>();

        if (maxHeath <= 0)
            maxHeath = 5;

        heath = maxHeath;
    }

    public virtual void TakeDamage(int damage)
    {
        heath -= damage;
    }

}