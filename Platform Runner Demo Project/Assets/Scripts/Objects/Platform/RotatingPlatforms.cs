using UnityEngine;

public class RotatingPlatforms : MonoBehaviour
{
    private void FixedUpdate()
    {
        transform.Rotate(0, 0, 2);
    }
}
