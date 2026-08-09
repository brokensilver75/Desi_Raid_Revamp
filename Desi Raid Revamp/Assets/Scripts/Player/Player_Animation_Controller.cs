using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem; // Retained from your old script
using UnityEngine.Animations.Rigging; // Added for the new IK system

public class Player_Animation_Controller : MonoBehaviour
{
    [Header("Animator & Rigging")]
    [SerializeField] private Animator player_animator;
    [SerializeField] private RigBuilder rigBuilder;
    
    [Header("IK Constraints")]
    [SerializeField] private TwoBoneIKConstraint leftHandIK;
    [SerializeField] private TwoBoneIKConstraint rightHandIK;

    [Header("Movement Settings")]
    [SerializeField] private float anim_smooth_multiplier = 10f; // Adjusted for Time.deltaTime
    private Vector2 move_direction; // Retained for storing Input System values
    private Vector3 local_move_vector; // Retained for local space conversion

    [Header("IK Settings")]
    [SerializeField] private float ikBlendSpeed = 10f;
    private float targetIKWeight = 1f;
    [Space(20)]

    [Header("Testing")]
    [SerializeField] private Player_IK_Controller player_ik_controller;


    // Cached Animator Hashes for performance
    private readonly int gunTypeHash = Animator.StringToHash("GunType");
    private readonly int upDownHash = Animator.StringToHash("UpDown");
    private readonly int leftRightHash = Animator.StringToHash("LeftRight");
    private readonly int movingHash = Animator.StringToHash("Moving");

    IEnumerator Start()
    {
        // Wait for the game manager to initialize before proceeding[cite: 6]
        yield return new WaitUntil(() => Game_Manager.game_manager_initialized);
    }

    private void Update()
    {
        Animate_Player_Combat();
        AdjustIKWeights();
    }

    // This remains linked to your Player Input component
    public void OnMove(InputAction.CallbackContext callbackContext)
    {
        move_direction = callbackContext.ReadValue<Vector2>();
    }

    private void Animate_Player_Combat()
    {
        // Convert world movement input into local space based on aiming direction
        local_move_vector = transform.InverseTransformDirection(new Vector3(move_direction.x, 0, move_direction.y));

        // Smoothly transition parameters. Note: Added Time.deltaTime to your original logic for framerate independence.
        player_animator.SetFloat(upDownHash, Mathf.Lerp(player_animator.GetFloat(upDownHash), local_move_vector.z, anim_smooth_multiplier * Time.deltaTime));
        player_animator.SetFloat(leftRightHash, Mathf.Lerp(player_animator.GetFloat(leftRightHash), local_move_vector.x, anim_smooth_multiplier * Time.deltaTime));
        
        // Update the Moving parameter to trigger states like idle to run
        player_animator.SetFloat(movingHash, move_direction.magnitude > 0.1f ? 1f : 0f);
    }

    /// <summary>
    /// Call this from your weapon management script when equipping a gun.
    /// </summary>
    public void EquipWeapon(int weaponType, Transform newLeftGrip, Transform newRightGrip)
    {
        player_ik_controller.Set_Left_IK_Target(newLeftGrip);
        player_ik_controller.Set_Right_IK_Target(newRightGrip);

        player_animator.SetInteger(gunTypeHash, weaponType);

        if (newLeftGrip != null)
        {
            leftHandIK.data.target.position = newLeftGrip.position;
            leftHandIK.data.target.rotation = newLeftGrip.rotation;
        }

        if (newRightGrip != null)
        {
            rightHandIK.data.target.position = newRightGrip.position;
            rightHandIK.data.target.rotation = newRightGrip.rotation;
        }

        rigBuilder.Build(); 
    }

    /// <summary>
    /// Call this to blend IK hands off (e.g., during a reload animation) and on (aiming).
    /// </summary>
    public void SetIKWeight(bool isActive)
    {
        targetIKWeight = isActive ? 1f : 0f;
    }

    private void AdjustIKWeights()
    {
        leftHandIK.weight = Mathf.Lerp(leftHandIK.weight, targetIKWeight, Time.deltaTime * ikBlendSpeed);
        rightHandIK.weight = Mathf.Lerp(rightHandIK.weight, targetIKWeight, Time.deltaTime * ikBlendSpeed);
    }
}