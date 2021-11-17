using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{   
    public CharacterController charController;

    public float movementSpeed = 10f;

    //Falldown on Y axis
    public float gravity = -9.81f;
    Vector3 velocity;

    //Ground Check
    public Transform groundCheck;
    public float groundDistance;
    public LayerMask groundMask;
    bool isGrounded;

    //Jumping
    public float jumpHeight = 5f;

 


    void Update()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        if(isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }

        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 movement = transform.right * x + transform.forward * z;

        charController.Move(movement * Time.deltaTime * movementSpeed);

        if(Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {

            velocity.y = Mathf.Sqrt(jumpHeight * -2 * gravity);

        }

        velocity.y += gravity * Time.deltaTime;

        charController.Move(velocity * Time.deltaTime);
    }
}
