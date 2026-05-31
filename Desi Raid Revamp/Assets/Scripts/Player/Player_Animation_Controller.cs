using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player_Animation_Controller : MonoBehaviour
{
    [SerializeField] Animator player_animator;
    [SerializeField] float anim_smooth_multiplier = 5f;
    private Vector2 move_direction;
    private Vector3 local_move_vector;

    delegate void Animate_Player_Delegate();
    Animate_Player_Delegate animate_player_delegate;

    IEnumerator Start()
    {
        yield return new WaitUntil(() => Game_Manager.game_manager_initialized);

        GameStateManager.On_Game_State_Changed += Handle_Game_State_Change;

        Handle_Game_State_Change(GameStateManager.GetCurrentGameState());
    }

    private void Update()
    {
        animate_player_delegate?.Invoke();
    }

    private void Animate_Player_Combat()
    {
        local_move_vector = transform.InverseTransformDirection(new Vector3(move_direction.x, 0, move_direction.y));
        player_animator.SetFloat("UpDown", Mathf.Lerp(player_animator.GetFloat("UpDown"), local_move_vector.z, anim_smooth_multiplier));
        player_animator.SetFloat("LeftRight", Mathf.Lerp(player_animator.GetFloat("LeftRight"), local_move_vector.x, anim_smooth_multiplier));
    }

    private void Animate_Player_Hub()
    {
        float magnitude = move_direction.magnitude;
        magnitude = Mathf.Ceil(magnitude);
        player_animator.SetFloat("Moving", magnitude);
    }

    private void Handle_Game_State_Change(GameState states)
    {
        switch (states)
        {
            case GameState.LEVEL_PLAY:
                player_animator.SetBool("hub_play", false);
                animate_player_delegate = Animate_Player_Combat;
                break;
            case GameState.HUB_PLAY:
                player_animator.SetBool("hub_play", true);
                animate_player_delegate = Animate_Player_Hub;
                break;
        }
    }

    public void OnMove(InputAction.CallbackContext callbackContext)
    {
        move_direction = callbackContext.ReadValue<Vector2>();
    }




}
