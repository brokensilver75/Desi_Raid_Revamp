using System.Collections.Generic;
using UnityEngine;

public class Player_External_Ammo_Handler : MonoBehaviour
{
    private Gun_Selector player_gun_selector;
    private List<Gun> equipped_guns;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        player_gun_selector = GetComponent<Gun_Selector>();
        equipped_guns = new List<Gun>();
    }

    // Refill Ammo from Ammo Box
    public bool Add_Ammo(Ammo_Box_SO ammo_box_so)
    {
        bool gun_refilled = false;

        if (player_gun_selector != null)
        {
            equipped_guns = player_gun_selector.Get_Equipped_Guns();
            
            foreach (Gun gun in equipped_guns)
            {
                if (gun.Get_Gun_Config().gun_ammo_type == ammo_box_so.ammo_type)
                {
                    if (gun.Get_Current_Ammo() < gun.Get_Total_Ammo())
                    {
                        gun.Refill(ammo_box_so.ammo_amt);
                        gun_refilled = true; 
                    }
                }
            }
        }

        return gun_refilled;
    }

    // Refill Ammo from picked up duplicate gun
    public bool Add_Ammo(Ammo_Type ammo_type, int ammo_amt)
    {
        bool gun_refilled = false;

        if (player_gun_selector != null)
        {
            equipped_guns = player_gun_selector.Get_Equipped_Guns();

            foreach (Gun gun in equipped_guns)
            {
                if (gun.Get_Gun_Config().gun_ammo_type == ammo_type)
                {
                    gun.Refill(ammo_amt);
                    gun_refilled = true;
                }
            }
        }

        return gun_refilled;
    }
    
}
