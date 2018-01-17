using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathCheck : MonoBehaviour {

    public Player player;

    void OnTriggerEnter2D(Collider2D col)
    {
        if(col.CompareTag("Player"))
            player.Die();
    }	

}
