using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations.Rigging;

public class Gun_Selector : MonoBehaviour
{
    [Serializable]
    class Gun_Data
    {
        public GameObject gun;
        public Transform gun_positioning_transform;

        public Gun Get_Gun()
        {
            return gun.GetComponent<Gun>();
        }
    }

    [Serializable]
    class Gun_Transfom_Data
    {
        public Gun_Type gun_type;
        public Transform gun_transform;
    }

    [Serializable]
    class Gun_Slot
    {
        public GameObject gun;
        public bool is_equipped;

        public Gun Get_Gun()
        {
            return gun.GetComponent<Gun>();
        }
    }    

    [Header("Right Hand")]
    [SerializeField] TwoBoneIKConstraint right_hand_ik_constraint;
    [SerializeField] Transform right_hand_ik_target;
    [Space(20)]

    [Header("Left Hand")]
    [SerializeField] TwoBoneIKConstraint left_hand_ik_constraint;
    [SerializeField] Transform left_hand_ik_target;
    [Space(20)]

    [Header("Guns List")]
    [SerializeField] List<Gun_Data> guns_prefabs_list = new List<Gun_Data>();
    [Space(20)]

    Dictionary<Gun_Type, Gun_Data> guns_dictionary = new Dictionary<Gun_Type, Gun_Data>();

    [Header ("GUNS INVENTORY")]
    [Header ("Main Gun Slots")]
    [SerializeField] Gun_Slot[] gun_slots = new Gun_Slot[2];

    GameObject gun1, gun2;

    private Gun equipped_gun;

    public void Initialize_Gun_Selector()
    {
        foreach (Gun_Data item in guns_prefabs_list)
        {
            if (!guns_dictionary.ContainsKey(item.Get_Gun().Get_Gun_Type()))
            {
                guns_dictionary.Add(item.Get_Gun().Get_Gun_Type(), item);
            }
        }

        gun1 = Instantiate(gun_slots[0].gun);
        gun1.transform.position = guns_dictionary[gun_slots[0].Get_Gun().Get_Gun_Type()].gun_positioning_transform.position;
        gun1.transform.rotation = guns_dictionary[gun_slots[0].Get_Gun().Get_Gun_Type()].gun_positioning_transform.rotation;
        gun1.transform.localScale = guns_dictionary[gun_slots[0].Get_Gun().Get_Gun_Type()].gun_positioning_transform.localScale;
        gun1.transform.SetParent(guns_dictionary[gun_slots[0].Get_Gun().Get_Gun_Type()].gun_positioning_transform);

        gun2 = Instantiate(gun_slots[1].gun);
        gun2.transform.position = guns_dictionary[gun_slots[1].Get_Gun().Get_Gun_Type()].gun_positioning_transform.position;
        gun2.transform.rotation = guns_dictionary[gun_slots[1].Get_Gun().Get_Gun_Type()].gun_positioning_transform.rotation;
        gun2.transform.localScale = guns_dictionary[gun_slots[1].Get_Gun().Get_Gun_Type()].gun_positioning_transform.localScale;
        gun2.transform.SetParent(guns_dictionary[gun_slots[1].Get_Gun().Get_Gun_Type()].gun_positioning_transform);


    }

    public void Select_Gun(Equipment_Slots slot)
    {
        if (gun1 == null)
        {
            Debug.Log($"[Gun_Selector] Instantiating gun 1");
            gun1 = Instantiate(gun_slots[0].gun);
            gun1.transform.position = guns_dictionary[gun_slots[0].Get_Gun().Get_Gun_Type()].gun_positioning_transform.position;
            gun1.transform.rotation = guns_dictionary[gun_slots[0].Get_Gun().Get_Gun_Type()].gun_positioning_transform.rotation;
            gun1.transform.localScale = guns_dictionary[gun_slots[0].Get_Gun().Get_Gun_Type()].gun_positioning_transform.localScale;
            gun1.transform.SetParent(guns_dictionary[gun_slots[0].Get_Gun().Get_Gun_Type()].gun_positioning_transform);
        }

        if (gun2 == null)
        {
            Debug.Log($"[Gun_Selector] Instantiating gun 2");
            gun2 = Instantiate(gun_slots[1].gun);
            gun2.transform.position = guns_dictionary[gun_slots[1].Get_Gun().Get_Gun_Type()].gun_positioning_transform.position;
            gun2.transform.rotation = guns_dictionary[gun_slots[1].Get_Gun().Get_Gun_Type()].gun_positioning_transform.rotation;
            gun2.transform.localScale = guns_dictionary[gun_slots[1].Get_Gun().Get_Gun_Type()].gun_positioning_transform.localScale;
            gun2.transform.SetParent(guns_dictionary[gun_slots[1].Get_Gun().Get_Gun_Type()].gun_positioning_transform);
        }

        switch (slot)
        {
            case Equipment_Slots.Slot_1:
                if (!gun_slots[0].is_equipped)
                {
                    gun1.SetActive(true);
                    gun2.SetActive(false);

                    gun_slots[0].is_equipped = true;
                    gun_slots[1].is_equipped = false;

                    equipped_gun = gun_slots[0].Get_Gun();

                    //right_hand_ik_target.localPosition = gun_slots[0].Get_Gun().Get_Right_Hand_Transform().position;
                    //left_hand_ik_target.localPosition = gun_slots[0].Get_Gun().Get_Left_Hand_Transform().position;

                }
                break;

            case Equipment_Slots.Slot_2:
                if (!gun_slots[1].is_equipped)
                {
                    gun1.SetActive(false);
                    gun2.SetActive(true);

                    gun_slots[0].is_equipped = false;
                    gun_slots[1].is_equipped = true;

                    equipped_gun = gun_slots[1].Get_Gun();

                    //right_hand_ik_target.localPosition = gun_slots[1].Get_Gun().Get_Right_Hand_Transform().position;
                    //left_hand_ik_target.localPosition = gun_slots[1].Get_Gun().Get_Left_Hand_Transform().position;
                }
                break;

            case Equipment_Slots.None:
                
                Destroy(gun1);
                Destroy(gun2);

                equipped_gun = null;

                gun_slots[0].is_equipped = false;
                gun_slots[1].is_equipped = false;
                
                Debug.Log("[Gun_Selector] No gun equipped");
                
                break;
        }

        if (equipped_gun != null)
        {
            equipped_gun.Reset_Last_Shoot_Time();
        }

    }

    public Gun Get_Equipped_Gun()
    {
        return equipped_gun;
    }
}
