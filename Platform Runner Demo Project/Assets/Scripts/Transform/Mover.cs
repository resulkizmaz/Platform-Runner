using UnityEngine;
using DG.Tweening;

public class Mover : Transformer
{
    [Space(10)]
    [SerializeField] private Vector3 endPoint;
    [Range(0.1f, 1f)]
    [SerializeField] private float gizmoRadius = 0.3f;


    private void Start()
    {
        // Animate the objects with code. ( using DoTween :)
        targetObject.transform.DOMove(transform.position + endPoint, speed).SetEase(Ease.Linear).SetLoops(-1, LoopType.Yoyo);
    }

    //ON DRAW GIZMOS
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position + new Vector3(0f, endPoint.y, 0f), transform.position + endPoint);
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(transform.position + endPoint, gizmoRadius);
    }
}
