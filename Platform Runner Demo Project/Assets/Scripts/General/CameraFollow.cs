using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] private Vector3 offset;
    [SerializeField] private float followTime = 10f;

    private Transform player;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    private void LateUpdate()
    {
        Vector3 newPosition = new Vector3(Mathf.Clamp(player.position.x, offset.x - 3, offset.x + 3), offset.y, player.position.z + offset.z);
        transform.position = Vector3.Lerp(transform.position, newPosition, followTime * Time.deltaTime);
    }
}
