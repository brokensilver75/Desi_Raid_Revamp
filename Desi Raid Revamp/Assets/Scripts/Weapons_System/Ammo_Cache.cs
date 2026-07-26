using UnityEngine;

public class Ammo_Cache : MonoBehaviour
{

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<Player_External_Ammo_Handler>(out Player_External_Ammo_Handler player_external_ammo_handler))
        {
            player_external_ammo_handler.Add_All_Ammo();
        }
        
    }
}
