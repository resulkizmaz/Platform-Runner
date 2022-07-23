using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public bool paintST;
    public bool followST;

    [SerializeField] GameObject player;
    [SerializeField] GameObject paintHolder;
    private void LateUpdate()
    {
        if (paintST)
            PaintState();
        if (followST)
            FollowState();
            
    }
    void FollowState()
    {
        transform.position = Vector3.Lerp(transform.position, 
            new Vector3((Mathf.Clamp(player.transform.position.x, -5, 5)), 
            player.transform.position.y + 7f, player.transform.position.z - 5.5f),
            7 * Time.deltaTime);
    }

    void PaintState()
    {
        gameObject.GetComponent<Camera>().fieldOfView = Mathf.Lerp(90, 60, .001f);
        transform.position = Vector3.Lerp(transform.position, paintHolder.transform.position, Time.deltaTime * 2);
        transform.rotation = paintHolder.transform.rotation;             
    }
}
