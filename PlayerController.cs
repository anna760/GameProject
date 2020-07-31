using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController: MonoBehaviour {
    public float moveSpeed;
    //public Rigidbody theRB;
    public float jumpForce;
    public CharacterController controller;

    private Vector3 moveDirection;
    public float gravityScale;

    public Animator anim;

    public Transform pivot;
    public float rotateSpeed; // To know how fast player rotates

    public GameObject playerModel;

    public float knockBackForce;
    public float knockBackTime;
    private float knockBackCounter;

	void Start () {
        //theRB = GetComponent<Rigidbody>();
        controller = GetComponent<CharacterController>();
	}
	
	void Update () {
        //theRB.velocity = new Vector3(Input.GetAxis("Horizontal") * moveSpeed, theRB.velocity.y, Input.GetAxis("Vertical") * moveSpeed);
        /*if (Input.GetKey(KeyCode.Space))
        {
            theRB.velocity = new Vector3(theRB.velocity.x, jumpForce, theRB.velocity.z);
        }*/

        //moveDirection = new Vector3(Input.GetAxis("Horizontal") * moveSpeed, moveDirection.y, Input.GetAxis("Vertical") * moveSpeed); // WASD player movement everywhere except y axis
        if (knockBackCounter <= 0) // Only called if knockback counter is 0 or less
        {
            float yStore = moveDirection.y;                                                                                         // To fix jumping, before applying movement type, store y direction we currently have and then...
            moveDirection = (transform.forward * Input.GetAxis("Vertical")) + (transform.right * Input.GetAxis("Horizontal"));      // Player moves the way in which the cam is rotated (up/down + left/right)
            moveDirection = moveDirection.normalized * moveSpeed;                                                                   // Normalizes speed of player if they press up+left or down+right at same time
            moveDirection.y = yStore;                                                                                               // ...apply the up/down movement we worked out previously^

            if (controller.isGrounded)                         // If controller is attached to ground, then you can press space - so player can't fly
            {
                moveDirection.y = 0f;                          // Gradually fall off edge instead of snapping onto ground (resets y value to 0 everytime it's being taken away)
                if (Input.GetKey(KeyCode.Space))
                {
                    moveDirection.y = jumpForce;
                }
            }
        }
        else
        {
            knockBackCounter -= Time.deltaTime;                                                   // Count downwards
        }
        moveDirection.y = moveDirection.y + (Physics.gravity.y * gravityScale * Time.deltaTime);  // Fall back down when jumping & smooth jump (go to position we want take away gravity scale in same direction)
        controller.Move(moveDirection * Time.deltaTime);                                          // Move player in given direction & Time.deltaTime - move smoother

        // Move player in different diractions based on camera look direction
        if(Input.GetAxis("Horizontal") != 0 || Input.GetAxis("Vertical") != 0)                    // As long as horizontal/vertical axis not 0
        {
            transform.rotation = Quaternion.Euler(0f, pivot.rotation.eulerAngles.y, 0f);                            // Rotate the player where pivot is facing
            Quaternion newRotation = Quaternion.LookRotation(new Vector3(moveDirection.x, 0f, moveDirection.z));    // Look towards point player is facing
            playerModel.transform.rotation = Quaternion.Slerp(playerModel.transform.rotation, newRotation, rotateSpeed * Time.deltaTime);   // Slerp - moves along arc smoothly, used for rotations
        }
        anim.SetBool("isGrounded", controller.isGrounded);                                                          // Checks to see if grounded
        anim.SetFloat("Speed", (Mathf.Abs(Input.GetAxis("Vertical")) + Mathf.Abs(Input.GetAxis("Horizontal"))));    // If any of these values are > 0, speed
    }

    public void Knockback(Vector3 direction)
    {
        knockBackCounter = knockBackTime;
        
        moveDirection = direction * knockBackForce;

        moveDirection.y = knockBackForce; // Knocked into air

    }
    
}
