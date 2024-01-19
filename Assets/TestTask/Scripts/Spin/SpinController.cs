using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AxGrid.Base;
using AxGrid.Model;
using DG.Tweening;
using UnityEngine;

public class SpinController : MonoBehaviourExtBind
{
    [SerializeField] private List<SpiningElement> _spiningElements;
    [SerializeField] private float _speed = 1.0f;
    [SerializeField] private float _startDuration = 1f;
    [SerializeField] private float _stopDuration = 1f;
    public PointRaycatser raycster; 

    private Action _OnSpin;
    private float _startSpeed = 0f;
    private bool _isSpin;

    [OnAwake]
    private void Init()
    {
        foreach (var value in _spiningElements)
        {
            value.Init();
        }
    }

    [OnUpdate]
    private void Spin()
    {
        if (_isSpin)
        {
            _OnSpin?.Invoke();
        }
    }

    private Task StartTweenAnimation(List<SpiningElement> spinPair)
    { 
        return DOTween.To(() => _startSpeed, x => _startSpeed = x, _speed, _startDuration)
            .OnUpdate(()=>ChangeSpeed(spinPair, _startSpeed))
            .OnComplete(()=>
            {
                ChangeSpeed(spinPair, _startSpeed);
                foreach (var value in spinPair)
                {
                    _OnSpin += value.Spin;
                }
            })
            .AsyncWaitForCompletion();
    }

    private void ChangeSpeed(List<SpiningElement> spinPair,float speed)
    {
        foreach (var spin in spinPair)
        {
            spin.Speed = _startSpeed;
            spin.Spin();
        }
    }

    [Bind("OnStartSpin")]
    private async void StartSpin()
    {
        _isSpin = true;
        
        List<SpiningElement> pair = new List<SpiningElement>();

        foreach (var element in _spiningElements)
        {
            pair.Add(element);

            if (pair.Count == 2)
            {
                await StartTweenAnimation(pair);
                pair.Clear();
            }
        }
    }

    [Bind("OnStopSpin")]
    private void StopSpin()
    {
        //_isSpin = false;
        
        // foreach (var value in _spiningElements)
        // {
        //     _OnSpin -= value.Spin;
        // }
        
        List<SpiningElement> pair = new List<SpiningElement>();

        foreach (var element in _spiningElements)
        {
            pair.Add(element);
            _OnSpin -= element.Spin;
            

            if (pair.Count == 2)
            {
                foreach (var value in pair)
                {
                    value.GoToEndPosition(raycster.GetDistance(),_stopDuration);
                }
                return;
            }
        }
    }
}