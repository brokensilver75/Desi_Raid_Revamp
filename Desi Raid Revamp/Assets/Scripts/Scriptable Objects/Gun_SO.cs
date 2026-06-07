using UnityEngine;

[CreateAssetMenu(fileName = "Gun_SO", menuName = "DESI RAID/GUNS/Guns")]
public class Gun_SO : ScriptableObject
{
    public string gun_name;
    public GameObject gun_model_prefab;
    public int mag_size;
    public Fire_Mode fire_mode;
    public float fire_rate;
    public float burst_rate;
    public int burst_amt;

    public Bullet_SO compatible_ammo;
}
