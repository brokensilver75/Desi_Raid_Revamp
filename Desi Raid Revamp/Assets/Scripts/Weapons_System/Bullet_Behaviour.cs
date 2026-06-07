using UnityEngine;
using UnityEngine.Pool;

public class Bullet_Behaviour : MonoBehaviour
{
    public Bullet_SO bullet_data;

    protected IObjectPool<Bullet_Behaviour> managed_pool;

    protected float current_life_time;

    public void SetPool(IObjectPool<Bullet_Behaviour> pool)
    {
        managed_pool = pool;
    }

    protected virtual void OnEnable()
    {
        current_life_time = bullet_data.bullet_max_life_time;
    }

    protected virtual void Update()
    {
        transform.position += transform.forward * bullet_data.bullet_speed * Time.deltaTime;

        current_life_time -= Time.deltaTime;
        if (current_life_time <= 0)
        {
            managed_pool.Release(this);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        // Pass the collision data to our virtual method
        HandleImpact(other);
    }

    // The 'virtual' keyword lets us override this in other scripts!
    protected virtual void HandleImpact(Collider other)
    {
        Debug.Log($"Standard hit on {other.name} for {bullet_data.bullet_base_damage} damage!");

        //TODO Spawn Impact VFX According to surface type (use bulletData.impact_infos)

        //TODO Apply damage to target (use bulletData.bullet_base_damage and other info)


        // Standard bullet dies immediately on impact
        managed_pool.Release(this);
    }
}