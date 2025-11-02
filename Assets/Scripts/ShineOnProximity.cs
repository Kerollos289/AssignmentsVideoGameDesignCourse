using UnityEngine;

public class ShineOnProximity : MonoBehaviour
{
    public Transform player;      // assign your player
    public Light objectLight;     // assign the light component
    public float maxDistance = 5f; // distance at which it shines fully
    public float maxIntensity = 3f; // maximum light intensity
    public float smoothSpeed = 2f;  // how fast it changes

    void Update()
    {
        if (player == null || objectLight == null) return;

        float distance = Vector3.Distance(player.position, transform.position);
        float targetIntensity = Mathf.Clamp01(1 - distance / maxDistance) * maxIntensity;

        objectLight.intensity = Mathf.Lerp(objectLight.intensity, targetIntensity, Time.deltaTime * smoothSpeed);
    }
}
