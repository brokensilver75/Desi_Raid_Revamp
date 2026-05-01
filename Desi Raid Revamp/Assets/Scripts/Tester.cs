using UnityEngine;

public class Tester : MonoBehaviour
{
    [SerializeField] Gun_SO gun;
    [SerializeField] GameObject player;
    [SerializeField] Gun_Selector gun_holder;
    private GameObject gun_instance;

    private void Start()
    {
        GameStateManager.On_Game_State_Changed += Handle_State_Changed;
    }

    private void Handle_State_Changed(GameStates game_state)
    {
        switch (game_state)
        {
            case GameStates.LEVEL_PLAY:
                //SpawnGun();
                break;
            default:
                if (gun_instance != null)
                {
                    Destroy(gun_instance);
                }
                break;
        }
    }

   /* [ContextMenu("Spawn Gun")]
    public void SpawnGun()
    {
        if (gun != null)
        {
            gun_instance = Instantiate(gun.model_prefab);
            Transform parent_transform = gun_holder.Get_Gun_Transform(gun.type);
            gun_instance.transform.position = parent_transform.position;
            gun_instance.transform.rotation = parent_transform.rotation;
            gun_instance.transform.localScale = parent_transform.localScale;
            gun_instance.transform.SetParent(parent_transform);
        }
    }*/
}
