using System;
using NUnit.Framework;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Player{   
public class PlayerMovement : MonoBehaviour
    {
        [SerializeField] InputAction jump; // space for jump
        [SerializeField] InputAction left;
        [SerializeField] InputAction right; 
        [SerializeField] float fallMultiplier = 5f;
        float airtimer; 
        [SerializeField] float jumpForce = 5f;
        [SerializeField] private float holdForce = 15f;
        [SerializeField] private float maxHoldTime = 0.2f;
        [SerializeField] float push = 5f;

        Rigidbody rb;
        private float holdTimer;
        private bool isHoldingJump;
        bool jumpRequest;
        bool leftKey;
        bool rightKey;
        bool isGrounded;
        bool isleftWall;
        bool isrightWall;
        int jumpdirection;
        float targetRotationX;
        float rotationSpeed = 720f;
        
      
        
        
        private void Awake()
        {
            rb = GetComponent<Rigidbody>();

            if(rb == null)
            {
                Debug.LogError("No rigidbody found!");
            }
            airtimer = 0f;
        }

        private void OnEnable()
        {
            jump.Enable(); 
            left.Enable();
            right.Enable();
        }

        private void OnDisable()
        {
            jump.Disable();
            left.Disable();
            right.Disable();
        }
        
        private void Update()
        {
            
            if(jump.WasPressedThisFrame() && isGrounded)
            {
                jumpRequest = true;

                Debug.Log(jumpRequest);
                
            }
            if (jump.WasReleasedThisFrame())
            {
                isHoldingJump = false;
            }
            if(isGrounded){
            if(left.IsPressed() && !isleftWall)
            {
                leftKey = true;
                jumpdirection = -1;
                

            }
            if(right.IsPressed() && !isrightWall)
            {
                rightKey = true;
                jumpdirection = 1;
            }
            }
            transform.rotation = Quaternion.RotateTowards(transform.rotation,Quaternion.Euler(targetRotationX,0,0),rotationSpeed * Time.deltaTime);
        }
            

        private void FixedUpdate()
        {
            if(airtimer > 0 )
            {
                airtimer -= Time.fixedDeltaTime;
                
                if(airtimer < 0)
                {
                 airtimer = 0;
                }
            }
            if(airtimer <= 0 && rb.linearVelocity.y < 0 ) //initiate heavy fall 
            {
             rb.AddForce(Vector3.up * Physics.gravity.y * (fallMultiplier - 1),ForceMode.Acceleration ); 
            }

            if(jumpRequest && isGrounded)
            {
                jumpRequest = false; //key request reset 
                isGrounded = false;
                rb.AddForce(Vector3.up* jumpForce,ForceMode.Impulse);

                isHoldingJump = true;
                holdTimer = maxHoldTime;
                // Debug.Log("jump!");

                if(jumpdirection == -1)
                {
                targetRotationX += 90f;
                Debug.Log("Target Rotation: " + targetRotationX);

                }
                if(jumpdirection == 1)
                {
                targetRotationX -= 90f;
                Debug.Log("Target Rotation: " + targetRotationX);

                }   
            }           
                    

if (isHoldingJump)
{
    if (jump.IsPressed() &&
        holdTimer > 0f &&
        rb.linearVelocity.y > 0f)
    {
        rb.AddForce(Vector3.up * holdForce, ForceMode.Acceleration);
        holdTimer -= Time.fixedDeltaTime;
    }

    if (holdTimer <= 0f || !jump.IsPressed())
    {
        isHoldingJump = false;
    }
}
            

            if(leftKey && !isGrounded  && !isleftWall)
            {
                leftKey = false;  //key request reset
                rb.AddForce(new Vector3(0,0,1)*push,ForceMode.Impulse);
                // Debug.Log("go left!");
            }
            if(rightKey && !isGrounded && !isrightWall)
            {
                rightKey = false;  //key request reset
                rb.AddForce(new Vector3(0,0,-1)*push,ForceMode.Impulse);
                // Debug.Log("go right!");
            }
            
        }


        private void OnCollisionEnter(Collision collision)
        {
            if(collision.gameObject.CompareTag("ground"))
            {
                isGrounded = true;
                // Debug.Log("is Grounded");
            }
            if(collision.gameObject.CompareTag("left wall"))
            {
                isleftWall = true;
                // Debug.Log("contact with the left wall");
            }
            if(collision.gameObject.CompareTag("right wall"))
            {
                isrightWall = true;
                // Debug.Log("contact with the right wall");
            }
        }
        private void OnCollisionExit(Collision collision)
        {
            if(collision.gameObject.CompareTag("ground"))
            {
                isGrounded = false;
                // Debug.Log("is not Grounded");
            }
            if(collision.gameObject.CompareTag("left wall"))
            {
                isleftWall = false;
                // Debug.Log("bye bye left wall");
            }
            if(collision.gameObject.CompareTag("right wall"))
            {
                isrightWall = false;
                // Debug.Log("bye bye right wall");
            }
        }




      
        private void OnJump(InputAction.CallbackContext context)
        {
            Debug.Log("Jump is Pressed!");
        }
    }
}