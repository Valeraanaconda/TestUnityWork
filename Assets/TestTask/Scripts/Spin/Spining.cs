using AxGrid.Base;
using AxGrid.Model;
using UnityEngine;
using UnityEngine.UI;

public class Spining : MonoBehaviourExtBind
{
    [SerializeField] private RectTransform _parentConteiner;
    [SerializeField] private RectTransform _respawnPosition;
    [SerializeField] private float _speed = 1.0f;

    private float _targetY;
    private Vector2 _initialPosition;
    private bool _isSpin;

    [OnAwake]
    private void Init()
    {
        _initialPosition = _respawnPosition.transform.localPosition;

        if (TryGetComponent(out VerticalLayoutGroup verticalLayoutGroup))
        {
            var spacing = verticalLayoutGroup.spacing;

            _initialPosition = new Vector2(_initialPosition.x, _initialPosition.y + spacing);
        }

        _targetY = _parentConteiner.rect.height / 2 * -1;
    }
    
    [OnUpdate]
    private void Spin()
    {
        if (_isSpin)
        {
            Vector3 currentPosition = transform.localPosition;

            var newY = currentPosition.y - _speed;

            if (newY <= _targetY)
            {
                RestartPosition();
            }
            else
            {
                transform.localPosition = new Vector2(currentPosition.x, newY);
            }
        }
    }
    
    private void RestartPosition()
    {
        transform.localPosition = _initialPosition;
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