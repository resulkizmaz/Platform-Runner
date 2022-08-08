using System;
using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class AIController : MonoBehaviour
{
    #region Inspector Variables

    [SerializeField] private Transform target;
    [SerializeField] private float restartTime = 1f;
    [SerializeField] private float forcePower;
    [Header("Ground Check")]
    [Range(0.1f, 1f)]
    [SerializeField] private float groundCheckAmount = 0.5f;
    [SerializeField] private float groundRayAmount = 1;
    [SerializeField] private LayerMask checkLayerMask;

    #endregion

    #region Private Variables

    //Components
    private NavMeshAgent _agent;
    private Rigidbody _rb;
    private AnimManager _animManager;
    [SerializeField]
    private Rotator rotaterObject;
    private GameManager _gameManager;

    private Vector3 _startPosition;

    private bool _isHit;
    private bool _isGrounded;
    private bool _start = true;
    private bool _isPush;

    #endregion

    public bool IsHit => _isHit;

    private void Awake()
    {
        _agent = GetComponent<NavMeshAgent>();
        _gameManager = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameManager>();
        _animManager = GetComponent<AnimManager>();
        _rb = GetComponent<Rigidbody>();
        _animManager.SetAnimationState(AnimManager.AnimationStates.Idle);
        
    }

    private void Start()
    {
        _startPosition = transform.position;
    }


    private void Update()
    {
        //Is the AI in contact with the ground
        GroundCheck();

        if (_gameManager.isDestination)
        {
            if (_agent.enabled == true && _isGrounded)
                _agent.SetDestination(target.position);
            if (_start)
            {
                _animManager.SetAnimationState(AnimManager.AnimationStates.Run);
                _start = false;
            }
        }
    }

    private void GroundCheck()
    {
        _isGrounded = Physics.CheckSphere(transform.position, groundCheckAmount, checkLayerMask);

        RaycastHit hit;
        if (Physics.Raycast(transform.position, Vector3.down, out hit, groundRayAmount))
        {
            if (hit.transform.CompareTag("RotatingPlatform"))
            {
                rotaterObject = hit.transform.parent.GetComponent<Rotator>();
                _isPush = true;
            }
            else
                _isPush = false;
        }

        if (_isPush) //Is the character being pushed
        {
            //SetPush function is called
            SetPush(rotaterObject.p_Direction, rotaterObject.p_DirectionMultiplier, rotaterObject.p_Speed);
        }

    }

    private void SetPush(Rotator.Direction axis, int direction, float speed)
    {
        Vector3 directionAxis = new Vector3();
        switch (axis)
        {
            case Rotator.Direction.X_Axis:
                directionAxis = Vector3.up;
                break;

            case Rotator.Direction.Y_Axis:
                directionAxis = Vector3.forward;
                break;

            case Rotator.Direction.Z_Axis:
                directionAxis = Vector3.right;
                break;
        }

        _rb.AddForce(directionAxis * speed * 15f * -direction * Time.fixedDeltaTime, ForceMode.Force);
    }


    //AI restart
    public void RestartAI()
    {
        StartCoroutine(MoveTimer());
    }

    IEnumerator MoveTimer()
    {
        _animManager.SetAnimationState(AnimManager.AnimationStates.Idle);
        _agent.enabled = false; //Navmesh agent component is turned off
        _gameManager.isDestination = false;
        _rb.AddForce(Vector3.zero);
        transform.position = _startPosition;

        yield return new WaitForSeconds(restartTime);

        _agent.enabled = true;
        _gameManager.isDestination = true;

        _animManager.SetAnimationState(AnimManager.AnimationStates.Run);

        StopCoroutine(MoveTimer());
    }

    public void Hit(Vector3 velocityF, float time)
    {
        print("Bounce AI");
        _isHit = true;
        _rb.constraints = RigidbodyConstraints.FreezeRotation;
        _gameManager.isDestination = false;
        _agent.enabled = false;
        _rb.isKinematic = false;
        _rb.AddForce(velocityF * forcePower, ForceMode.Impulse);
        StartCoroutine(HitDecrease(time));
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
        yield return new WaitForSeconds(time);

        _rb.isKinematic = false;

        StopCoroutine(HitDecrease(time));
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
