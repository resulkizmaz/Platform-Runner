using UnityEngine;

public class Rotator : Transformer
{
    public enum RotateDirection { Left, Right }; // Directions clockwise or counterclockwise.

    [SerializeField] private RotateDirection rotateDirection;
    [HideInInspector]
    public Vector3 rotateAxis;

    public float p_Speed => speed;
    

    private int directionMultiplier;
    public int p_DirectionMultiplier => directionMultiplier;


    private void Start()
    {
        switch (rotateDirection)
        {
            case RotateDirection.Left:
                directionMultiplier = 1;
                break;

            case RotateDirection.Right:
                directionMultiplier = -1;
                break;
        }

        switch (direction)
        {
            case Direction.X_Axis:
                rotateAxis = Vector3.right;
                break;

            case Direction.Y_Axis:
                rotateAxis = Vector3.up;
                break;

            case Direction.Z_Axis:
                rotateAxis = Vector3.forward;
                break;

            default:
                break;
        }
    }

    private void Update()
    {
        targetObject.transform.Rotate(rotateAxis * speed * directionMultiplier * Time.deltaTime);
    }
}
