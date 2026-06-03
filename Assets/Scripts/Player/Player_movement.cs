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


        [SerializeField] // to give the value access in the inspector , can change the value in the inspector 
        float jumpForce = 5f;

        [SerializeField]
        float push = 5f;

        Rigidbody rb;

        private void Awake()
        {
            rb = GetComponent<Rigidbody>();

            if(rb == null)
            {
                Debug.LogError("No rigidbody found!");
            }
        }

        private void OnEnable()
        {
            jump.Enable(); 
            left.Enable();
            right.Enable();
        }

        private void OnDisable()
        {
            jump.performed -= OnJump;
            jump.Disable();
        }
        
        private void FixedUpdate()
        {
            if(jump.IsPressed())
            {
                rb.AddForce(Vector3.up* jumpForce,ForceMode.Impulse);
                Debug.Log("jump!");
            }
            if(left.IsPressed())
            {
                rb.AddForce(new Vector3(0,0,1)*push,ForceMode.Impulse);
                Debug.Log("go left!");

            }
            if(right.IsPressed())
            {
                rb.AddForce(new Vector3(0,0,-1)*push,ForceMode.Impulse);
                Debug.Log("go right!");
            }
        }

        private void OnJump(InputAction.CallbackContext context)
        {
            Debug.Log("Jump is Pressed!");
        }
    }
}