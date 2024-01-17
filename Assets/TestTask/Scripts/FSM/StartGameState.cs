using System.Threading.Tasks;
using AxGrid.FSM;
using UnityEngine;

[State("Start")]
public class StartGameState : FSMState
{
    [Enter]
    public async void Enter()
    {
        Debug.Log("Enter Start state");
        Model.Set("StartEn", false);
        Model.Set("StopEn", false);
        
        Model?.EventManager.Invoke($"OnStartSpin");
        
        await Task.Delay(3000);
        Model.Set("StopEn", true);
    }
    
    [Exit]
    public void Exit()
    {
        Debug.Log("Exit Start state");
    }
}