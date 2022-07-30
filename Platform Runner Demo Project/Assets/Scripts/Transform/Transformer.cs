using UnityEngine;

public class Transformer : MonoBehaviour
{
    public enum Direction { X_Axis, Y_Axis, Z_Axis }; // The Axis that the object rotate around.

    [Header("Reference")]
    [SerializeField] protected GameObject targetObject;
    [Header("Settings")]
    [SerializeField] protected Direction direction; //Direction of movement
    [SerializeField] protected float speed = 10f; //Movement speed

    public Direction p_Direction => direction;
}
