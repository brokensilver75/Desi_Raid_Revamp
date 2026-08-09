using UnityEngine;
using UnityEngine.Animations.Rigging;

public class Player_IK_Controller : MonoBehaviour
{
    [SerializeField] RigBuilder rigBuilder;
    
    [SerializeField] TwoBoneIKConstraint leftHandIK;
    [SerializeField] TwoBoneIKConstraint rightHandIK;

    [SerializeField] Transform targetIK_right;
    [SerializeField] Transform targetIK_left;

    public void Set_Right_IK_Target(Transform target_IK)
    {
        targetIK_right = target_IK;        
    }

    public void Set_Left_IK_Target(Transform target_IK)
    {
        targetIK_left = target_IK;
    }

    // Update is called once per frame
    void Update()
    {
        if (targetIK_left != null && targetIK_right != null)
        {
            rightHandIK.data.target.position = targetIK_right.position;
            rightHandIK.data.target.rotation = targetIK_right.rotation;

            leftHandIK.data.target.position = targetIK_left.position;
            leftHandIK.data.target.rotation = targetIK_left.rotation; 
        }
    }
}
