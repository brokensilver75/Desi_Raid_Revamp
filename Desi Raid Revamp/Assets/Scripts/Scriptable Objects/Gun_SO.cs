using UnityEngine;

[CreateAssetMenu(fileName = "Gun", menuName = "Guns/Gun")]
public class Gun_SO : ScriptableObject
{
    public Gun_Type type;
    public string name;
    public GameObject model_prefab;
    public Vector3 spawn_point;
    public Vector3 spawn_rotaion;

    public Shoot_Config_SO shoot_config;
    public Trail_Config_SO trail_config;

    MonoBehaviour active_monobehaviour;
    GameObject model;
    float last_shoot_time;

    #region Change to VFX

    #endregion
}
