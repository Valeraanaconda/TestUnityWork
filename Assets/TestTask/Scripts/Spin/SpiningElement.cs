﻿using System.Threading.Tasks;
using AxGrid.Base;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class SpiningElement : MonoBehaviourExtBind
{
    [SerializeField] private RectTransform _parentConteiner;
    [SerializeField] private RectTransform _respawnPosition;

    public float Speed { get; set; }

    private float _targetY;
    private Vector2 _initialPosition;
    
    public void Init()
    {
        _initialPosition = _respawnPosition.transform.localPosition;

        if (TryGetComponent(out VerticalLayoutGroup verticalLayoutGroup))
        {
            var spacing = verticalLayoutGroup.spacing;

            _initialPosition = new Vector2(_initialPosition.x, _initialPosition.y + spacing);
        }

        _targetY = _parentConteiner.rect.height / 2 * -1;
    }

    public void Spin()
    {
        Vector3 currentPosition = transform.localPosition;

        var newY = currentPosition.y - Speed;
        
        CheckTargetY(newY,currentPosition);
    }

    private float value;
    public Task GoToEndPosition(float dist,float duration)
    {
        Vector3 currentPosition = transform.localPosition;
        value = dist;
        float newY = currentPosition.y - dist;
        
        return transform.DOLocalMoveY(newY, duration)
            .AsyncWaitForCompletion();
    }

    public void ApplyPosition()
    {
        Vector3 currentPosition = transform.localPosition;
        float newY = currentPosition.y - value;
        transform.localPosition = new Vector2(currentPosition.x, newY);
    }

    private void CheckTargetY(float Y,Vector3 currentPosition)
    {
        if (Y <= _targetY)
        {
            RestartPosition();
        }
        else
        {
            transform.localPosition = new Vector2(currentPosition.x, Y);
        }
    }
    
    private void RestartPosition()
    {
        transform.localPosition = _initialPosition;
    }
}