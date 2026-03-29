using UnityEngine;
using UnityEngine.InputSystem;

public class Player_Controller : MonoBehaviour
{
    [SerializeField] float move_speed = 5f; // Speed at which the player moves
    [SerializeField] Vector2 move_direction; // Direction in which the player is moving
    [SerializeField] CharacterController player_character_controller;

    
    delegate void Move_Player_Delegate(); // Delegate to define the method sigature for moving the player
    Move_Player_Delegate move_player_delegate;

    private void Start()
    {
        GameStateManager.On_Game_State_Changed += HandleGameStateChange;
    }

    private void HandleGameStateChange(GameStates state)
    {
        switch(state)
        {
            case GameStates.GAME_PLAY:
                move_player_delegate = MovePlayer; //Enable player movement during gameplay
                break;
            default:
                move_player_delegate = null; //Diable player movement outside gameplay
                move_direction = Vector2.zero; // Reset move direction when not outside gameplay
                break;
        }
    }

    public void OnMove(InputAction.CallbackContext callbackContext)
    {
        move_direction = callbackContext.ReadValue<Vector2>();
    }

    private void Update()
    {
        move_player_delegate?.Invoke(); //Invoke the delegate to move the player if it's not null
    }

    private void MovePlayer()
    {
        player_character_controller.Move(new Vector3(move_direction.x, 0, move_direction.y) * move_speed * Time.deltaTime);
    }

    private void OnDestroy()
    {
        GameStateManager.On_Game_State_Changed -= HandleGameStateChange;
    }

    [ContextMenu ("Start Game")]
    public void StartGame()
    {
        GameStateManager.ChangeGameState(GameStates.GAME_PLAY);
    }
}
