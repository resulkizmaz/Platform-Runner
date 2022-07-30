using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class AIController : MonoBehaviour
{
    #region Inspector Variables

    [SerializeField] private Transform target;
    [SerializeField] private float restartTime = 1f;
    [Header("Ground Check")]
    [Range(0.1f, 1f)]
    [SerializeField] private float groundCheckAmount = 0.5f;
    [SerializeField] private LayerMask checkLayerMask;

    #endregion

    #region Private Variables

    //Components
    private NavMeshAgent _agent;
    private Rigidbody _rb;
    private AnimManager _animManager;

    private GameManager _gameManager;

    private Vector3 _startPosition;

    private bool _isHit;
    private bool _isGrounded;
    private bool _start = true;

    #endregion

    public bool IsHit => _isHit;



    private void Start()
    {
        _agent = GetComponent<NavMeshAgent>();
        _gameManager = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameManager>();
        _animManager = GetComponent<AnimManager>();
        _rb = GetComponent<Rigidbody>();
        _animManager.SetAnimationState(AnimManager.AnimationStates.Idle);

        _startPosition = transform.position;
    }


    private void Update()
    {
        if (_gameManager.isDestination)
        {
            if (_agent.enabled == true)
                _agent.SetDestination(target.position);
            if (_start)
            {
                _animManager.SetAnimationState(AnimManager.AnimationStates.Run);
                _start = false;
            }
        }

        //Is the AI in contact with the ground
        _isGrounded = Physics.CheckSphere(transform.position, groundCheckAmount, checkLayerMask);
    }

    //AI restart
    public void RestartAI()
    {
        _animManager.SetAnimationState(AnimManager.AnimationStates.Idle);
        _agent.enabled = false; //Navmesh agent component is turned off
        _gameManager.isDestination = false;
        transform.position = _startPosition;
        StartCoroutine(MoveTimer());
    }

    IEnumerator MoveTimer()
    {
        yield return new WaitForSeconds(restartTime);
        _agent.enabled = true;
        _gameManager.isDestination = true;

        _animManager.SetAnimationState(AnimManager.AnimationStates.Run);
    }

    public void Hit(Vector3 velocityF, float time)
    {
        print("Bounce AI");
        _isHit = true;
        _rb.constraints = RigidbodyConstraints.FreezeRotation;
        _gameManager.isDestination = false;
        _agent.enabled = false;
        _rb.isKinematic = false;
        _rb.AddForce(velocityF * 5000f, ForceMode.Impulse);
        StartCoroutine(HitDecrease(time + 1f));
    }

    IEnumerator HitDecrease(float time)
    {
        yield return new WaitForSeconds(time);
        if (_isGrounded)
        {
            _rb.isKinematic = true;
            _gameManager.isDestination = true;
            _agent.enabled = true;
        }
        else
        {
            _rb.isKinematic = false;
            _gameManager.isDestination = false;
            _agent.enabled = false;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Finish"))
        {
            _animManager.SetAnimationState(AnimManager.AnimationStates.Idle);
            _agent.enabled = false;
            _gameManager.isDestination = false;
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.CompareTag("AI") || collision.transform.CompareTag("Player")) 
            _agent.speed = 8f;
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.transform.CompareTag("AI") || collision.transform.CompareTag("Player")) 
            _agent.speed = 10f;
    }

    private void OnDrawGizmos()
    {
        //GroundCheck sphere visualization
        Gizmos.color = Color.green;
        Gizmos.DrawSphere(transform.position, groundCheckAmount);
    }


}
