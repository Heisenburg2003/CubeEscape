using UnityEngine;

public class lavaMovement : MonoBehaviour
{
    [SerializeField] private float lavaSpeed = 15f;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += Vector3.up * lavaSpeed * Time.deltaTime;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            Debug.Log("Player Died");
        }
    }
}


