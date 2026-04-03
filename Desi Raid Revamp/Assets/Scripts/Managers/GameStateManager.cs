using System;
using UnityEngine;

public class GameStateManager : MonoBehaviour
{
    private static GameStates currentGameState; // Static variable to hold the current game state

    public static event Action<GameStates> On_Game_State_Changed; //Event to notify when the game state change    

    public static void ChangeGameState (GameStates gameState)
    {
        currentGameState = gameState; // Update the current game state to the new state
        On_Game_State_Changed?.Invoke(currentGameState); // Invoke the event to notify subscribers of the state change
    }



    public static GameStates GetCurrentGameState()
    {
        return currentGameState; // Return the current game state
    }

    [ContextMenu("Start Game")]
    public void StartCombat()
    {
        ChangeGameState(GameStates.LEVEL_PLAY);
    }

    [ContextMenu("Start Hub")]
    public void StartHub()
    {
        ChangeGameState(GameStates.HUB_PLAY);
    }
}
