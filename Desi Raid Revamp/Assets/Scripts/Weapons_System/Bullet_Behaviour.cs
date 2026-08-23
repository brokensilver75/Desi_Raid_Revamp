using UnityEngine;
using UnityEngine.Pool;

public class Bullet_Behaviour : MonoBehaviour
{
    public Bullet_SO bullet_data;

    public IObjectPool<Bullet_Behaviour> managed_pool;

    public float current_life_time;

    public LayerMask ignore_layer_mask;
    public LayerMask damage_layer_mask;

    public void SetPool(IObjectPool<Bullet_Behaviour> pool)
    {
        managed_pool = pool;
    }

    public virtual void OnEnable()
    {
        current_life_time = bullet_data.bullet_max_life_time;
    }

    public virtual void Update()
    {
        float bullet_move_distance = bullet_data.bullet_speed * Time.deltaTime;

        if (Physics.Raycast(transform.position, transform.forward, out RaycastHit hit, bullet_move_distance, ~ignore_layer_mask))
        {
            //transform.position = hit.point; // Move the bullet to the point of impact

            HandleImpact(hit.collider);

            return; // Exit early since we've hit something
        }

        transform.position += transform.forward * bullet_data.bullet_speed * Time.deltaTime;

        current_life_time -= Time.deltaTime;
        if (current_life_time <= 0)
        {
            managed_pool.Release(this);
        }
    }

    // The 'virtual' keyword lets us override this in other scripts!
    public virtual void HandleImpact(Collider other)
    {
        Debug.Log($"Standard hit on {other.name} for {bullet_data.bullet_base_damage} damage!");

        //TODO Spawn Impact VFX According to surface type (use bulletData.impact_infos)

        //TODO Apply damage to target (use bulletData.bullet_base_damage and other info)
        // If it's a unit, deal damage and destroy
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

        // Standard bullet dies immediately on impact
        managed_pool.Release(this);
    }
}