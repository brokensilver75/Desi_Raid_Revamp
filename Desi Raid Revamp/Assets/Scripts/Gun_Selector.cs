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

    //[Header("List of Transforms for Gun Positioning")]
    //[SerializeField] List<Gun_Transfom_Data> gun_transforms_list = new List<Gun_Transfom_Data>();

    Dictionary<Gun_Type, Gun_Data> guns_dictionary = new Dictionary<Gun_Type, Gun_Data>();

    [Header ("GUNS INVENTORY")]
    [Header ("Main Gun Slots")]
    [SerializeField] Gun_Slot[] gun_slots = new Gun_Slot[2];

    GameObject gun1, gun2;    

    void Start()
    {
        //Initialize_Gun_Selector();
    }

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

    public void Select_Gun(int gun_index)
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

        switch (gun_index)
        {
            case 0:
                if (!gun_slots[0].is_equipped)
                {
                    gun1.SetActive(true);
                    gun2.SetActive(false);

                    gun_slots[0].is_equipped = true;
                    gun_slots[1].is_equipped = false;

                    right_hand_ik_target.position = gun_slots[0].Get_Gun().Get_Right_Hand_Transform().position;
                    left_hand_ik_target.position = gun_slots[0].Get_Gun().Get_Left_Hand_Transform().position;

                    //gun_slots[0].Get_Gun().transform.SetParent(guns_dictionary[gun_slots[0].Get_Gun().Get_Gun_Type()].gun_positioning_transform);

                }
                break;
            case 1:
                if (!gun_slots[1].is_equipped)
                {
                    gun1.SetActive(false);
                    gun2.SetActive(true);

                    gun_slots[0].is_equipped = false;
                    gun_slots[1].is_equipped = true;

                    right_hand_ik_target.position = gun_slots[1].Get_Gun().Get_Right_Hand_Transform().position;
                    left_hand_ik_target.position = gun_slots[1].Get_Gun().Get_Left_Hand_Transform().position;

                    //gun_slots[1].Get_Gun().transform.SetParent(guns_dictionary[gun_slots[0].Get_Gun().Get_Gun_Type()].gun_positioning_transform);
                }
                break;
            default:
                Destroy(gun1);
                Destroy(gun2);
                //gun1 = null;
                //gun2 = null;
                gun_slots[0].is_equipped = false;
                gun_slots[1].is_equipped = false;
                Debug.Log("[Gun_Selector]Invalid gun index: " + gun_index);
                break;
        }

    }
}
