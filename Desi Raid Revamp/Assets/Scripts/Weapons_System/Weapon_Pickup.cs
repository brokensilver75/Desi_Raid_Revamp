using UnityEngine;

public class Weapon_Pickup : MonoBehaviour
{
    [SerializeField] GameObject gun_prefab;

    private void Start()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (other.TryGetComponent(out Gun_Selector gun_Selector))
            {
                if (gun_prefab.TryGetComponent(out Gun gun_Component))
                {
                    gun_prefab.transform.SetParent(null);
                    gun_Selector.Assign_Slots(gun_Component, Equipment_Slots.SECONDARY_SLOT);
                    gameObject.SetActive(false);
                }
            } 
        }
    }
}
