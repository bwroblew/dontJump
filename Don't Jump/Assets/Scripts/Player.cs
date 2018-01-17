using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour {

    public bool grounded;
    public JumpButton jumpButton;
    public Rigidbody2D rb2d;
    public Image bar;

    public MainManager mm;

    public float jumpTime;
    public float maxMul = 1.7f;
    public const float jumpForce = 350f;

    public bool gr;

    public float currSpeed;
    public float maxSpeed;

    private Animator anim;
    
	void Start () {
        rb2d = this.GetComponent<Rigidbody2D>();
        anim = gameObject.GetComponent<Animator>();
        jumpTime = 1f;
        currSpeed = 0f;
        maxSpeed = 6f;
	}
	
	void FixedUpdate () {
        
        Vector3 newPos = new Vector3(Time.deltaTime * currSpeed, 0, 0);
        transform.position = transform.position + newPos;
        anim.SetBool("Grounded", grounded);
        bar.GetComponent<RectTransform>().localScale = new Vector2((jumpTime - 1) * 10 / 7, 1);
        if (jumpButton.isPres() && grounded)
        {
            if(jumpTime < maxMul)
            {
                jumpTime += Time.deltaTime;
            }
            else
            {
                jumpTime = maxMul;
                Jump();
            }
        }
        else if (grounded)
        {
            Jump();
        }
        
	}

    public void Jump()
    {
        if (jumpTime > maxMul)
            jumpTime = maxMul;
        //rb2d.Sleep();
        //rb2d.WakeUp();
        rb2d.velocity = new Vector3(rb2d.velocity.x, jumpForce * jumpTime * 0.019f, 0);
        //rb2d.AddForce(new Vector2(0, jumpForce * jumpTime));
        jumpTime = 1f;
    }

    public void Die()
    {

        mm.FinishRound();
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        gr = true;
    }

    void OnCollisionExit2D(Collision2D col)
    {
        gr = false;
    }
    
}
