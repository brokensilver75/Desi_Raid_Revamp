using UnityEngine;

[CreateAssetMenu(fileName = "Shoot_Config", menuName = "Guns/Shoot Configuration")]
public class Shoot_Config_SO : ScriptableObject
{
    public LayerMask hit_mask;
    public Vector3 spread = new Vector3(0.1f, 0.1f, 0.1f);
    public float fire_rate = 0.25f;
}
