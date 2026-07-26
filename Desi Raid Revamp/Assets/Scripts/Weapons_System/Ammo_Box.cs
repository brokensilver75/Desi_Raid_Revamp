using UnityEngine;

public class Ammo_Box : MonoBehaviour
{
    [Header("Ammo Box Setttings")]
    [SerializeField] Ammo_Box_SO ammo_box_so;
    [Space(20)]

    [Header("Box Rotation")]
    [SerializeField] float rotation_speed = 50f;

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(Vector3.up, rotation_speed * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<Player_External_Ammo_Handler>(out Player_External_Ammo_Handler player_external_ammo_handler))
        {
            if(player_external_ammo_handler.AddAmmo(ammo_box_so))
            {
                gameObject.SetActive(false);
            }
        }
    }
}
