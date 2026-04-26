using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations.Rigging;

public class Gun_Selector : MonoBehaviour
{
    [Header("Right Hand")]
    [SerializeField] TwoBoneIKConstraint right_hand_ik_constraint;
    [SerializeField] Transform right_hand_ik_target;
    [Space(20)]

    [Header("Left Hand")]
    [SerializeField] TwoBoneIKConstraint left_hand_ik_constraint;
    [SerializeField] Transform left_hand_ik_target;

    [Serializable]
    public class Gun_Transform_Data
    {
        public Gun_Type gun_type;
        public Transform gun_transform;
        public Transform right_hand_transform;
        public Transform left_hand_transform;
    }

    [SerializeField] List<Gun_Transform_Data> gun_transforms_list = new List<Gun_Transform_Data>();
    //public Dictionary<Gun_Type, Transform> gun_positions_dictionary = new Dictionary<Gun_Type, Transform>();
    public Dictionary<Gun_Type, Gun_Transform_Data> gun_transforms_dictionary = new Dictionary<Gun_Type, Gun_Transform_Data>();

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        GameStateManager.On_Game_State_Changed += Handle_Game_State_Changed;
        foreach (Gun_Transform_Data item in gun_transforms_list)
        {
            gun_transforms_dictionary.Add(item.gun_type, item);
        }
    }

    [ContextMenu ("Select Gun")]
    public void Select_Gun()
    {
        Select_Gun(Gun_Type.Assault);
    }

    void Select_Gun(Gun_Type gun_type)
    {
        Transform selected_gun_transform = Get_Gun_Transform(gun_type);

        if (selected_gun_transform != null)
        {
            right_hand_ik_target.position = Get_Gun_Transform_Data(gun_type).right_hand_transform.position;
            right_hand_ik_target.rotation = Get_Gun_Transform_Data(gun_type).right_hand_transform.rotation;
            left_hand_ik_target.position = Get_Gun_Transform_Data(gun_type).left_hand_transform.position;
            left_hand_ik_target.rotation = Get_Gun_Transform_Data(gun_type).left_hand_transform.rotation;
        }
    }

    private void Handle_Game_State_Changed(GameStates states)
    {
        switch (states)
        {
            case GameStates.LEVEL_PLAY:
                right_hand_ik_constraint.weight = 1f;
                left_hand_ik_constraint.weight = 1f;
                break;
            default:
                right_hand_ik_constraint.weight = 0f;
                left_hand_ik_constraint.weight = 0f;
                break;
        }
    }

    public Gun_Transform_Data Get_Gun_Transform_Data(Gun_Type gun_Type)
    {
        return (gun_transforms_dictionary != null && gun_transforms_dictionary.ContainsKey(gun_Type)) ? gun_transforms_dictionary[gun_Type] : null;
    }

    public Transform Get_Gun_Transform(Gun_Type gun_type)
    {
        return (gun_transforms_dictionary != null && gun_transforms_dictionary.ContainsKey(gun_type)) ? gun_transforms_dictionary[gun_type].gun_transform : null;
    }
}
