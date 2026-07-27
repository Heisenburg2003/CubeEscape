using UnityEngine;
using System.Collections;

public class SteamHazard : MonoBehaviour
{
    [SerializeField] private GameObject steamEffect;
    [SerializeField]  private Collider damageCollider;


    [SerializeField] private float activeTime = 1f;
    [SerializeField] private float inactiveTime = 1.5f;
    [SerializeField] private float startDelay = 0f;


    private void Start()
    {
        StartCoroutine(SteamLoop());
    }

    private IEnumerator SteamLoop()
    {
        yield return new WaitForSeconds(startDelay);
        while (true)
        {
            steamEffect.SetActive(true);
            damageCollider.enabled = true;

            yield return new WaitForSeconds(activeTime);

            steamEffect.SetActive(false);
            damageCollider.enabled = false;

            yield return new WaitForSeconds(inactiveTime);
        }
    }
     private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            Debug.Log("Player Hit");
        }
    }
}