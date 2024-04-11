//////////////////////////////////////////////
//Assignment/Lab/Project: Animations
//Name: Tristin Gatt
//Section: SGD.213.2172
//Instructor: Brian Sowers
//Date: 04/10/2024
/////////////////////////////////////////////

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    //references to player components and door object

    Rigidbody playerRigidBody;
    [SerializeField] float speed;
    [SerializeField] GameObject doorObject;
    Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        //fill references
        playerRigidBody = GetComponent<Rigidbody>();
        animator = GetComponentInChildren<Animator>();
        doorObject = GameObject.FindGameObjectWithTag("Door");
    }

    // Update is called once per frame
    void Update()
    {
        //input controls and animations
        MovePlayer(GetInputDir());
        HandleMovementAnimation();
        Jump();
        InteractAnimation();

        //determines if idle
        if (playerRigidBody.velocity.magnitude < 1)
        {
            animator.SetBool("NotMoving", true);
        }
        else
        {
            animator.SetBool("NotMoving", false);
        }
    }

    //if player enters trap collision, call damage player method: plays hurt animation
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Trap")
        {
            DamagePlayer();
        }
    }

    //gets player movement inputs and calculates movement direction
    Vector3 GetInputDir()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        Vector3 inputDir = new Vector3(horizontalInput, 0, verticalInput);
        return inputDir;
    }

    //moves player based on getInputDir
    void MovePlayer(Vector3 moveDirection)
    {
        playerRigidBody.AddForce(moveDirection * speed * Time.deltaTime, ForceMode.VelocityChange);
    }

    //plays strafing animation based on user inputs
    void HandleMovementAnimation()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        animator.SetFloat("LeftAndRight", horizontalInput);
        animator.SetFloat("FrontAndBack", verticalInput);
    }

    //on space, plays jump animation and pushes player up
    void Jump()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            {
                playerRigidBody.AddForce(Vector3.up * 5, ForceMode.Impulse);
                animator.SetTrigger("Jumping");
            }
        }
    }

    //door interaction
    void InteractAnimation()
    {
        //check for input P
        if (Input.GetKeyDown(KeyCode.P))
        {
            //play push animation
            animator.SetTrigger("Push");

            //if the door is close enough, play door's opening animation.
            if ((doorObject.transform.position - transform.position).magnitude < 2)
            {
                doorObject.GetComponent<Animator>().SetTrigger("OpenDoor");
            }
        }
    }

    //plays player flinch animation, called when enters trap collision
    public void DamagePlayer()
    {
        animator.SetTrigger("Hurt");
    }

}
