using UnityEngine;

public class Ricochet_Bullet : Bullet_Behaviour
{
    private int current_bounces = 0;

    public override void OnEnable()
    {
        base.OnEnable();
        current_bounces = 0;
    }

    public override void HandleImpact(Collider other)
    {
        //TODO Spawn Impact VFX According to surface type (use bulletData.impact_infos)

        // If it's a unit, deal damage and destroy
        if (other.TryGetComponent(out Player_Unit player_unit))
        {
            Debug.Log("Hit player, stopping bullet!");

            player_unit.Take_Damage(bullet_data.bullet_base_damage);
            
            managed_pool.Release(this);
            return;
        }

        else if (other.TryGetComponent(out Enemy_Unit enemy_unit))
        {
            Debug.Log("Hit enemy, stopping bullet!");

            enemy_unit.Take_Damage(bullet_data.bullet_base_damage);

            managed_pool.Release(this);
            return;
        }

        else if (other.TryGetComponent(out Training_Dummy_Unit training_dummy_unit))
        {
            Debug.Log("Hit training dummy, stopping bullet!");
            training_dummy_unit.Take_Damage(bullet_data.bullet_base_damage);
            managed_pool.Release(this);
            return;
        }

            // If it's a wall, bounce!
            current_bounces++;

        if (current_bounces > bullet_data.max_bounces)
        {
            managed_pool.Release(this);
        }
        else
        {
            // Simple bounce math (requires a Raycast to find the wall's normal vector)
            if (Physics.Raycast(transform.position, transform.forward, out RaycastHit hit, 1f))
            {
                transform.forward = Vector3.Reflect(transform.forward, hit.normal);
            }

            Debug.Log($"Bounced! {current_bounces}/{bullet_data.max_bounces}");
        }
    }
}