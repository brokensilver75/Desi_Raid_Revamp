using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player_Controller_Combat : MonoBehaviour
{
    const float COMBAT_MOVE_SPEED = 5f;
    const float HUB_MOVE_SPEED = 2.5f;

    const float SCROLL_COOLDOWN = 0.1f; // Cooldown time in seconds between scroll inputs
    float last_scroll_time = 0f; // Timestamp of the last scroll input

    [Header("Player Components")]
    [SerializeField] private Player_Animation_Controller player_animation_controller;
    [SerializeField] private Gun_Selector player_gun_selector;
    [Space(20)]

    [SerializeField] private float move_speed = 5f; // Speed at which the player moves
    [SerializeField] private Vector2 move_direction; // Direction in which the player is moving
    [SerializeField] private CharacterController player_character_controller;
    [SerializeField] private LayerMask aim_layer_mask; // Layer mask for the aim layer


    delegate void Player_Delegate(); // Delegate to define the method sigature for moving the player
    Player_Delegate move_delegate;
    Player_Delegate aim_delegate;
    Player_Delegate shoot_delegate;

    Vector3 mouse_position; // Variable to store the mouse position in world space.
    private Gun equipped_gun;
    private float fire_rate;

    private void FixedUpdate()
    {
        mouse_position = Mouse_Input.GetMousePosition(Camera.main);//, aim_layer_mask);        
    }

    //Wait for Game Manager to initialize before start
    private IEnumerator Start()
    {
        yield return new WaitUntil(() => Game_Manager.game_manager_initialized);

        GameStateManager.On_Game_State_Changed += Handle_Game_State_Change;

        if (TryGetComponent(out player_character_controller))
        {
            player_gun_selector.Init(); // Initialize the player's gun selector
            equipped_gun = player_gun_selector.Get_Current_Gun(); // Set the currently equipped gun
        }

        else
        {
            Debug.LogError("Player Controller Combat is missing a CharacterController component.");
        }

        Handle_Game_State_Change(GameStateManager.GetCurrentGameState());
    }

    private void Handle_Game_State_Change(GameState state)
    {
        switch (state)
        {
            case GameState.LEVEL_PLAY:
                move_delegate = MovePlayer_Combat; //Enable player movement during gameplay
                aim_delegate = AimPLayer; //Enable player aiming during gameplay
                move_speed = COMBAT_MOVE_SPEED; // Change Player Movement Speed
                break;

            case GameState.HUB_PLAY:
                move_delegate = MovePlayer_Hub; //Enable player movement during hub gameplay
                aim_delegate = null; //Disable player aiming outside gameplay
                shoot_delegate = null; //Disable player shooting outside gameplay
                move_speed = HUB_MOVE_SPEED; // Change Player Movement Speed
                break;

            default:
                move_delegate = MovePlayer_Default; //Diable player movement outside gameplay
                move_direction = Vector2.zero; // Reset move direction when not outside gameplay
                aim_delegate = null; //Disable player aiming outside gameplay
                shoot_delegate = null; //Disable player shooting outside gameplay
                transform.rotation = Quaternion.identity; // Reset player rotation when not outside gameplay
                break;
        }
    }

    public void On_Move(InputAction.CallbackContext callback_context)
    {
        move_direction = callback_context.ReadValue<Vector2>();
    }

    public void On_Scroll(InputAction.CallbackContext callback_context)
    {
        // Ignore scroll input if it's within the cooldown period
        if (Time.time - last_scroll_time < SCROLL_COOLDOWN)
            return;

        float scroll_value = callback_context.ReadValue<float>();

        //Debug.Log("[Player_Controller_Combat] Scroll Value: " + scroll_value);

        if (scroll_value == 0)
            return;

        int direction = (int)Mathf.Sign(scroll_value);

        if (direction < 0)
        {
            player_gun_selector.Scroll_Previous();
        }

        else
        {
            player_gun_selector.Scroll_Next();
        }

        equipped_gun = player_gun_selector.Get_Current_Gun();
        fire_rate = equipped_gun.Get_Fire_Rate();
        last_scroll_time = Time.time;
    }

    public void On_Shoot(InputAction.CallbackContext callback_context)
    {
        if (callback_context.started)
        {
            switch (equipped_gun.Get_Fire_Mode())
            {
                case Fire_Mode.FULL_AUTO:
                    shoot_delegate = Handle_Full_Auto_Fire;
                    break;

                case Fire_Mode.SINGLE_SHOT:
                    player_gun_selector.Get_Current_Gun().Shoot();
                    break;
            }
        }
        
        if (callback_context.canceled)
        {
            shoot_delegate = null;
        }
    }


    private void Update()
    {
        move_delegate?.Invoke(); //Invoke the delegate to move the player if it's not null
        aim_delegate?.Invoke(); //Invoke the delegate to aim the player if it's not null
        shoot_delegate?.Invoke(); //Invoke the delegate to shoot the player's gun if it's not null
    }
    private void Handle_Full_Auto_Fire()
    {
        if (equipped_gun.Get_Time_Since_Last_Shot() >= fire_rate)
        {
            player_gun_selector.Get_Current_Gun().Shoot();
        }
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
        GameStateManager.On_Game_State_Changed -= Handle_Game_State_Change;
    }

}
