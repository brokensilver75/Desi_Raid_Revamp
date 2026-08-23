using UnityEngine;

public class Armor_Piercing_Bullet : Bullet_Behaviour
{
    private int current_penetrations = 0;

    public override void OnEnable()
    {
        base.OnEnable(); // Call the parent's timer setup
        current_penetrations = 0; // Reset our pierce count
    }

    public override void HandleImpact(Collider other)
    {
        Debug.Log($"Pierced through {other.name}!");
        current_penetrations++;

        //TODO Spawn Impact VFX According to surface type (use bulletData.impact_infos)

        //TODO Apply damage to target (use bulletData.bullet_base_damage and other info)
        if (other.TryGetComponent(out Player_Unit player_unit))
        {
            player_unit.Take_Damage(bullet_data.bullet_base_damage);
        }

        else if (other.TryGetComponent(out Enemy_Unit enemy_unit))
        {
            enemy_unit.Take_Damage(bullet_data.bullet_base_damage);
        }

        else if (other.TryGetComponent(out Training_Dummy_Unit training_dummy_unit))
        {
            training_dummy_unit.Take_Damage(bullet_data.bullet_base_damage);
        }

        // Only return to pool if we've hit our maximum penetrations
        if (current_penetrations >= bullet_data.max_penetrations)
        {
            managed_pool.Release(this);
        }
    }
}