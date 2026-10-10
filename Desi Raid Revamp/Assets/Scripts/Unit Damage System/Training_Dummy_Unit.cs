using TMPro;
using UnityEngine;
using UnityEngine.VFX;

public class Training_Dummy_Unit : Unit
{
    [SerializeField] private TextMeshProUGUI damage_text;

    [Header ("BLOOD SYSYTEM")]
    [SerializeField] private VisualEffect blood_effect;
    [SerializeField] private BloodSplotchesSystem blood_splotches_system;
    [SerializeField] private CapsuleCollider unit_collider;
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

    public override void Show_Blood(Vector3 impact_point, Vector3 bullet_direction)
    {
        base.Show_Blood(impact_point, bullet_direction);

        if (unit_collider == null)
        {
            if (TryGetComponent(out unit_collider))
            {
                Debug.Log("[Training Dummy] CapsuleCollider component found and assigned.");
            }
            else
            {
                Debug.LogWarning("[Training Dummy] CapsuleCollider component not found on the GameObject.");
            }
        }

        if (blood_effect != null)
        {
            Vector3 blood_splash_point = impact_point + (unit_collider.radius * bullet_direction);
            blood_effect.gameObject.SetActive(true);
            blood_effect.transform.position = blood_splash_point;
            blood_effect.transform.forward = bullet_direction;
            
            blood_splotches_system.transform.position = blood_splash_point;
            blood_splotches_system.transform.forward = bullet_direction;
            
            blood_splotches_system.GetParticleSystem().Play();
            blood_effect.Play();
        }

        else
        {
            Debug.LogWarning("[Training Dummy] Blood effect is not assigned in the inspector.");
        }
    }
}
