using UnityEngine;

[CreateAssetMenu(fileName = "Gun", menuName = "Guns/Gun")]
public class Gun_SO : ScriptableObject
{
    public Gun_Type type;
    public string name;
    public GameObject model_prefab;
    public Vector3 spawn_point;
    public Vector3 spawn_rotaion;    
}
