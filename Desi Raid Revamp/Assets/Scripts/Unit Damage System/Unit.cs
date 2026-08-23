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
            Die();
        }
    }

    public virtual void Die(string unit_name = null)
    {
        //TODO: Implement death logic for unit
        Debug.Log($"[Unit] Unit {unit_name} has died");
    }
}
