using UnityEngine;

public class lavaMovement : MonoBehaviour
{
    [SerializeField] private float lavaSpeed = 15f;

    void Update()
    {
        transform.position += Vector3.up * lavaSpeed * Time.deltaTime;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            PlayerDeath playerDeath = other.GetComponent<PlayerDeath>();

            if (playerDeath != null)
            {
                playerDeath.Die();

            }
             Debug.Log("Player made contact with lava");
        
        }
    }
}


