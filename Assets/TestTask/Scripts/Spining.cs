using AxGrid.Base;
using AxGrid.Model;
using UnityEngine;

public class Spining : MonoBehaviourExtBind
{
    [Bind("OnStartSpin")]
    private void StartSpin()
    {
        Debug.Log("Start Spin");
    }

    [Bind("OnStopSpin")]
    private void StopSpin()
    {
        Debug.Log("Stop Spin");
    }
}