using UnityEngine;

[CreateAssetMenu(fileName = "Impact_SO", menuName = "DESI RAID/Impact_SO")]
public class Impact_Config_SO : ScriptableObject
{
    public GameObject[] vfx_array; // Array of VFX prefabs for different surface types (index corresponds to Impact_Surface enum)

    public GameObject Get_VFX_For_Surface(Impact_Surface surface)
    {
        if ((int)surface < 0 || (int)surface >= vfx_array.Length)
        {
            Debug.LogWarning("Surface type index is out of bounds of the VFX array.");
            return null; // Return null if the surface type index is invalid
        };

        return vfx_array[(int)surface]; // Return the VFX prefab corresponding to the surface type
    }
}
