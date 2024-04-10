using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    Rigidbody playerRigidBody;
    [SerializeField] float speed;

    [SerializeField] GameObject doorObject;

    Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        playerRigidBody = GetComponent<Rigidbody>();
        animator = GetComponentInChildren<Animator>();
        doorObject = GameObject.FindGameObjectWithTag("Door");
    }

    // Update is called once per frame
    void Update()
    {
        MovePlayer(GetInputDir());
        HandleMovementAnimation();
        Jump();
        InteractAnimation();

        if (playerRigidBody.velocity.magnitude < 1)
        {
            animator.SetBool("NotMoving", true);
        }
        else
        {
            animator.SetBool("NotMoving", false);
        }
    }

    Vector3 GetInputDir()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        Vector3 inputDir = new Vector3(horizontalInput, 0, verticalInput);
        return inputDir;
    }

    void MovePlayer(Vector3 moveDirection)
    {
        playerRigidBody.AddForce(moveDirection * speed * Time.deltaTime, ForceMode.VelocityChange);
    }

    void HandleMovementAnimation()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        animator.SetFloat("LeftAndRight", horizontalInput);
        animator.SetFloat("FrontAndBack", verticalInput);
    }

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
    void InteractAnimation()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            animator.SetTrigger("Push");
            Debug.Log((doorObject.transform.position - transform.position).magnitude);
            if ((doorObject.transform.position - transform.position).magnitude < 2)
            {
                doorObject.GetComponent<Animator>().SetTrigger("OpenDoor");
            }
        }
    }

}
