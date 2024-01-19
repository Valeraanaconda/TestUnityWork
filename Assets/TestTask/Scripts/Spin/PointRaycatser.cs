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
            float distanceToHitObject = hit.distance;


            GameObject hitObject = hit.collider.gameObject;
            // Debug.Log("Луч столкнулся с объектом: " + hitObject.name);
            // Debug.Log("Дистанция до объекта: " + distanceToHitObject);

            return 67 + distanceToHitObject;
        }
        return 0;
    }

    // private void Update()
    // {
    //     var rayOrigin = new Vector2(transform.position.x,transform.position.y+Y);
    //     Vector3 rayDirection = transform.up;
    //
    //     VisualizeRay(rayOrigin, rayDirection, length);
    // }
    //
    // void VisualizeRay(Vector3 origin, Vector3 direction, float length)
    // {
    //     Debug.DrawRay(origin, direction * length, Color.red);
    // }
}