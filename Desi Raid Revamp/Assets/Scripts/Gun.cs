using UnityEngine;

public class Gun : MonoBehaviour
{
    [SerializeField] Gun_SO gun_SO;

    [SerializeField] Transform right_hand_transform;

    [SerializeField] Transform left_hand_transform;

    [SerializeField] int current_ammo;

    #region Get Methods
    public Transform Get_Right_Hand_Transform()
    {
        return right_hand_transform;
    }

    public Transform Get_Left_Hand_Transform()
    {
        return left_hand_transform;
    }

    public Gun_SO Get_Gun_SO()
    {
        return gun_SO;
    }

    public Gun_Type Get_Gun_Type()
    {
        return gun_SO.type;
    }

    public int Get_Mag_Size()
    {
        return gun_SO.mag_size;
    }

    public int Get_Current_Ammo()
    {
        return current_ammo;
    } 
    #endregion

    public void Set_Current_Ammo(int ammo)
    {
        current_ammo = ammo;
    }

    public void Fire()
    {
        gun_SO.Shoot();
    }

    public void Reset_Last_Shoot_Time()
    {
        gun_SO.Reset_Shoot_Time();
    }



}
