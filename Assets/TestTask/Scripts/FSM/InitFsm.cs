using AxGrid;
using AxGrid.Base;
using AxGrid.FSM;
using AxGrid.Model;

public class InitFsm : MonoBehaviourExtBind
{
    private const string INIT = "Init";
    private const string START = "Start";
    private const string STOP = "Stop";
        
    [OnAwake]
    private void CreateFsm()
    {
        Settings.Fsm = new FSM();
        Settings.Fsm.Add(new InitState());
        Settings.Fsm.Add(new StartGameState());
        Settings.Fsm.Add(new StopGameState());
            
        Settings.Fsm.Start(INIT);
    }
    
    [Bind("OnStartClick")]
    private void GoToGameState()
    {
        Settings.Fsm.Change(START);
    }
    
    [Bind("OnStopClick")]
    public void GoToStopState()
    {
        Settings.Fsm.Change(STOP);
    }
}