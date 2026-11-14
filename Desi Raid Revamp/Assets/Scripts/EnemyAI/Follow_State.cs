using UnityEngine;
using UnityEngine.AI;

public class Follow_State : IEnemy_AI_State
{
    Enemy_AI_State_Manager ai_state_manager;

    #region IEnemy_AI_State Implementation
    public void Enter_State(Enemy_AI_State_Manager enemy_AI_State_Manager)
    {
        Debug.Log("Entering Follow State");
        ai_state_manager = enemy_AI_State_Manager;
    }

    public void Exit_State()
    {
        Debug.Log("Exiting Follow State");
    }

    public void Update_State()
    {
        Debug.Log("Updating Follow State");
        
        if (ai_state_manager == null)
        {
            Debug.Log($"[Observe_State] AI State Manager is null");
            return;
        }
        
        Transform enemy_transform = ai_state_manager.transform;
        NavMeshAgent enemy_navmesh_agent = ai_state_manager.navmesh_agent;
        Vector3 direction = (ai_state_manager.player_object.transform.position - enemy_transform.position).normalized;
        
        enemy_transform.forward = new Vector3(direction.x, 0, direction.z);
        
        if (enemy_navmesh_agent != null)
        {
            enemy_navmesh_agent.destination = ai_state_manager.player_object.transform.position;
        }
    }    
    #endregion
}
