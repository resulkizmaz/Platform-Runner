using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class GameManager : MonoBehaviour
{
    #region Inspector Variables

    [Header("Reference")]
    [SerializeField] private GameObject _cameraObject;
    [SerializeField] private GameObject _painterManager;
    [SerializeField] private GameObject _paintWall;
    [SerializeField] private GameObject _paintBar;
    [SerializeField] private Transform _startPoint;
    [SerializeField] private Transform _paintPoint;
    [SerializeField] private ParticleSystem _finishParticle;
    [Header("Settings")]
    [SerializeField] private float _cameraTime = 2f;
    public float paintEnergy = 100f;

    [Space(10f)]
    [SerializeField] private List<GameObject> _levels;
    [Header("UI")]
    [SerializeField] public UIManager uIManager;
    [Header("AI")]
    [SerializeField] private List<Transform> _characters;

    #endregion

    [HideInInspector] public bool isDestination = false;
    [HideInInspector] public bool isclick = false;

    #region Private Variables

    private GameObject _player;
    private PlayerController _playerController;

    private int _currentLevel = 1;

    #endregion

    public Transform StartPoint => _startPoint;

    private void Start()
    {
        _player = GameObject.FindGameObjectWithTag("Player");
        _playerController = _player.GetComponent<PlayerController>();

        LevelProperties(_currentLevel);

        _paintBar.SetActive(false);
    }

    //********UPDATE********
    private void Update()
    {
        _characters.Sort(CharacterSorting);
        int characterNumber = _characters.Count - _characters.IndexOf(_player.transform);
        uIManager.sortingText.text = characterNumber.ToString();
    }

    #region Character Management

    //Player restart function
    public void RestartCharacter()
    {
        _playerController.SetMovement(false); //Player movement is stopped
        _player.transform.position = _startPoint.position; //Player position is replaced by the starting position
        _cameraObject.GetComponent<CameraFollow>().enabled = true; //The camera follows the player
        _playerController.isPlayerActive = true; //The player is made movable
        SetUI(UIManager.UIState.Begin); //Startup ui is set
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
        StartCoroutine(CameraTimer(_paintPoint.position, _cameraTime));
    }

    //The function to be called when the level is completely finished
    public void FinishLevel()
    {
        if (_currentLevel < 2)
            _currentLevel++;
        else
        {
            _currentLevel = 1;
            paintEnergy = 100;
        }

        LevelProperties(_currentLevel);

        int count = 0;
        //level pass is provided
        foreach (GameObject item in _levels)
        {
            if (count == _currentLevel - 1)
                item.SetActive(true);
            else
                item.SetActive(false);

            _paintBar.SetActive(false);
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
                uIManager.paintingBar.transform.parent.gameObject.SetActive(true);
                uIManager.sortingPanel.SetActive(false);
                break;

            case 2:
                uIManager.paintingBar.transform.parent.gameObject.SetActive(false);
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

    public void PaintableAnim()
    {
        _paintWall.SetActive(true);
        _paintBar.SetActive(true);
    }



    #endregion

    #region Camera Management

    private void CameraTween(Vector3 endValue, float time)
    {
        _cameraObject.transform.DOMove(endValue, time).SetEase(Ease.Linear);
        _cameraObject.GetComponent<CameraFollow>().enabled = false;
    }

    IEnumerator CameraTimer(Vector3 endValue, float time)
    {
        yield return new WaitForSeconds(time);
        CameraTween(endValue, time);
        yield return new WaitForSeconds(time);
        _painterManager.SetActive(true);
    }

    #endregion

    public void SetUI(UIManager.UIState state)
    {
        uIManager.SetUIPanels(state);
    }

    private int CharacterSorting(Transform p1, Transform p2)
    {
        return p1.position.z.CompareTo(p2.position.z);
    }
}
