using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerControllerCurrent : MonoBehaviour
{

    //Variables 
    private Rigidbody2D rb;
    public float moveSpeed;
    private float moveInput;
    //see void flip \/
    private bool facingRight = true;

    //transform is for x,y coordinate of check to see if overlap is on terrain (public because a different script needs to see the x,y coordinates
    private bool OnTerrain;
    public Transform TerrainCheck;
    public float checkRadius;
    public LayerMask whatisTerrain; // lets you select in inspector what layer counts as ground

    //Jumping
    public float jumpForce;
    private float jumpTimerMax;
    public float jumpTime;
    private bool isJumping;


    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        moveInput = Input.GetAxis("Horizontal");
        rb.velocity = new Vector2(moveInput * moveSpeed, rb.velocity.y);
        OnTerrain = Physics2D.OverlapCircle(TerrainCheck.position, checkRadius, whatisTerrain);


    }

    void Update()
    {
        if (OnTerrain == true && Input.GetKeyDown(KeyCode.Space))
        {
            isJumping = true;
            jumpTimerMax = jumpTime;
            rb.velocity = Vector2.up * jumpForce;
        }


        // likely a better version of the freeze would involve setting the rb type to kenematic
        //then back to dynamic on button release
        if (OnTerrain == true && Input.GetKey(KeyCode.LeftControl))
        {

            isJumping = false;
            jumpTimerMax = jumpTime;
            rb.gravityScale = 0;
            rb.velocity = new Vector2(0,0);

        }

        else
        {
            rb.gravityScale = 5;
            rb.velocity = new Vector2 (rb.velocity.x, rb.velocity.y);
        }

        if (Input.GetKey(KeyCode.Space) && isJumping == true)
        {
            if (jumpTimerMax > 0)
            {
                rb.velocity = Vector2.up * jumpForce;
                jumpTimerMax -= Time.deltaTime;
            }
            else
            {
                isJumping = false;

            }
        }

        if (facingRight == false && moveInput > 0)
        {
            Flip();
        }
        else if (facingRight == true && moveInput < 0)
        {
            Flip();
        }
    }
    

    void Flip()
    {
        facingRight = !facingRight;
        Vector3 Scaler = transform.localScale;
        Scaler.x *= -1;
        transform.localScale = Scaler;

    }

    private void OnCollisionEnter2D(Collision2D collision) //Bounce pad collision 
    {
        
    }














































































    //Da End
}