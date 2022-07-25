using System.Collections;
using UnityEngine;

public class ObstaclesTagManager : MonoBehaviour
{

    protected bool isPush;
    protected bool isHit;

    [SerializeField] protected GameObject boy;
    [SerializeField] protected Transform spawnPoint;
    private void OnCollisionEnter(Collision collision)
    {

        if (gameObject.CompareTag("Obstacles"))
            isHit = true;
        if (gameObject.CompareTag("Dynamic"))
            isPush = true;


        if(isPush)
        {
            if (collision.gameObject.CompareTag("Player"))
                Push();
        }
            
        if (isHit)
        {
            if (collision.gameObject.CompareTag("Player"))
                Kill();
        }
    }

    public void Kill()
    {
        boy.transform.position = spawnPoint.position;
        boy.GetComponent<PlayerScript>().HitObstacles();
        // RESPAWND THE PLAYER
    }
    public void Push()
    {
        boy.GetComponent<PlayerScript>().PushPlayer();
        // PUSH THE PLAYER
    }
}
