using UnityEngine;

public class Game_Manager : MonoBehaviour
{
    private static bool is_initialized;

    public static Game_Manager instance;

    [SerializeField] GameState level_game_state;

    public static bool game_manager_initialized => is_initialized;

    void Start()
    {
        if (instance == null)
        {
            instance = this;
        }

        else
        {
            Destroy(gameObject);
        }

        GameStateManager.ChangeGameState(level_game_state);

        is_initialized = true;
    }
}
