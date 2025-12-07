using UnityEngine;

public class KeyPickup : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Destroy(gameObject);
            UIManager.instance.hasKey = true;
            UIManager.instance.UpdateObjective("Find the door!");
            
        }
    }
}
