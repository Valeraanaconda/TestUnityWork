using AxGrid.FSM;
using UnityEngine;

[State("Init")]
public class InitState : FSMState
{
    [Enter]
    public void Enter()
    {
        Debug.Log("Enter init state");
    }
}