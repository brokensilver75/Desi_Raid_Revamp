using UnityEngine;

[CreateAssetMenu(fileName = "Gun_Config", menuName = "Guns/Gun_Config")]
public class Shoot_Config_SO : ScriptableObject
{
    public Gun_Fire_Type fire_type;

    public Ammo_Config ammo_config;

    public float fire_rate;

    public Vector3 spread = new Vector3(0.1f, 0.1f, 0.1f);
}
