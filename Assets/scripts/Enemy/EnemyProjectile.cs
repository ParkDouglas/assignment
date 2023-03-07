using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Dependencies.Sqlite;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class EnemyProjectile : MonoBehaviour
{
    public float Lifetime;

    [HideInInspector]
    public float speed;

    // Start is called before the first frame update
    void Start()
    {
        if (Lifetime <= 0)
            Lifetime = 2.0f;

        GetComponent<Rigidbody2D>().velocity = new Vector2(speed, 0);
        Destroy(gameObject, Lifetime);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
       // if (collision.gameObject.CompareTag("Ground") || collision.gameObject.CompareTag("Wall"))
         //   Destroy(gameObject);

        if (collision.gameObject.CompareTag("Player"))
        {
            GameManager.instance.Lives--;
           Destroy(this.gameObject);
        }
        
            


    }

}


