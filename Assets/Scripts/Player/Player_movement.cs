using System;
using NUnit.Framework;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Player{
public class PlayerMovement : MonoBehaviour
    {
        [SerializeField] // to give the value access in the inspector , can change the value in the inspector 
        InputAction jump; // space for jump
        [SerializeField]
        InputAction left;
        [SerializeField]
        InputAction right;
        
        [SerializeField]
        float airControlDuration = 2f;
        [SerializeField]
        float fallMultiplier = 5f;
        float airtimer; 
        [SerializeField] // to give the value access in the inspector , can change the value in the inspector 
        float jumpForce = 5f;

        [SerializeField]
        float push = 5f;

        Rigidbody rb;
        bool jumpRequest;
        bool leftKey;
        bool rightKey;
        bool isGrounded;
        bool isleftWall;
        bool isrightWall;
        
        
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
            // if(airtimer > 0)
            // {
            //     airtimer -= Time.fixedDeltaTime;
            
            if(jump.WasPressedThisFrame() && isGrounded)
            {
                jumpRequest = true;

                Debug.Log(jumpRequest);
                // rb.AddForce(Vector3.up* jumpForce,ForceMode.Impulse);
                // airtimer = airControlDuration;
                // Debug.Log("jump!");
                // Debug.Log(airtimer);
            }
            if(left.IsPressed() && !isleftWall)
            {
                leftKey = true;
                // rb.AddForce(new Vector3(0,0,1)*push,ForceMode.Impulse);
                // Debug.Log("go left!");

            }
            if(right.IsPressed() && !isrightWall)
            {
                rightKey = true;
                // rb.AddForce(new Vector3(0,0,-1)*push,ForceMode.Impulse);
                // Debug.Log("go right!");
            }
            }
            
// }
        private void FixedUpdate()
        {
            if(airtimer > 0)
            {
                airtimer -= Time.fixedDeltaTime;
                

                if(airtimer <= 0)
            {
                 airtimer = 0;
                 Debug.Log(airtimer);
            }

                if(airtimer < 0 && rb.linearVelocity.y < 0)
            {
            rb.AddForce(Vector3.up * Physics.gravity.y * (fallMultiplier - 1),ForceMode.Acceleration );
            }
            }
            if(jumpRequest && isGrounded)
            {
                jumpRequest = false; //key request reset 
                isGrounded = false;
                rb.AddForce(Vector3.up* jumpForce,ForceMode.Impulse);
                airtimer = airControlDuration;
                Debug.Log("jump!");
                
                
            }
            if(leftKey && !isGrounded  && !isleftWall)
            {
                leftKey = false;  //key request reset
                rb.AddForce(new Vector3(0,0,1)*push,ForceMode.Impulse);
                Debug.Log("go left!");
            }
            if(rightKey && !isGrounded && !isrightWall)
            {
                rightKey = false;  //key request reset
                rb.AddForce(new Vector3(0,0,-1)*push,ForceMode.Impulse);
                Debug.Log("go right!");
            }
            
        }


        private void OnCollisionEnter(Collision collision)
        {
            if(collision.gameObject.CompareTag("ground"))
            {
                isGrounded = true;
                Debug.Log("is Grounded");
            }
            if(collision.gameObject.CompareTag("left wall"))
            {
                isleftWall = true;
                Debug.Log("contact with the left wall");
            }
            if(collision.gameObject.CompareTag("right wall"))
            {
                isrightWall = true;
                Debug.Log("contact with the right wall");
            }
        }
        private void OnCollisionExit(Collision collision)
        {
            if(collision.gameObject.CompareTag("ground"))
            {
                isGrounded = false;
                Debug.Log("is not Grounded");
            }
            if(collision.gameObject.CompareTag("left wall"))
            {
                isleftWall = false;
                Debug.Log("bye bye left wall");
            }
            if(collision.gameObject.CompareTag("right wall"))
            {
                isrightWall = false;
                Debug.Log("bye bye right wall");
            }
        }




      
        private void OnJump(InputAction.CallbackContext context)
        {
            Debug.Log("Jump is Pressed!");
        }
    }
}