using System.Collections;
using UnityEngine;
using UnityEngine.Pool;

[CreateAssetMenu(fileName = "Gun", menuName = "Guns/Gun")]
public class Gun_SO : ScriptableObject
{
    public Gun_Type type;

    public string gun_name;

    public int mag_size;

    private float last_shoot_time;

    public Shoot_Config_SO shoot_config;

    public Trail_Config_SO trail_config;

    private ParticleSystem shoot_system;

    private ObjectPool<TrailRenderer> trail_pool;

    public void Shoot()
    {
        if (Time.time > shoot_config.fire_rate + last_shoot_time)
        {
            Debug.Log($"[{gun_name}] SHOTS FIRED!!");
            last_shoot_time = Time.time;
            shoot_system.Play();

            Vector3 shoot_direction = shoot_system.transform.forward + new Vector3(
                Random.Range(-shoot_config.spread.x, shoot_config.spread.x),
                Random.Range(-shoot_config.spread.y, shoot_config.spread.y),
                Random.Range(-shoot_config.spread.z, shoot_config.spread.z));

            shoot_direction.Normalize();

            if (Physics.Raycast(shoot_system.transform.position, shoot_direction, out RaycastHit hit, float.MaxValue))
            {

            }
        }
    }

    public void Reset_Shoot_Time()
    {
        last_shoot_time = 0;
    }

    private TrailRenderer Create_Trail()
    {
        GameObject instance = new GameObject("Bullet Trail");
        TrailRenderer trail = instance.AddComponent<TrailRenderer>();

        trail.colorGradient = trail_config.gradient_color;
        trail.material = trail_config.material;
        trail.widthCurve = trail_config.width_curve;
        trail.time = trail_config.duration;
        trail.minVertexDistance = trail_config.min_vertex_distance;

        trail.emitting = false;
        trail.shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.Off;

        return trail;
    }

    IEnumerator Play_Trail(Vector3 start_point, Vector3 end_point, RaycastHit hit)
    {
        TrailRenderer instance = trail_pool.Get();
        instance.transform.position = start_point;
        yield return null;
        instance.emitting = true;
        float distance = Vector3.Distance(start_point, end_point);
        float remaining_distance = distance;
        while (remaining_distance > 0)
        {
            instance.transform.position = Vector3.Lerp(start_point, end_point, Mathf.Clamp01(1 - (remaining_distance / distance)));
            remaining_distance -= trail_config.simulation_speed * Time.deltaTime;
            yield return null;
        }

        instance.transform.position = end_point;

        if (hit.collider != null)
        {
            // TODO Logic for bullet collision
        }

        yield return new WaitForSeconds(trail_config.duration);

        yield return null;

        instance.emitting = false;
        instance.gameObject.SetActive(false);

        trail_pool.Release(instance);
    }


    //public int current_ammo;

    //public Vector3 spawn_point;
    //public Vector3 spawn_rotation;
}
