using System;
using System.Collections.Generic;
using AxGrid.Base;
using AxGrid.Model;
using UnityEngine;

public class SpinController : MonoBehaviourExtBind
{
    [SerializeField] private List<SpiningElement> _spiningElements;
    [SerializeField] private float _speed = 1.0f;
    
    private Action OnSpin;
    
    private bool _isSpin;

    [OnAwake]
    private void Init()
    {
        foreach (var value in _spiningElements)
        {
            value.Init(_speed);
            OnSpin += value.Spin;
        }
    }

    [OnUpdate]
    private void Spin()
    {
        if (_isSpin)
        {
            OnSpin?.Invoke();
        }
    }
    
    [Bind("OnStartSpin")]
    private void StartSpin()
    {
        _isSpin = true;
    }

    [Bind("OnStopSpin")]
    private void StopSpin()
    {
        _isSpin = false;
    }
}
