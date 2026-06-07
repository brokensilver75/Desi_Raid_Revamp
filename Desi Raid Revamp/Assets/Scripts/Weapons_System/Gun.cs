using System;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class Gun : MonoBehaviour
{
    [SerializeField] private Gun_SO gun_SO;

    int total_ammo;
    [SerializeField] int current_ammo;

    float last_shot_time;
    float last_burst_time;

    void Start()
    {
        total_ammo = gun_SO.mag_size;
        current_ammo = gun_SO.mag_size;
    }

    public void Shoot()
    {
        if (current_ammo > 0)
        {
            current_ammo--;
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
}
