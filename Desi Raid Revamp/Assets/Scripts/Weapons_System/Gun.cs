using UnityEngine;
using UnityEngine.Pool;

public class Gun : MonoBehaviour
{
    private const int MAX_BULLET_POOL_SIZE = 100;
    private const int DEFAULT_BULLET_POOL_SIZE = 30;

    [SerializeField] private Gun_SO gun_SO;
    [SerializeField] private Transform gun_barrel_transform;

    int total_ammo;
    [SerializeField] int current_ammo;

    float last_shot_time;
    float last_burst_time;

    IObjectPool<Bullet_Behaviour> bullet_pool;

    void Start()
    {
        total_ammo = gun_SO.mag_size;
        current_ammo = gun_SO.mag_size;

        bullet_pool = new ObjectPool<Bullet_Behaviour>(
            createFunc: Create_Bullet,
            actionOnGet: On_Get_Bullet,
            actionOnRelease: On_Release_Bullet,
            actionOnDestroy: On_Destroy_Bullet,
            collectionCheck: false,
            defaultCapacity: DEFAULT_BULLET_POOL_SIZE,
            maxSize: MAX_BULLET_POOL_SIZE
            );
        
    }

    public void Shoot()
    {
        if (current_ammo > 0)
        {
            current_ammo--;

            Bullet_Behaviour fired_bullet = bullet_pool.Get();
        }

        else
        {
            Debug.Log("[Gun] Not enough ammo");
        }
    }

    public void Add_Ammo(int ammo_amt)
    {
        current_ammo += ammo_amt;

        current_ammo = Mathf.Clamp(current_ammo, 0, total_ammo);
    }

    public string Get_Gun_Name()
    {
        return gun_SO.gun_name;
    }

    public int Get_Current_Ammo()
    {
        return current_ammo;
    }

    public int Get_Total_Ammo()
    {
        return total_ammo;
    }

    public Fire_Mode Get_Fire_Mode()
    {
        return gun_SO.fire_mode;
    }

    public float Get_Fire_Rate()
    {
        return gun_SO.fire_rate;
    }

    public float Get_Time_Since_Last_Shot()
    {
        return Time.time - last_shot_time;
    }

    public float Get_Time_Since_Last_Burst()
    {
        return Time.time - last_burst_time;
    }

    public int Get_Burst_Amount()
    {
        return gun_SO.burst_amt;
    }

    public float Get_Burst_Rate()
    {
        return gun_SO.burst_rate;
    }

    public void Set_Last_Shot_Time(float time)
    {
        last_shot_time = time;
    }

    public void Set_Last_Burst_Time(float time)
    {
        last_burst_time = time;
    }

    #region Bullet Pooling
    private Bullet_Behaviour Create_Bullet()
    {
        Bullet_Behaviour bullet_instance = Instantiate(gun_SO.bullet_ammo_type.bullet_prefab);
        bullet_instance.SetPool(bullet_pool);
        return bullet_instance;
    }

    private void On_Get_Bullet(Bullet_Behaviour bullet)
    {
        bullet.transform.position = gun_barrel_transform.position;
        bullet.transform.rotation = gun_barrel_transform.rotation;
        bullet.gameObject.SetActive(true);
    }

    private void On_Release_Bullet(Bullet_Behaviour bullet)
    {
        bullet.gameObject.SetActive(false);
    }

    private void On_Destroy_Bullet(Bullet_Behaviour bullet)
    {
        Destroy(bullet.gameObject);
    }

    #endregion
}
