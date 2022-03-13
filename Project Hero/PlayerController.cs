using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerController : MonoBehaviour
{
    // basic calls to get sprite moving
    private Rigidbody2D rb;
    public float moveSpeed;
    private float moveInput;
    //see void flip \/
    //private bool facingRight = true;

    // basic calls for jumping
    private bool OnGround;
    //transform is for x,y coordinate of check to see if overlap is on terrain (public because a different script needs to see the x,y coordinates
    public Transform groundCheck; //this makes it so we're looking for the xy coordinates of groundCheck thats what transform does not move it
    public float checkRadius;
    public LayerMask whatisGround; // lets you select in inspector what layer counts as ground

    public float jumpForce;
    //The max amount of time Jumping will be read for \/
    private float jumpTimerMax;
    public float jumpTime;
    private bool isJumping;

    // basic calls for wall jumping
    private bool OnWall;
    public Transform wallCheck;
    public float wallCheckRadius;
    public LayerMask whatisWall;


    // Start is called before the first frame update
    void Start()
    {
        //ridgidbody reference
        rb = GetComponent<Rigidbody2D>();
        
    }

    // FixedUpdate put physics here
    private void FixedUpdate()
    {

        moveInput = Input.GetAxis("Horizontal");
        rb.velocity = new Vector2(moveInput * moveSpeed, rb.velocity.y);

        // https://youtu.be/j111eKN8sJw?t=420 for variable jump (some variables are deliberatly changed from the video ex. jumpTimeCounter as jumpTimerMax this was done purposfully 

        OnGround = Physics2D.OverlapCircle(groundCheck.position, checkRadius, whatisGround);
        OnWall = Physics2D.OverlapCircle(wallCheck.position, wallCheckRadius, whatisWall);


    }



    void Flip() // the explaination for this is so bad I refuse to implement it without understanding how the underlying code works
    {



    }

    // Update is called once per frame
    void Update()

    {
        /* so how the jumping works
       OnGround is a true/false varaible the value is determined by groundCheck.Position
       which looks at the xy coordinates for where the checkradius should be,
       checkRadius is how large the radius is,
       and whatIsGround looks for the layer labelled ground
       when the layermask is oversomthing labelled ground TRUE is sent to the OnGround bool variable 
       */

        //-----------------------------------------------------------------------------------------------------------------
        // OnGround = Physics2D.OverlapCircle(groundCheck.position, checkRadius, whatisGround);

        //GetKeyDown only checks if the key was pressed GetKey checks each second and will return false when no longer held
        if (OnGround == true && Input.GetKeyDown(KeyCode.Space))
        {
            isJumping = true;
            jumpTimerMax = jumpTime;
            rb.velocity = Vector2.up * jumpForce;
        }


        // the way this works is jumpTimerMax is great than zero when first just key (space) is pressed,
        // which lets the player jump giving the variable jump height however jumpTimerMax will gradually reach 0 due to time.deltatime effectivly timing out the jump
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

        if (Input.GetKeyUp(KeyCode.Space))
        {
            isJumping = false;

        }

        // wall jumping code starts here

        //  OnWall = Physics2D.OverlapCircle(wallCheck.position, wallCheckRadius, whatisWall);

        if (OnWall == true)

        {
            rb.gravityScale = 0;
            
        }
        else
        
            rb.gravityScale = 5;
            
        if (Input.GetKeyDown(KeyCode.LeftControl)) 
        {
            rb.gravityScale = 5;
        }








        /*
         * this was code used to get character moving and worked but not as needed
         * 
            if (Input.GetKeyDown(KeyCode.RightArrow))
            {
                rb.AddForce(new Vector2(MoveSpeed, 0));
            }
                
            else if (Input.GetKeyDown(KeyCode.LeftArrow))
            {
               rb.AddForce(new Vector2(-MoveSpeed, 0));
            }
        */


    }
}
