using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponHolder1 : MonoBehaviour
{
    public LayerMask whatIsEnemy; 
    void FixedUpdate()
    {
        LookAtMouse();
    }
    /*
    private void LookAtMouse()
    {
        Vector3 mousePosition = Input.mousePosition; ;
        mousePosition = Camera.main.ScreenToWorldPoint(mousePosition);
        mousePosition.z = 0;
        Collider2D[] mousedir = Physics2D.OverlapBoxAll(new Vector2((mousePosition.x + transform.parent.position.x) / 2, (mousePosition.y + transform.parent.position.y) / 2), new Vector2(Mathf.Sqrt(Mathf.Pow(mousePosition.y - transform.parent.position.y, 2) + Mathf.Pow(mousePosition.x - transform.parent.position.x, 2)), 4f), Mathf.Atan2(mousePosition.y - transform.parent.position.y, mousePosition.x - transform.parent.position.x) * Mathf.Rad2Deg,whatIsEnemy);
        Transform closestEnemy = GetClosestEnemy(mousedir);
        if (mousedir.Length != 0)
        {
            transform.up = new Vector2(closestEnemy.position.x- transform.position.x, closestEnemy.position.y - transform.position.y);
        }
        else
        {
            transform.up = new Vector2(mousePosition.x - transform.position.x, mousePosition.y - transform.position.y);
        }
        DebugDrawBox(new Vector2((mousePosition.x+transform.parent.position.x)/2,(mousePosition.y+transform.parent.position.y)/2), new Vector2(Mathf.Sqrt(Mathf.Pow(mousePosition.y-transform.parent.position.y, 2) + Mathf.Pow(mousePosition.x-transform.parent.position.x, 2)),4f), Mathf.Atan2(mousePosition.y-transform.parent.position.y,mousePosition.x-transform.parent.position.x)*Mathf.Rad2Deg, Color.blue, 0.1f);
    }
    void DebugDrawBox(Vector2 point, Vector2 size, float angle, Color color, float duration)
    {

        var orientation = Quaternion.Euler(0, 0, angle);

        // Basis vectors, half the size in each direction from the center.
        Vector2 right = orientation * Vector2.right * size.x / 2f;
        Vector2 up = orientation * Vector2.up * size.y / 2f;

        // Four box corners.
        Vector2 topLeft = point + up - right;
        Vector2 topRight = point + up + right;
        Vector2 bottomRight = point - up + right;
        Vector2 bottomLeft = point - up - right;

        // Now we've reduced the problem to drawing lines.
        Debug.DrawLine(topLeft, topRight, color, duration);
        Debug.DrawLine(topRight, bottomRight, color, duration);
        Debug.DrawLine(bottomRight, bottomLeft, color, duration);
        Debug.DrawLine(bottomLeft, topLeft, color, duration);
    }
    private Transform GetClosestEnemy(Collider2D[] wps)
    {
        Transform bestTarget = null;
        float closestDistanceSqr = Mathf.Infinity;
        Vector3 currentPosition = transform.position;
        foreach (Collider2D potentialTarget in wps)
        {
            Vector3 directionToTarget = potentialTarget.transform.position - currentPosition;
            float dSqrToTarget = directionToTarget.sqrMagnitude;
            if (dSqrToTarget < closestDistanceSqr)
            {
                closestDistanceSqr = dSqrToTarget;
                bestTarget = potentialTarget.transform;
            }
        }
        return bestTarget;
    }
    */
    void LookAtMouse()
    {
        Vector3 mousePosition = Input.mousePosition; ;
        mousePosition = Camera.main.ScreenToWorldPoint(mousePosition);
        mousePosition.z = 0;
        transform.up = new Vector2(mousePosition.x - transform.position.x, mousePosition.y - transform.position.y);
    }
}
