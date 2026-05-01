using UnityEngine;

[CreateAssetMenu(fileName = "Ammo_Config", menuName = "Guns/Ammo_Config")]
public class Ammo_Config : ScriptableObject
{
    public Gun_Type gun_type;

    public Modifiers ammo_modifier;

    public GameObject ammo_model_prefab;
}

public enum Modifiers
{
    regular,
    ricochet,
    armor_piercing
}
