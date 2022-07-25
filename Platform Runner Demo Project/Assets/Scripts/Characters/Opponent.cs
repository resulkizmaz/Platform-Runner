using System.Collections;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent),typeof(AnimManager))]
public class Opponent : MonoBehaviour
{
    public Transform target;
    public Transform spawnPoint;
    public float updateSpeed;

    //private Transform target;
    private NavMeshAgent _agent;
    private AnimManager _animManager;
    private Rigidbody _rigidBody;

    //float randomX = Random.Range(-3f,3f);
    private void Awake()
    {
        _agent = GetComponent<NavMeshAgent>();
        _animManager = GetComponent<AnimManager>();
        _rigidBody = GetComponent<Rigidbody>();
    }
    private void Start()
    {
        //target.position = new Vector3(randomX, _target.position.y, _target.position.z);
        StartCoroutine(FollowTarget());
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
            _agent.speed = 7f;
        if (collision.gameObject.CompareTag("Opponent"))
            _agent.speed = 7f;
        if (collision.gameObject.CompareTag("Dynamic"))
            StartCoroutine(PushAI());        
        if (collision.gameObject.CompareTag("Obstacles"))
            StartCoroutine(ReSpawnAI());

    }
    private void OnCollisionExit(Collision collision) // Intract Mechanic : When people touch each other the speed value decreases.
    {
        if (collision.gameObject.CompareTag("Player"))
            _agent.speed = 9f;
        if (collision.gameObject.CompareTag("Opponent"))
            _agent.speed = 9f;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("EndPoint"))
            StartCoroutine(VictoryState());
    }

    private IEnumerator FollowTarget()
    {
        yield return new WaitForSeconds(updateSpeed);
        _agent.SetDestination(target.position);
        _animManager.AnimStates(AnimManager.States.run);

        StopCoroutine(FollowTarget());
    }
    private IEnumerator VictoryState()
    {
        _agent.enabled = false;
        _rigidBody.constraints = RigidbodyConstraints.FreezeRotation;

        yield return new WaitForEndOfFrame();
        _animManager.AnimStates(AnimManager.States.victory);

        StopCoroutine(VictoryState());
        StopCoroutine(FollowTarget());
    }

    private IEnumerator ReSpawnAI()
    {
        gameObject.transform.position = spawnPoint.position;
        yield return new WaitForSeconds(.5f);

        StopCoroutine(ReSpawnAI());
        StartCoroutine(FollowTarget());
    }

    private IEnumerator PushAI()
    {
        yield return new WaitForEndOfFrame();
    }

    
}
