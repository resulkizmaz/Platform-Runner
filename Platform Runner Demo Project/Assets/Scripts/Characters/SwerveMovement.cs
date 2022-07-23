using System.Collections;
using UnityEngine;

public class SwerveMovement : PlayerScript
{
    [SerializeField] float moveSpeed;
    [SerializeField] GameObject _camera;

    Animator _anim;
    CameraFollow _camFollow;

    bool canRun;
    private void Awake()
    {
        _camFollow = _camera.GetComponent<CameraFollow>();
        _anim = GetComponent<Animator>();

        StartCoroutine(RunBoy());
    }
    private void Update()
    {
        RunPlayer();
    }

   
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Obstacles"))
            HitObstacles();       
        if (collision.gameObject.CompareTag("Donut"))
            PushBack();
        if (collision.gameObject.CompareTag("PaintTrigger"))
            StartCoroutine(PaintBoy());
    }
    void RunPlayer()
    {
        if (canRun)
        {
            transform.Translate(Vector3.forward * moveSpeed * Time.deltaTime);
        }
    }

    public void PushBack()
    {

    }
    public void HitObstacles()
    {
        transform.position = spawnPoint.position;
        _anim.SetTrigger("idleBoy");
        StartCoroutine(RunBoy());
    }

    IEnumerator RunBoy()
    {
        canRun = false;
        _camFollow.followST = true;

        yield return new WaitForSeconds(.5f);

        _anim.SetTrigger("runBoy");
        canRun = true;


        StopCoroutine(RunBoy());
    }
    IEnumerator PaintBoy()
    {
        canRun = false;
        _camFollow.followST = false;
        _anim.SetTrigger("idleBoy");

        yield return new WaitForSeconds(1f);
        _camFollow.paintST = true;

        StopCoroutine(PaintBoy());
    }
    
}
