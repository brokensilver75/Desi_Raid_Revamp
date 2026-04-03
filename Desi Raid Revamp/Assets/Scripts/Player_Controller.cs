using UnityEngine;
using UnityEngine.InputSystem;

public class Player_Controller : MonoBehaviour
{
    [SerializeField] float move_speed = 5f; // Speed at which the player moves
    [SerializeField] Vector2 move_direction; // Direction in which the player is moving
    [SerializeField] CharacterController player_character_controller;
    [SerializeField] LayerMask aim_layer_mask; // Layer mask for the aim layer

    
    delegate void Move_Player_Delegate(); // Delegate to define the method sigature for moving the player
    Move_Player_Delegate move_player_delegate;
    Move_Player_Delegate aim_player_delegate;

    Vector3 mouse_position; // Variavble to store the mouse position in world space.

    private void FixedUpdate()
    {
        mouse_position = Mouse_Input.GetMousePosition(Camera.main, aim_layer_mask);        
    }

    private void Start()
    {
        GameStateManager.On_Game_State_Changed += HandleGameStateChange;
        // Set GameState to GAME_PLAY for testing.
        StartGame();
    }

    private void HandleGameStateChange(GameStates state)
    {
        switch(state)
        {
            case GameStates.GAME_PLAY:
                move_player_delegate = MovePlayer; //Enable player movement during gameplay
                aim_player_delegate = AimPLayer; //Enable player aiming during gameplay
                break;
            default:
                move_player_delegate = null; //Diable player movement outside gameplay
                move_direction = Vector2.zero; // Reset move direction when not outside gameplay
                aim_player_delegate = null; //Disable player aiming outside gameplay
                transform.rotation = Quaternion.identity; // Reset player rotation when not outside gameplay
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
        aim_player_delegate?.Invoke(); //Invoke the delegate to aim the player if it's not null
    }

    private void MovePlayer()
    {
        player_character_controller.Move(new Vector3(move_direction.x, 0, move_direction.y) * move_speed * Time.deltaTime);
    }

    private void AimPLayer()
    {
        mouse_position.y = transform.position.y; // Set the y-coordinate of the mouse position to match the player's z-coordinate
        Vector3 player_direction = (mouse_position - transform.position).normalized; // Calculate the direction from the player to the mouse position and normalize it
        transform.rotation = Quaternion.LookRotation(player_direction); // Rotate the player to face the mouse position
    }

    private void OnDestroy()
    {
        GameStateManager.On_Game_State_Changed -= HandleGameStateChange;
    }

    
    public void StartGame()
    {
        GameStateManager.ChangeGameState(GameStates.GAME_PLAY);
    }

    
}
