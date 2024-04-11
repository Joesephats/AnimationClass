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

public class SpikeTrap : MonoBehaviour
{
    Animator animator;

    // Start is called before the first frame update
    private void Start()
    {
        //get animator component
        animator = GetComponent<Animator>();
    }

    //if player enters trap collision, play spike animation
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            Debug.Log("Trap collide player");
            animator.SetTrigger("ActivateTrap");
        }
    }
}
