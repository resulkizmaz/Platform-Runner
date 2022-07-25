using System.Collections;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{

    [SerializeField] Transform spawnPoint,endPoint;
    [SerializeField] float forwardSpeed;
    [SerializeField] float sideSpeed;
    [SerializeField] GameObject _camera;

    Rigidbody _rigidBody;
    CameraFollow _camFollow;
    AnimManager _animManager;

    bool _canRun;
    bool _isInteract;
    int _second;
    float _timer;
    
    private void Awake()
    {
        _rigidBody = GetComponent<Rigidbody>();
        _camFollow = _camera.GetComponent<CameraFollow>();
        _animManager = GetComponent<AnimManager>();
        
    }
    private void Start()
    {
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
        if (collision.gameObject.CompareTag("Opponent"))
            forwardSpeed = 350f * Time.deltaTime; // I did it two times for use the "Mathf.Lerp" better.
    }

    private void OnCollisionExit(Collision collision) // Intract Mechanic : When people touch each other the speed value decreases.
    {
        if (collision.gameObject.CompareTag("Opponent"))
            forwardSpeed = 420f * Time.deltaTime;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("EndPoint"))
            StartCoroutine(VictoryState());
    }
    void RunPlayer()
    {
        if (_canRun)
        {
            transform.Translate(Vector3.forward * forwardSpeed * Time.deltaTime);
            _animManager.AnimStates(AnimManager.States.run);
        }   
        else
        {
            _animManager.AnimStates(AnimManager.States.idle);
        }
    }

    public void HitObstacles()
    {
        StartCoroutine(RunBoy());
    }
    public void PushPlayer()
    {

    }

    IEnumerator RunBoy()
    {
        _canRun = false;
        _camFollow.followST = true;

        yield return new WaitForSeconds(.5f);
        _canRun = true;

        StopCoroutine(RunBoy());
    }
    IEnumerator PaintBoy()
    {
        _canRun = false;
        _camFollow.followST = false;

        yield return new WaitForSeconds(1f);
        _camFollow.paintST = true;

        StopCoroutine(PaintBoy());
    }
    IEnumerator VictoryState()
    {
        _canRun = false;
        yield return new WaitForEndOfFrame();
        _animManager.AnimStates(AnimManager.States.victory);

        StopCoroutine(VictoryState());
    }

    public void Timer()
    {
        _timer += Time.deltaTime;
        _second = Mathf.RoundToInt(_timer);
    }
}
