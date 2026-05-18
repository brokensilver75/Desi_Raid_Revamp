using UnityEngine;

[CreateAssetMenu(fileName = "Trail_Config_SO", menuName = "Guns/Trail_Config")]
public class Trail_Config_SO : ScriptableObject
{
    public Material material;
    public AnimationCurve width_curve;
    public float duration = 0.5f;
    public float min_vertex_distance = 0.1f;
    public Gradient gradient_color;

    public float max_range = 100f;
    public float simulation_speed = 100f;
}
