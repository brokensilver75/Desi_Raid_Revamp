using UnityEngine;

public class Observe_State : IEnemy_AI_State
{
    /* Enemy_AI_Stats_SO ai_stats;
    GameObject player_object, enemy_object;

    public void Set_Target_Parameters(Enemy_AI_Stats_SO ai_stats, GameObject target_object, GameObject self_object)
    {
        this.ai_stats = ai_stats;
        this.player_object = target_object;
        this.enemy_object = self_object;
    } */

    Enemy_AI_State_Manager ai_state_manager;

    #region IEnemy_State_Implementation
    public void Enter_State(Enemy_AI_State_Manager enemy_AI_State_Manager)
    {
        Debug.Log("Entered Observe State");
        ai_state_manager = enemy_AI_State_Manager;
    }

    public void Exit_State()
    {
        Debug.Log("Exited Observe State");
    }

    public void Update_State()
    {
        Debug.Log("[Observe_State] Updating Observe State");

        if (ai_state_manager == null)
        {
            Debug.Log($"[Observe_State] AI State Manager is null");
            return;
        }
        
        float distance_to_target = Vector3.Distance(ai_state_manager.transform.position, ai_state_manager.player_object.transform.position);

        if (distance_to_target > ai_state_manager.enemy_stats_so.vision_radius)
        {
            Debug.Log($"[Observe_State] Player out of sensory range!");
            return;
        }

        Debug.Log($"[Observe_State] Player within sensory range!");
        ai_state_manager.Change_State(ai_state_manager.follow_state);
    } 
    #endregion
}
