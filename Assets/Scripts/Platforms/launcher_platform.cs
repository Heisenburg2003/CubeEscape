using UnityEngine;


public enum LaunchDirection 
{
    left,
    right,
    Up,
    Down
}
public class launcher_platform : MonoBehaviour
{
    [SerializeField] LaunchDirection launchDirection;
    [SerializeField] private float launchForce = 15f;


    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("Contact made");

            Rigidbody playerRb = collision.gameObject.GetComponent<Rigidbody>();
              Debug.Log("About to apply force");

              Vector3 direction = Vector3.zero;

              switch(launchDirection)
            {
                case LaunchDirection.Up:
                direction = new Vector3(0,1,0);
                break;
                case LaunchDirection.Down:
                direction = new Vector3(0,-1,0);
                break;
                case LaunchDirection.right:
                direction = new Vector3(0,0,-1);
                break;
                case LaunchDirection.left:
                direction = new Vector3(0,0,1);
                break;
            }
            playerRb.AddForce(direction * launchForce,ForceMode.Impulse);
              Debug.Log("force applied: " + launchForce);
            
        }
    }

}
