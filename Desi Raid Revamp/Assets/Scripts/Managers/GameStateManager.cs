using System;
using UnityEngine;

public class GameStateManager
{
    private static GameState currentGameState; // Static variable to hold the current game state

    public static event Action<GameState> On_Game_State_Changed; //Event to notify when the game state change    

    public static void ChangeGameState (GameState gameState)
    {
        currentGameState = gameState; // Update the current game state to the new state
        On_Game_State_Changed?.Invoke(currentGameState); // Invoke the event to notify subscribers of the state change
        Debug.Log($"[GameStateManager] Game State changed to {currentGameState}");
    }

    public static GameState GetCurrentGameState()
    {
        return currentGameState; // Return the current game state
    }
    
}
