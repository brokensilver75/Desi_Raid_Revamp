using System;
using System.Collections.Generic;
using Unity.Cinemachine;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    public static CameraManager instance; // Singleton instance of the CameraManager
    [Serializable]
    public class PlayerCameraWithState
    {
        public CinemachineCamera player_camera; //Cinamachine camera for the player
        public GameStates game_state; //Game state associated with the camera
    }

    [Header("Combat Camera Values")]
    [SerializeField] public List<PlayerCameraWithState> player_cameras; //List of Cinemachine cameras for the player

    CinemachineCamera active_camera; // Reference to active camera

    private void Awake()
    {
        if (instance == null)
        {
            instance = this; // Set the singleton instance to this object
        }
        else
        {
            Destroy(gameObject); // Destroy this object if an instance already exists
        }
    }

    private void Start()
    {
        GameStateManager.On_Game_State_Changed += HandleCameraStateChange;
    }

    private void HandleCameraStateChange(GameStates states)
    {
        switch (states)
        {
            case GameStates.LEVEL_PLAY:
                foreach (PlayerCameraWithState camera in player_cameras)
                {
                    if (camera.game_state == GameStates.LEVEL_PLAY)
                    {
                        active_camera = camera.player_camera; // Set the active camera reference
                        camera.player_camera.gameObject.SetActive(true); // Activate the camera for the current game state
                    }
                    else
                    {
                        camera.player_camera.gameObject.SetActive(false); // Deactivate cameras that are not associated with the current game state
                    }
                }
                break;

            case GameStates.HUB_PLAY:
                foreach (PlayerCameraWithState camera in player_cameras)
                {
                    if (camera.game_state == GameStates.HUB_PLAY)
                    {
                        active_camera = camera.player_camera; // Set the active camera reference
                        camera.player_camera.gameObject.SetActive(true); // Activate the camera for the current game state
                        //camera.player_camera.Priority = 10; // Set high priority for the active camera
                    }
                    else
                    {
                        camera.player_camera.gameObject.SetActive(false); // Deactivate cameras that are not associated with the current game state
                        //camera.player_camera.Priority = 0; // Set low priority for inactive cameras
                    }
                }
                break;

            default:

                break;
        }
    }

    public CinemachineCamera GetActiveCamera()
    {
        return active_camera;
    }

    private void OnDestroy()
    {
        GameStateManager.On_Game_State_Changed -= HandleCameraStateChange;
    }
}
