using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : MonoBehaviour
{
    public enum PowerUpType
    {
        Jump,
        Life,
        Score,
        Speed,
    }
    public PowerUpType currentPickup;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
            {

               PlayerController temp = collision.gameObject.GetComponent<PlayerController>();

                switch (currentPickup)
                {
                    case PowerUpType.Jump:
                    temp.StartJumpForceChange();
                    break;

                    case PowerUpType.Life:
                    temp.Lives++;
                    
                    break;
                    case PowerUpType.Score:
                    break;
                    case PowerUpType.Speed:
                    temp.StartSpeedChange();
                    break;
                }
        }
        
        Destroy(gameObject);
    }
}