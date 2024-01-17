using AxGrid.FSM;
using UnityEngine;

[State("Stop")]
public class StopGameState : FSMState
{
    [Enter]
    public void Enter()
    {
        Model?.EventManager.Invoke($"OnStopSpin");
        Model.Set("StartEn", true);
        Model.Set("StopEn", false);
    }
    
    [Exit]
    public void Exit()
    {
        Debug.Log("Exit Stop state");
    }
}