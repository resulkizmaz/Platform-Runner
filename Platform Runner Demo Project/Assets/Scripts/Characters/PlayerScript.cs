using System.Collections;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{

    [SerializeField] Transform spawnPoint,endPoint;
    [SerializeField] float forwardSpeed;
    [SerializeField] float sideSpeed;
    [SerializeField] GameObject _camera;

    Rigidbody rigidBody;
    CameraFollow camFollow;
    AnimManager animManager;

    bool canRun;
    int _second;
    float _timer;
    
    private void Awake()
    {
        rigidBody = GetComponent<Rigidbody>();
        camFollow = _camera.GetComponent<CameraFollow>();
        animManager = GetComponent<AnimManager>();
        StartCoroutine(RunBoy());
    }
    void Update()
    {
        RunPlayer();
        //Timer();
    }
    
   
    
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("PaintTrigger"))
            StartCoroutine(PaintBoy());
    }
    void RunPlayer()
    {
        

        if (canRun)
        {
            forwardSpeed = 10;
            transform.Translate(Vector3.forward * forwardSpeed * Time.deltaTime);
            animManager.AnimStates(AnimManager.States.run);
        }else
        {
            
            animManager.AnimStates(AnimManager.States.idle);
        }
        
    }

    public void HitObstacles()
    {
        StartCoroutine(RunBoy());
    }

    IEnumerator RunBoy()
    {
        canRun = false;
        camFollow.followST = true;

        yield return new WaitForSeconds(.5f);
        canRun = true;

        StopCoroutine(RunBoy());
    }
    IEnumerator PaintBoy()
    {
        canRun = false;
        camFollow.followST = false;

        yield return new WaitForSeconds(1f);
        camFollow.paintST = true;

        StopCoroutine(PaintBoy());
    }

    public void Timer()
    {
        _timer += Time.deltaTime;
        _second = Mathf.RoundToInt(_timer);
    }
}
