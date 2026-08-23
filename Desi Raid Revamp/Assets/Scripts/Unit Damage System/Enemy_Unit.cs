using UnityEngine;

public class Enemy_Unit : Unit
{
    public override void Take_Damage(float damage)
    {
        base.Take_Damage(damage);
        Debug.Log($"[Enemy_Unit] Enemy unit took {damage} damage. Current health: {current_health}");
    }

    public override void Die()
    {
        Debug.Log($"[Enemy_Unit] Enemy unit has died.");
        gameObject.SetActive(false); // Deactivate the enemy unit instead of destroying it
    }
}
