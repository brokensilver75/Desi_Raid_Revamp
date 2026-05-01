using UnityEngine;

[CreateAssetMenu(fileName = "Gun", menuName = "Guns/Gun")]
public class Gun_SO : ScriptableObject
{
    public Gun_Type type;

    public string gun_name;

    public Gun_Config_SO gun_config;

    public int mag_size;

    public int current_ammo;

    //public Vector3 spawn_point;
    //public Vector3 spawn_rotation;
}
