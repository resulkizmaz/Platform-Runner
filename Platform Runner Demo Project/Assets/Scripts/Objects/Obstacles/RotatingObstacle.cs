using UnityEngine;

public class RotatingObstacle : ObstaclesTagManager
{
    private void FixedUpdate()
    {
        transform.Rotate(0, 2, 0);
    }
   
}
