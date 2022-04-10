using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLogic : MonoBehaviour
{
    [Header("Player Settings")] //Header in the inspector for organization
    [SerializeField] private float speed; //private float variable for movement speed that will appear in the inspector because it is serialized
    [SerializeField] private float jump; //private float variable for jump speed that will appear in the inspector because it is serialized
    [SerializeField] private float groundDist; //How far from the ground is considered onGround
    [SerializeField] private LayerMask groundMask;
    private bool onGround, doubleJump; //boolean to check if character is on ground and for double jump
    private Rigidbody2D rigidbody2D; //rigidbody2d variable to store reference to component
    private BoxCollider2D boxCollider2D; //boxcollider2d component

    // Start is called before the first frame update
    void Start()
    {
        rigidbody2D = GetComponent<Rigidbody2D>(); //Get the rigidbody2d component from our player
        boxCollider2D = GetComponent<BoxCollider2D>(); //Get the boxcollider2d
    }

    // Update is called once per frame
    void Update()
    {
        //onGround = Physics2D.Raycast(transform.position, Vector2.down, boxCollider2D.bounds.extents.y + groundDist, groundMask); //Raycast for capsule or spherical characters that don't have much surface area
        onGround = Physics2D.OverlapBox(transform.position + (Vector3) Vector2.down * boxCollider2D.bounds.extents.y, new Vector2(boxCollider2D.size.x, groundDist), 0f, groundMask); //For characters with large surface area on the bottom like squares
        
        if(onGround) doubleJump = false;

        float hor = Input.GetAxis("Horizontal"); //Get the horizontal input from our keyboard - -1 for left, 1 for right, 0 for no input
        if(hor != 0f) //Move the character only when we have an input
        {
            rigidbody2D.velocity = new Vector2(hor * speed, rigidbody2D.velocity.y); //set the velocity to the direction of the horizontal input, but maintain the y velocity to keep gravity
        }

        if(Input.GetButtonDown("Jump") && (onGround || !doubleJump)) //True if the space bar was pressed in this exact frame, false afterwards even if still pressed
        {
            if(!onGround) doubleJump = true;
            rigidbody2D.velocity += Vector2.up * jump; //Add a Vector2 with x value 0 and y value of jump to make the character go up
        }
    }

    #if UNITY_EDITOR //Compiles only in editor
    void OnDrawGizmos() //Gizmos in scene view
    {
        boxCollider2D = GetComponent<BoxCollider2D>(); //Runs independent of Start function so variables need to be initialized
       // Gizmos.DrawRay(transform.position, Vector2.down * (boxCollider2D.bounds.extents.y + groundDist)); //Visual representation of the Raycast
        Gizmos.DrawCube(transform.position + (Vector3) Vector2.down * boxCollider2D.bounds.extents.y, new Vector2(boxCollider2D.size.x, groundDist)); //Visual representation of the OverlapBox
    }
    #endif
}
