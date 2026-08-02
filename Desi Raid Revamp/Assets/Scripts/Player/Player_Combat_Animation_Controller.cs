using System.Collections;
using UnityEngine;
using UnityEngine.Animations.Rigging; // Required for IK constraints

public class Player_Combat_Animation_Controller : MonoBehaviour
{
    [Header("Animator & Rigging")]
    [SerializeField] private Animator animator;
    [SerializeField] private RigBuilder rigBuilder;

    [Header("IK Constraints")]
    [SerializeField] private TwoBoneIKConstraint leftHandIK;
    [SerializeField] private TwoBoneIKConstraint rightHandIK;
    [SerializeField] private MultiAimConstraint spineAimIK;

    [Header("IK Settings")]
    [SerializeField] private float ikBlendSpeed = 10f;
    private float targetIKWeight = 1f;

    // Cached Animator Hashes for performance
    private readonly int gunTypeHash = Animator.StringToHash("GunType");
    private readonly int upDownHash = Animator.StringToHash("UpDown");
    private readonly int leftRightHash = Animator.StringToHash("LeftRight");
    private readonly int movingHash = Animator.StringToHash("Moving");

    private IEnumerator Start()
    {
        // Wait for the game manager to initialize before proceeding[cite: 2]
        yield return new WaitUntil(() => Game_Manager.game_manager_initialized);
    }

    private void Update()
    {
        // Smoothly blend IK weights on and off (e.g., set targetIKWeight to 0 when reloading)
        AdjustIKWeights();
    }

    /// <summary>
    /// Call this method when the player equips a new weapon.
    /// </summary>
    public void EquipWeapon(int weaponType, Transform newLeftGrip, Transform newRightGrip)
    {
        // Updates the Animator to transition to Pistol Idle (0) or Idle Aiming (1)
        animator.SetInteger(gunTypeHash, weaponType);

        // Reassign the IK targets to the new weapon's grip points
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

        // Force the rig to update immediately to prevent 1-frame visual snapping
        rigBuilder.Build();
    }

    /// <summary>
    /// Feeds movement input into the Combat Blend Tree.
    /// In a top-down shooter, locomotion is calculated relative to where the player is aiming.
    /// </summary>
    public void UpdateLocomotion(Vector2 movementInput, Vector3 lookDirection)
    {
        // Convert world movement input into local space based on where the spine is aiming
        Vector3 localMove = transform.InverseTransformDirection(new Vector3(movementInput.x, 0, movementInput.y));

        // Feeds the UpDown and LeftRight parameters to drive the Combat Blend Tree
        animator.SetFloat(leftRightHash, localMove.x, 0.1f, Time.deltaTime);
        animator.SetFloat(upDownHash, localMove.z, 0.1f, Time.deltaTime);

        // Updates the Moving parameter for state transitions[cite: 1]
        animator.SetFloat(movingHash, movementInput.magnitude > 0.1f ? 1f : 0f);
    }

    /// <summary>
    /// Call this to toggle IK on (aiming) or off (sprinting, reloading).
    /// </summary>
    public void SetIKWeight(bool isActive)
    {
        targetIKWeight = isActive ? 1f : 0f;
    }

    private void AdjustIKWeights()
    {
        // Smoothly lerp the weights so arms don't snap violently
        leftHandIK.weight = Mathf.Lerp(leftHandIK.weight, targetIKWeight, Time.deltaTime * ikBlendSpeed);
        rightHandIK.weight = Mathf.Lerp(rightHandIK.weight, targetIKWeight, Time.deltaTime * ikBlendSpeed);

        // Optional: reduce spine aim weight slightly if needed, or keep it locked
        spineAimIK.weight = Mathf.Lerp(spineAimIK.weight, targetIKWeight, Time.deltaTime * ikBlendSpeed);
    }
}