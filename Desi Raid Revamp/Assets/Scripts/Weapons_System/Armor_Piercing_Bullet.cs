using UnityEngine;

public class Armor_Piercing_Bullet : Bullet_Behaviour
{
    private int current_penetrations = 0;

    protected override void OnEnable()
    {
        base.OnEnable(); // Call the parent's timer setup
        current_penetrations = 0; // Reset our pierce count
    }

    protected override void HandleImpact(Collider other)
    {
        Debug.Log($"Pierced through {other.name}!");
        current_penetrations++;

        //TODO Spawn Impact VFX According to surface type (use bulletData.impact_infos)

        //TODO Apply damage to target (use bulletData.bullet_base_damage and other info)

        // Only return to pool if we've hit our maximum penetrations
        if (current_penetrations >= bullet_data.max_penetrations)
        {
            managed_pool.Release(this);
        }
    }
}