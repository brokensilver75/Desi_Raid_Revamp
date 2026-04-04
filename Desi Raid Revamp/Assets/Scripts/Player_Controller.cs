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
    }

    private void HandleGameStateChange(GameStates state)
    {
        switch(state)
        {
            case GameStates.LEVEL_PLAY:
                move_player_delegate = MovePlayer_Combat; //Enable player movement during gameplay
                aim_player_delegate = AimPLayer; //Enable player aiming during gameplay                
                break;

            case GameStates.HUB_PLAY:
                move_player_delegate = MovePlayer_Hub; //Enable player movement during hub gameplay
                //aim_player_delegate = AimPLayer; //Enable player aiming during hub gameplay
                aim_player_delegate = null; //Disable player aiming outside gameplay
                //transform.rotation = Quaternion.identity; // Reset player rotation when not outside gameplay
                break;

            default:
                move_player_delegate = MovePlayer_Default; //Diable player movement outside gameplay
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

    private void MovePlayer_Combat()
    {
        player_character_controller.Move(new Vector3(move_direction.x, 0, move_direction.y) * move_speed * Time.deltaTime);
        if (!player_character_controller.isGrounded)
        {
            player_character_controller.Move(Physics.gravity * Time.deltaTime); //Apply gravity when not grounded
        }
    }    

    private void MovePlayer_Hub()
    {
        // 1. Get the active camera's transform
        Transform cameraTransform = CameraManager.instance.GetActiveCamera().transform;

        // 2. Extract the forward and right vectors from the camera
        Vector3 cameraForward = cameraTransform.forward;
        Vector3 cameraRight = cameraTransform.right;

        // 3. Flatten the vectors on the Y axis to keep movement strictly on the ground
        cameraForward.y = 0f;
        cameraRight.y = 0f;

        // 4. Normalize to prevent the player from moving slower or faster based on camera tilt
        cameraForward.Normalize();
        cameraRight.Normalize();

        // 5. Calculate the final movement direction based on player input
        // move_direction.x is Left/Right (A/D)
        // move_direction.y is Forward/Backward (W/S)
        Vector3 direction = (cameraForward * move_direction.y + cameraRight * move_direction.x).normalized;

        // 6. Move the CharacterController
        player_character_controller.Move(direction * move_speed * Time.deltaTime);

        // Optional: Rotate the player model to face the direction they are moving
        if (direction != Vector3.zero)
        {
            // Slerp provides smooth rotation. Adjust the '10f' to change the rotation speed.
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(direction), Time.deltaTime * 10f);
        }

        if (!player_character_controller.isGrounded)
        {
            player_character_controller.Move(Physics.gravity * Time.deltaTime); //Apply gravity when not grounded
        }
    }

    private void MovePlayer_Default()
    {
        if (!player_character_controller.isGrounded)
        {
            player_character_controller.Move(Physics.gravity * Time.deltaTime); //Apply gravity when not grounded
        }
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

    public void AlternateGameState()
    {
        if (GameStateManager.GetCurrentGameState() == GameStates.LEVEL_PLAY)
        {
            GameStateManager.ChangeGameState(GameStates.HUB_PLAY);
        }
        else
        {
            GameStateManager.ChangeGameState(GameStates.LEVEL_PLAY);
        }
    }
    
}
