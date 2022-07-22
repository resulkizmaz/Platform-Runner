using System.Collections;
using UnityEngine;

public class Player : MonoBehaviour
{

    //COROUTÝNE'LERÝ DÜZENLE!!!!!!




    [SerializeField] GameObject _camera;
    [SerializeField] Transform playerSpawn;
    [SerializeField] float playerMoveSpeed;
    [SerializeField] float cameraFollowSpeed;

    Rigidbody _rigidBody;
    Animator _anim;
    bool canRun;
    bool camFollow;
    float timer;
    int second;

    private void Awake()
    {
        _anim = GetComponent<Animator>();
        _rigidBody = GetComponent<Rigidbody>();

        StartCoroutine(RunBoy());
        
    }
    void Update()
    {
        Timer();
        RunPlayer();
    }
    private void LateUpdate()
    {
        CameraFollow();
    }
    void RunPlayer()
    {
        if (canRun)
        {
            //_rigidBody.velocity = transform.forward * playerMoveSpeed * Time.deltaTime;
            transform.position += transform.forward * playerMoveSpeed * Time.deltaTime;
        }        
    }


    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Obstacles"))
        {
            transform.position = playerSpawn.position;
            StartCoroutine(RunBoy());
        }
        if (collision.gameObject.CompareTag("Donut"))
            PushBack();
        if (collision.gameObject.CompareTag("PaintTrigger"))
            StartCoroutine(PaintBoy());

    }

    public void Timer()
    {
        timer += Time.deltaTime;
        second = Mathf.RoundToInt(timer);
    }
    void PushBack()
    {

    }
    void PaintTrigger()
    {
        
        
    }
    void CameraFollow()
    {
        if (camFollow)
        {
            _camera.transform.position = Vector3.Lerp(_camera.transform.position,
                                    new Vector3((Mathf.Clamp(transform.position.x, -5, 5)),
                                    transform.position.y + 7f, transform.position.z - 5.5f),
                                    cameraFollowSpeed * Time.deltaTime);
        }
    }

    IEnumerator RunBoy()
    {
        canRun = false;
        camFollow = true;
        _anim.SetTrigger("idleBoy");

        yield return new WaitForSeconds(1f);

        _anim.SetTrigger("runBoy");
        canRun = true;


        StopCoroutine(RunBoy());
    }
    IEnumerator PaintBoy()
    {
        canRun = false;
        camFollow = false;
        _anim.SetTrigger("idleBoy");

        yield return new WaitForSeconds(1f);

        _camera.transform.position = Vector3.Lerp(_camera.transform.position,
                                     new Vector3(),
                                     cameraFollowSpeed * Time.deltaTime);

        _camera.GetComponent<Camera>().fieldOfView = Mathf.Lerp(60, 110, cameraFollowSpeed * Time.deltaTime);


        StopCoroutine(PaintBoy());
    }
}
