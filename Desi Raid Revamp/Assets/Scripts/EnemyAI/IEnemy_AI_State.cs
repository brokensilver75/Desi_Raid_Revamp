public interface IEnemy_AI_State
{
    public void Enter_State(Enemy_AI_State_Manager ai_state_manager);
    public void Exit_State();
    public void Update_State();
}