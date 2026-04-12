using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class Player_Animation_Controller : MonoBehaviour
{
    [SerializeField] Animator player_animator;
    [SerializeField] float anim_smooth_multiplier = 5f;
    private Vector2 move_direction;
    private Vector3 local_move_vector;

    delegate void Animate_Player_Delegate();
    Animate_Player_Delegate animate_player_delegate;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        GameStateManager.On_Game_State_Changed += Handle_Game_State_Change;
    }

    private void Update()
    {
        animate_player_delegate?.Invoke();
        //Animate_Player_Combat();
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

    private void Handle_Game_State_Change(GameStates states)
    {
        switch (states)
        {
            case GameStates.LEVEL_PLAY:
                player_animator.SetBool("hub_play", false);
                animate_player_delegate = Animate_Player_Combat;
                break;
            case GameStates.HUB_PLAY:
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
