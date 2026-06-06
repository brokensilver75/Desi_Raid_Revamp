using System.Collections.Generic;
using UnityEngine;

public class Gun_Selector : MonoBehaviour
{
    [SerializeField] Gun[] gun_prefabs; //Temporary array to hold the player's guns, will be replaced with a more robust system later
    [SerializeField] private Equipment_Slots current_slot = Equipment_Slots.NONE;
    [SerializeField] private List<Gun> equipped_guns = new List<Gun>();

    public void Init()
    {
        //TODO: Take guns from player loadout and assign them to the gun slots
        foreach (Gun gun in gun_prefabs)
        {
            equipped_guns.Add(gun);
        }

        if (gun_prefabs.Length != 0)
        {
            for (int i = 0; i <= 1; i++)
            {
                Assign_Slots(gun_prefabs[i], (Equipment_Slots)i);
            }
        }

        current_slot = Equipment_Slots.SLOT_1;

        Equip_Gun((int)current_slot);
    }

    public void Assign_Slots(Gun gun, Equipment_Slots gun_slot)
    {
        int slot_index = (int)gun_slot;

        switch (gun_slot)
        {
            case Equipment_Slots.NONE:
                break;

            case Equipment_Slots.SECONDARY_SLOT:
                if (equipped_guns[slot_index] != null)
                {
                    Debug.Log("Secondary slot FILLED.");
                }

                else
                {
                    equipped_guns.Add(gun);
                    //equipped_guns[slot_index] = gun;
                }

                break;

            default:
                equipped_guns[slot_index] = gun;
                break;
        }

        //Equi_Gun(slot_index);
    }

    private void Equip_Gun(int slot_index)
    {
        for (int i = 0; i < equipped_guns.Count; i++)
        {
            GameObject gun_object = equipped_guns[i].gameObject;

            if (i == slot_index)
            {
                gun_object.SetActive(true);
            }

            else
            {
                gun_object.SetActive(false);
            }

            gun_object.transform.SetParent(transform);
        }        

        current_slot = (Equipment_Slots)slot_index;

    }

    public void Scroll_Next()
    {
        //Debug.Log("[Gun_Selector] Scrolling to next gun...");

        Equip_Gun((int)(current_slot + 1) % equipped_guns.Count);
    }

    public void Scroll_Previous()
    {
        //Debug.Log("[Gun_Selector] Scrolling to previous gun...");

        int new_index = (int)(current_slot - 1) % equipped_guns.Count;

        new_index = new_index < 0 ? new_index + equipped_guns.Count : new_index;

        Equip_Gun(new_index);
    }

    public Gun Get_Current_Gun()
    {
        return equipped_guns[(int)current_slot];
    }
}
