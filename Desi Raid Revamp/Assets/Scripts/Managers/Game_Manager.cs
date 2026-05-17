using UnityEngine;

public class Game_Manager : MonoBehaviour
{
    private static bool is_initialized;

    public static Game_Manager instance;

    [SerializeField] GameStates level_game_state;

    public static bool game_manager_initialized => is_initialized;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
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

    // Update is called once per frame
    void Update()
    {

    }
}
