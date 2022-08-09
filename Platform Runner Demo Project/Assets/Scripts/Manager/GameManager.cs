using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    #region Inspector Variables

    [Header("Reference")]
    [SerializeField] private GameObject _paintWall;
    [SerializeField] private GameObject _camera;
    [SerializeField] private GameObject _paintBrush;
    [SerializeField] private GameObject _paintDone;
    [SerializeField] private GameObject _paintText;
    [SerializeField] private Transform _startPoint;
    [SerializeField] private Transform _paintPoint;
    [SerializeField] private ParticleSystem _finishParticle;
    

    [Space(10f)]
    [SerializeField] private List<GameObject> _levels;
    [Header("UI")]
    [SerializeField] public UIManager uIManager;
    [Header("AI")]
    [SerializeField] private List<Transform> _characters;

    #endregion

    [HideInInspector] public bool isDestination = false;

    #region Private Variables

    private GameObject _player;
    private PlayerController _playerController;

    private int _currentLevel = 1;
    private bool _paintCam;

    #endregion

    public Transform StartPoint => _startPoint;

    private void Awake()
    {
        _player = GameObject.FindGameObjectWithTag("Player");
        _playerController = _player.GetComponent<PlayerController>();
    }

    private void Start()
    {
        SetUI(UIManager.UIState.Begin);

        LevelProperties(_currentLevel); // Handles to Level index (which level we are playing?)
    }

    //********UPDATE********
    private void Update()
    {
        _characters.Sort(CharacterSorting);
        int characterNumber = _characters.Count - _characters.IndexOf(_player.transform); // to check the sorting between each item in the list and the player.
        uIManager.sortingText.text = characterNumber.ToString(); // Shows player's rank
    }

    //*****FixedUpdate********
    private void LateUpdate()
    {
        if (_paintCam)
            _camera.transform.position = Vector3.Lerp(_camera.transform.position, _paintPoint.position, Time.fixedDeltaTime * 5f);
    }

    #region Character Management

    //Player restart function
    public void RestartCharacter()
    {
        _playerController.SetMovement(false); //Player movement is stopped
        _player.transform.position = _startPoint.position; //Player position is replaced by the starting position
        _camera.GetComponent<CameraFollow>().enabled = true; //The camera follows the player
        _playerController.isPlayerActive = true; //The player is made active
        _playerController.AnimManager.SetAnimationState(AnimManager.AnimationStates.Idle); // Set the animation.
        //SetUI(UIManager.UIState.Begin);
        
    }

    public void PlayerStop()
    {
        //Player movement is stopped
        _playerController.isPlayerActive = false;
        _playerController.SetMovement(false);
    }

    public void CharacterFinishAnim()
    {
        _playerController.AnimManager.SetAnimationState(AnimManager.AnimationStates.Win);
    }
    #endregion

    #region Level Management

    public void OnFinish()
    {
        _playerController.isPlayerActive = false;
        _playerController.SetMovement(false);
        _playerController.AnimManager.SetAnimationState(AnimManager.AnimationStates.Win);
    }

    public void FinishLevel()   //The function to be called when the level is completely finished
    {
        if (_currentLevel < 2)
            _currentLevel++;
        else
            _currentLevel = 1;

        LevelProperties(_currentLevel);


        //level pass is provided
        int count = 0;
        foreach (GameObject item in _levels)
        {
            if (count == _currentLevel - 1)
                item.SetActive(true);
            else
                item.SetActive(false);

            _paintWall.SetActive(false);
            count++;
        }

        RestartCharacter();
    }

    public void FinishParticle()
    {
        _finishParticle.Play();
    }

    private void LevelProperties(int levelIndex)
    {
        switch (levelIndex)
        {
            case 1:
                uIManager.sortingPanel.SetActive(false);
                break;

            case 2:
                uIManager.sortingPanel.SetActive(true);
                isDestination = false;

                break;

            default:
                break;
        }
    }

    public void FinishUI()
    {
        SetUI(UIManager.UIState.Win);
    }


    #endregion

    #region Paint Wall

    public void PaintState()
    {
        _paintWall.SetActive(true);
        _camera.GetComponent<CameraFollow>().enabled = false;
        PlayerStop();
        _paintText.SetActive(true);
        StartCoroutine(PaintCamera());
    }
    IEnumerator PaintCamera()
    {
        yield return new WaitForSeconds(2f);
        _paintCam = true;
        yield return new WaitForSeconds(1f);
        _paintText.SetActive(false);
        _paintBrush.SetActive(true);
        _paintDone.SetActive(true);

        StopCoroutine(PaintCamera());
    }
    public void PaintDone()
    {
        _paintDone.SetActive(false);
        _paintBrush.SetActive(false);
        _paintWall.SetActive(false);
        _paintCam = false;
        _camera.GetComponent<CameraFollow>().enabled = true;

        var clones = GameObject.FindGameObjectsWithTag("Clone"); // Destroyed created trails when level ends.
        foreach (var _clone in clones)
        {
            Destroy(_clone);
        }

        FinishUI();
    }

    #endregion
    
    public void SetUI(UIManager.UIState state) // Manages UI states.
    {
        uIManager.SetUIPanels(state);
    }

    private int CharacterSorting(Transform p1, Transform p2) // checking the z-axis oriented sorting. 
    {
        return p1.position.z.CompareTo(p2.position.z);
    }
}
