using UnityEngine;
using System.Collections;

public class SpeedPowerUp : MonoBehaviour
{
    public float duration = 5f;
    public float speedMultiplier = 1.5f;

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerController pc = other.GetComponent<PlayerController>();
            StartCoroutine(SpeedBoost(pc));
            Destroy(gameObject);
        }
    }

    IEnumerator SpeedBoost(PlayerController p)
    {
        float original = p.moveSpeed;
        p.moveSpeed *= speedMultiplier;

        yield return new WaitForSeconds(duration);

        p.moveSpeed = original;
    }
}
