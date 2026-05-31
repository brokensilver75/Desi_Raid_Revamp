using System.Collections.Generic;
using UnityEngine;

public class Gun_Selector : MonoBehaviour
{
    [SerializeField] Gun[] gun_prefabs; //Temporary array to hold the player's guns, will be replaced with a more robust system later
    private Equipment_Slots current_slot = Equipment_Slots.NONE;
    private List<Gun> equipped_guns = new List<Gun>();

    public void Init()
    {
        //TODO: Take guns from player loadout and assign them to the gun slots
        current_slot = Equipment_Slots.SLOT_1;

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
                    equipped_guns[slot_index] = gun;
                }

                break;

            default:
                equipped_guns[slot_index] = gun;
                break;
        }

        //Equi_Gun(slot_index);
    }

    private void Equi_Gun(int slot_index)
    {
        for (int i = 0; i < equipped_guns.Count; i++)
        {
            if (i == slot_index)
            {
                equipped_guns[i].gameObject.SetActive(true);
            }

            else
            {
                equipped_guns[i].gameObject.SetActive(false);
            }
        }

        current_slot = (Equipment_Slots)slot_index;

    }

    public void Scroll_Next()
    {

    }
}
