using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Box : MonoBehaviour
{
    // Representa a quantidade de bombas
    public int bombs;    
   
    // Representa a quantidade de vida
    public int health;


    private void OnTriggerEnter2D(Collider2D collision)
    {
        Player player = collision.GetComponent<Player>();

        if (player != null)
        {
            player.SetHealthAndBombs(health, bombs);
            Destroy(gameObject);
        }


    }


}
