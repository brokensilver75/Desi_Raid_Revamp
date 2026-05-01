using UnityEngine;

[CreateAssetMenu(fileName = "Gun_Config", menuName = "Guns/Gun_Config")]
public class Gun_Config_SO : ScriptableObject
{
    public Gun_Fire_Type fire_type;

    public Ammo_Config ammo_config;

    public float fire_rate;    
}
