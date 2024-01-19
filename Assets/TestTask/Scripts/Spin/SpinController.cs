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
    [SerializeField] private List<PointRaycatser> _raycsters;
    [SerializeField] private float _speed = 1.0f;
    [SerializeField] private float _startDuration = 1f;
    [SerializeField] private float _stopDuration = 1f;

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

    private Task StartSpinning(List<SpiningElement> spinPair)
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
        //todo RefactorThis
        List<SpiningElement> pair = new List<SpiningElement>();

        foreach (var element in _spiningElements)
        {
            pair.Add(element);

            if (pair.Count == 2)
            {
                await StartSpinning(pair);
                pair.Clear();
            }
        }
    }

    [Bind("OnStopSpin")]
    private async void StopSpin()
    {
        List<SpiningElement> pair = new List<SpiningElement>();
        List<Task> tasks = new List<Task>();
        int count = 0;
        
        foreach (var element in _spiningElements)
        {
            pair.Add(element);
            _OnSpin -= element.Spin;
            
            if (pair.Count == 2)
            {
                foreach (var value in pair)
                {
                    Task task = value.GoToEndPosition(_raycsters[count].GetDistance(), _stopDuration);
                    tasks.Add(task);
                }
                
                await Task.WhenAll(tasks);
                pair.Clear();
                tasks.Clear();
                
                count++;
            }
        }
        
        _isSpin = false;
    }
}