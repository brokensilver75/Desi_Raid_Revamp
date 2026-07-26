using System.Runtime.CompilerServices;
using UnityEngine;

public class Pickup_Animator : MonoBehaviour
{
    [Header("Rotation Settings")]
    [SerializeField] private float rotation_speed = 50f;
    [SerializeField] private Vector3 rotation_axis = Vector3.up;
    [Space(20)]

    [Header("Floatation Settings")]
    [SerializeField] private float float_speed = 2f;
    [SerializeField] private float float_height = 0.5f;

    private Vector3 start_position;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        start_position = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        Rotate_Object();
        Float_Object();
    }

    private void Rotate_Object()
    {
        transform.Rotate(rotation_axis, rotation_speed * Time.deltaTime);
    }

    private void Float_Object()
    {
        float y_pos = start_position.y + Mathf.Sin(Time.time * float_speed) * float_height;

        transform.position = new Vector3(start_position.x, y_pos, start_position.z);
    }
}
