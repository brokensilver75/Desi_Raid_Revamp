using System.Runtime.CompilerServices;
using UnityEngine;

public class Enemy_AI_State_Manager : MonoBehaviour
{
    [Header ("Data and Stats")]
    [SerializeField] public Enemy_AI_Stats_SO enemy_stats_so;    

    [SerializeField] public GameObject player_object;
    [Space(20)]

    IEnemy_AI_State current_state;

    [Header ("AI States")]
    [SerializeField] public Follow_State follow_state;
    [SerializeField] public Observe_State observe_state;

    void Awake()
    {
        observe_state = new Observe_State();
        follow_state = new Follow_State();
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (observe_state != null)
        {
            Change_State(observe_state);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (current_state != null)
        {
            current_state.Update_State();
        }
    }

    public void Change_State(IEnemy_AI_State new_state)
    {
        if (current_state != null)
        {
            current_state.Exit_State();
        }

        current_state = new_state;
        
        current_state.Enter_State(this);
    }
    
}
