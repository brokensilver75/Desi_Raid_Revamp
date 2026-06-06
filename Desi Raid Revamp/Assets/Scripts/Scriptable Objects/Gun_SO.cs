using UnityEngine;

[CreateAssetMenu(fileName = "Gun_SO", menuName = "GUNS/Gun_SO")]
public class Gun_SO : ScriptableObject
{
    public string gun_name;
    public GameObject gun_model_prefab;
    public int mag_size;
    public Fire_Mode fire_mode;
    public float fire_rate;
}
