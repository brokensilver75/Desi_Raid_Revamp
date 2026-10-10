using UnityEngine;

public class Unit : MonoBehaviour
{
    public float current_health;
    public float max_health;

    public virtual void Take_Damage(float damage)
    {
        current_health -= damage;

        if (current_health <= 0)
        {
            current_health = 0;
            Die();
        }
    }

    public virtual void Show_Blood(Vector3 impact_point, Vector3 bullet_direction)
    {
        Debug.Log($"[Unit] Show blood effect at {impact_point}");
    }

    public virtual void Die()
    {
        //TODO: Implement death logic for unit
        Debug.Log($"[Unit] Unit has died");
    }
}
