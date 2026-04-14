using UnityEngine;

[CreateAssetMenu(fileName = "Trail_Config", menuName = "guns/Trail Configuration")]
public class Trail_Config_SO : ScriptableObject
{
    public Material material;
    public AnimationCurve width_curve;
    public float duration = 0.5f;
    public float min_vertex_distance = 0.1f;

}
