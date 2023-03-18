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
    public AudioClip pickupSound;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
            {

              

                switch (currentPickup)
                {
                    case PowerUpType.Jump:
                    collision.gameObject.GetComponent<PlayerController>().StartJumpForceChange();
                    break;

                    case PowerUpType.Life:
                    GameManager.instance.Lives++;
                    break;

                    case PowerUpType.Score:
                    break;
                    case PowerUpType.Speed:
                    collision.gameObject.GetComponent<PlayerController>().StartSpeedChange();
                    break;
                }
        }

        if (pickupSound)
            collision.gameObject.GetComponent<AudioSourceManager>().PlayOneShot(pickupSound, false);

        Destroy(gameObject);
    }
}