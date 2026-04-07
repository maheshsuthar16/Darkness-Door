using System;
using UnityEngine;

public class Coin : MonoBehaviour
{   [SerializeField] AudioClip coinPickup;
    bool wasCollected = false ;

    [SerializeField]  private int  coinValue = 5 ;
    [SerializeField]  private int  scoreValue = 15;


    void OnTriggerEnter2D(Collider2D other)
    {
      if (other.CompareTag("Player"))
       {    wasCollected = true;
            AudioSource.PlayClipAtPoint(coinPickup, transform.position);
            
            gameObject.SetActive(false);
            GameManager.Instance.AddCoin(coinValue,scoreValue);
             Destroy(gameObject);
        }
    }
}
