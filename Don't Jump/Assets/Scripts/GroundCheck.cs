using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundCheck : MonoBehaviour {

    public GameObject playerObj;
    private Player player;
    
	void Start ()
    {
        player = playerObj.GetComponent<Player>();
	}
	
	void Update () {
        if (!player.grounded)
            player.jumpTime = 1f;
    }

    

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Platform"))
        {
            
            player.grounded = true;
        }
    }

    void OnTriggerExit2D(Collider2D col)
    {
        if (col.CompareTag("Platform"))
        {
            
            player.grounded = false;
        }
    }

}
