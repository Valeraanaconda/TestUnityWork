using AxGrid.Base;
using UnityEngine;

public class PointRaycatser : MonoBehaviourExt
{
    public float GetDistance()
    {
        var rayOrigin = transform.position;
        var rayDirection = transform.up;

        var hit = Physics2D.Raycast(rayOrigin, rayDirection);
        
        if (hit.collider != null)
        {
            float distance = Vector2.Distance(transform.position,hit.transform.position);

            return distance;
        }
        return 0;
    }
}