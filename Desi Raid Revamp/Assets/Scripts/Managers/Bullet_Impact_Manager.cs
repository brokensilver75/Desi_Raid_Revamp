using UnityEngine;

public class Bullet_Impact_Manager : MonoBehaviour
{
    public static Bullet_Impact_Manager instance { get; private set; }

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            instance = this;
        }
    }

    public void Handle_Bullet_Impact(Vector3 position, Vector3 normal, GameObject hit_object)
    {
        // Here you can implement the logic to handle bullet impact effects
        // For example, you can instantiate a particle effect at the impact position
        // and orient it based on the normal of the surface hit.
        // Example:
        // Instantiate(impactEffectPrefab, position, Quaternion.LookRotation(normal));
        Debug.Log($"Bullet impacted at {position} on object {hit_object.name}");
    }

}
