using UnityEngine;

public class Ammo_Box : MonoBehaviour
{
    [Header("Ammo Box Setttings")]
    [SerializeField] Ammo_Box_SO ammo_box_so;    

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<Player_External_Ammo_Handler>(out Player_External_Ammo_Handler player_external_ammo_handler))
        {
            if(player_external_ammo_handler.Add_Ammo(ammo_box_so))
            {
                gameObject.SetActive(false);
            }
        }
    }
}
