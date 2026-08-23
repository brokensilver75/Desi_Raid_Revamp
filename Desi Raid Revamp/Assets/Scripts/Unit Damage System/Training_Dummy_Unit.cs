using TMPro;
using UnityEngine;

public class Training_Dummy_Unit : Unit
{
    [SerializeField] private TextMeshProUGUI damage_text;
    int damage_taken = 0;

    public override void Take_Damage(float damage_amount)
    {
        // For training dummy, we can just log the damage taken
        Debug.Log($"Training Dummy took {damage_amount} damage.");
        damage_taken += (int)damage_amount;

        if (damage_text != null)
        {
            damage_text.SetText(damage_taken.ToString()); 
        }
    }

}
