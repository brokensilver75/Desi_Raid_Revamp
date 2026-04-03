using UnityEngine;
using UnityEngine.InputSystem;

public static class Mouse_Input
{
    public static Vector3 GetMousePosition(Camera mainCamera, LayerMask maskLayer)
    {
        Ray ray = mainCamera.ScreenPointToRay(Mouse.current.position.ReadValue());

        if (Physics.Raycast(ray, out RaycastHit hitInfo, Mathf.Infinity, maskLayer))
        {
            //Debug.Log($"[Mouse_Input] Mouse Position: {hitInfo.point}");
            return hitInfo.point;
        }

        else
        {
            //Debug.Log("[Mouse_Input] Mouse position is not on the aim layer.");
            return Vector3.zero;
        }
    }
}
