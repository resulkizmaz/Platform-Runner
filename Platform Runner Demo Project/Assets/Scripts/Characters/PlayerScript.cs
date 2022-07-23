using System.Collections;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{

    [SerializeField] 
    protected Transform spawnPoint;
    protected Rigidbody _rigidBody;
    
    protected int second;
    float timer;
    
    private void Awake()
    {
        _rigidBody = GetComponent<Rigidbody>();
    }
    void Update()
    {
        //Timer();
    }
    public void Timer()
    {
        timer += Time.deltaTime;
        second = Mathf.RoundToInt(timer);
    }
    
}
