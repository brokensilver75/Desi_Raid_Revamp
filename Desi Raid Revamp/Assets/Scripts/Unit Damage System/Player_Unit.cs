using UnityEngine;
using UnityEngine.Rendering;

public class Player_Unit : Unit
{
    public override void Take_Damage(float damage)
    {
        base.Take_Damage(damage);

        Debug.Log($"[Player_Unit] Player unit took {damage} damage. Current health: {current_health}");
    }

    public override void Die ()
    {
        Debug.Log($"[Player_Unit] Player unit has died. Game Over!");
    }
}
