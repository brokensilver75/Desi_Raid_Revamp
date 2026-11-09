using UnityEngine;

public interface IEnemy_AI_State
{
    public void Enter_State();
    public void Exit_State();
    public void Update_State();
    public void Change_State();
}