using UnityEngine;

public class Ricochet_Bullet : Bullet_Behaviour
{
    private int current_bounces = 0;

    protected override void OnEnable()
    {
        base.OnEnable();
        current_bounces = 0;
    }

    protected override void HandleImpact(Collider other)
    {
        //TODO Spawn Impact VFX According to surface type (use bulletData.impact_infos)
        
        // If it's an enemy, deal damage and destroy
        if (other.CompareTag("Enemy"))
        {
            Debug.Log("Hit enemy, stopping bullet!");
            
            //TODO Apply damage to target (use bulletData.bullet_base_damage and other info)

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
            // transform.forward = Vector3.Reflect(transform.forward, hit.normal);
            Debug.Log($"Bounced! {current_bounces}/{bullet_data.max_bounces}");
        }
    }
}