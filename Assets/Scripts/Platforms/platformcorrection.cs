using UnityEngine;

public class platformcorrection : MonoBehaviour
{
    [SerializeField] float pullSpeed = 8f;

    private void OnTriggerStay(Collider other)
    {
        if (!other.CompareTag("Player")) return;

        Rigidbody rb = other.attachedRigidbody;
        if (rb == null || rb.linearVelocity.y > -0.1f) return;

        Vector3 pos = rb.position;
        pos.z = Mathf.MoveTowards(pos.z, transform.position.z, pullSpeed * Time.fixedDeltaTime);
        rb.MovePosition(pos);
    }
}