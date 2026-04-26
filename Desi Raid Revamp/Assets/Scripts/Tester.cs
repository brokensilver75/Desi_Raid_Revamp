using UnityEngine;

public class Tester : MonoBehaviour
{
    [SerializeField] Gun_SO gun;
    [SerializeField] GameObject player;
    [SerializeField] Gun_Selector gun_holder;

    [ContextMenu("Spawn Gun")]
    public void SpawnGun()
    {
        if (gun != null)
        {
            GameObject gun_instance = Instantiate(gun.model_prefab);
            Transform parent_transform = gun_holder.Get_Gun_Transform(gun.type);
            gun_instance.transform.position = parent_transform.position;
            gun_instance.transform.rotation = parent_transform.rotation;
            gun_instance.transform.localScale = parent_transform.localScale;
            gun_instance.transform.SetParent(parent_transform);
        }
    }
}
