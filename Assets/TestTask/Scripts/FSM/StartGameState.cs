using System;
using System.Threading.Tasks;
using AxGrid.FSM;
using UnityEngine;

[State("Start")]
public class StartGameState : FSMState
{
    private const float PAUSE_DURATION = 3000f;

    [Enter]
    public async void Enter()
    {
        Debug.Log("Enter Start state");
        Model.Set("StartEn", false);
        Model.Set("StopEn", false);
        
        Model?.EventManager.Invoke($"OnStartSpin");
        
        await Task.Delay(TimeSpan.FromMilliseconds(PAUSE_DURATION));
        Model.Set("StopEn", true);
    }
    
    [Exit]
    public void Exit()
    {
        Debug.Log("Exit Start state");
    }
}