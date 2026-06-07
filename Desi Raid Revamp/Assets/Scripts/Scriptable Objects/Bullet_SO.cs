using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Bullet_SO", menuName = "DESI RAID/GUNS/Bullets")]
public class Bullet_SO: ScriptableObject
{
    [Header("Identity & Visuals")]
    public string bullet_name;
    public GameObject bullet_prefab; // The physical projectile
    //public GameObject impact_;    // The spark/blood particle spawned on hit
    public Impact_Config_SO impact_config; // Reference to our impact config SO for easy VFX lookup

    [Header("Flight Stats")]
    public float bullet_speed = 50f;
    public float bullet_max_life_time = 3f;
    public float bullet_base_damage = 10f;

    [Header("Special Properties")]
    public int max_bounces = 0;      // For Ricochet
    public int max_penetrations = 0; // For Armor Piercing
}