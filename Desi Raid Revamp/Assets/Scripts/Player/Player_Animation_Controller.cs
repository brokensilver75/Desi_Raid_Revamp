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
    }

    private void Update()
    {
        Animate_Player_Combat();
    }

    private void Animate_Player_Combat()
    {
        local_move_vector = transform.InverseTransformDirection(new Vector3(move_direction.x, 0, move_direction.y));
        player_animator.SetFloat("UpDown", Mathf.Lerp(player_animator.GetFloat("UpDown"), local_move_vector.z, anim_smooth_multiplier));
        player_animator.SetFloat("LeftRight", Mathf.Lerp(player_animator.GetFloat("LeftRight"), local_move_vector.x, anim_smooth_multiplier));
    }    

    public void OnMove(InputAction.CallbackContext callbackContext)
    {
        move_direction = callbackContext.ReadValue<Vector2>();
    }

}
