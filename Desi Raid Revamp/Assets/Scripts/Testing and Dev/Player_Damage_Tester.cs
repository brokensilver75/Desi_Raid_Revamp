using UnityEngine;

public class Player_Damage_Tester : MonoBehaviour
{
    [SerializeField] private Player_Unit player_unit;
    [SerializeField] private float test_damage_amt = 10f;

    [ContextMenu ("Test Player Damage")]
    public void Test_Player_Damage()
    {
        if (player_unit != null)
        {
            player_unit.Take_Damage(test_damage_amt);
        }

        else
        {
            Debug.LogWarning("[Player_Damage_Tester] Player unit reference is not assigned.");
        }
    }
}
