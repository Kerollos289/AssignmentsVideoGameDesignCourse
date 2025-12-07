using UnityEngine;

public class HealthPickup : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            other.GetComponent<PlayerHealth>().Heal();
            Destroy(gameObject);
        }
    }
}
