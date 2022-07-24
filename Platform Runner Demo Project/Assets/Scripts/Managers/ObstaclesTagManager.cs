using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstaclesTagManager : MonoBehaviour
{

    protected bool isPush;
    protected bool isHit;

    [SerializeField] protected GameObject _boy;
    [SerializeField] protected GameObject _girl;
    [SerializeField] protected Transform _spawnPoint;
    private void OnCollisionEnter(Collision collision)
    {

        if (gameObject.CompareTag("Obstacles"))
            isHit = true;
        if (gameObject.CompareTag("Dynamic"))
            isPush = true;


        if(isPush)
        {
            if (collision.gameObject.CompareTag("Player"))
                PushPlayer();
            if (collision.gameObject.CompareTag("Opponent"))
                PushOpponent();
        }
            
        if (isHit)
        {
            if (collision.gameObject.CompareTag("Player"))
                KillPlayer();
            if (collision.gameObject.CompareTag("Opponent"))
                collision.transform.position = _spawnPoint.position;
        }
    }

    public void KillPlayer()
    {
        _boy.transform.position = _spawnPoint.position;
        _boy.GetComponent<PlayerScript>().HitObstacles();
        // RESPAWND THE PLAYER
    }
    public void PushPlayer()
    {
        // PUSH THE PLAYER
    }
    
    public void PushOpponent()
    {
        // PUSH THE PLAYER
    }
}
