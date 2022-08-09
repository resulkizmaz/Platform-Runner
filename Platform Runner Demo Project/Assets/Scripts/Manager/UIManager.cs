using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    public enum UIState { Gameplay, Win, Restart, Settings, Begin, Pause };

    //[Header("Other Reference")]
    //[SerializeField] private MousePainter mousePainter;
    //[Space(10)]
    //public Image paintingBar;
    //public Text paintEnergyText;
    public GameObject sortingPanel;
    public Text sortingText;
    [Header("Panels")]
    [SerializeField] private GameObject _gameplayPanel;
    [SerializeField] private GameObject _winPanel;
    [SerializeField] private GameObject _pausePanel;
    [SerializeField] private GameObject _settingsPanel;
    public GameObject beginPanel;

    private GameManager _gameManager;


    private void Start()
    {
        _gameManager = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameManager>();
    }

    public void SetUIPanels(UIState state)
    {
        switch (state)
        {
            case UIState.Gameplay:
                _gameplayPanel.SetActive(true);
                _winPanel.SetActive(false);
                beginPanel.SetActive(false);
                _pausePanel.SetActive(false);
                _settingsPanel.SetActive(false);
                break;

            case UIState.Win:
                _winPanel.SetActive(true);
                _gameplayPanel.SetActive(false);
                beginPanel.SetActive(false);
                _settingsPanel.SetActive(false);
                _pausePanel.SetActive(false);
                break;

            case UIState.Begin:
                _winPanel.SetActive(false);
                _gameplayPanel.SetActive(false);
                beginPanel.SetActive(true);
                _pausePanel.SetActive(false);
                _settingsPanel.SetActive(false);
                break;

            case UIState.Pause:
                _winPanel.SetActive(false);
                _gameplayPanel.SetActive(false);
                beginPanel.SetActive(false);
                _settingsPanel.SetActive(false);
                _pausePanel.SetActive(true);
                break;


            case UIState.Settings:
                _winPanel.SetActive(false);
                _gameplayPanel.SetActive(false);
                beginPanel.SetActive(false);
                _pausePanel.SetActive(true);
                _settingsPanel.SetActive(true);
                break;

            case UIState.Restart:
                SceneManager.LoadScene(0);
                break;
        }
    }

    public void OnPausePanel()
    {
        Time.timeScale = 0f;
        SetUIPanels(UIState.Pause);
    }

    public void ClosePausePanel()
    {
        Time.timeScale = 1f;
        SetUIPanels(UIState.Gameplay);
    }
    public void SettingsPanel() // Quality Settings ( Project Settings - Quality )
    {
        SetUIPanels(UIState.Settings);
    }
    public void CloseSettingsPanel()
    {
        SetUIPanels(UIState.Pause);
    }
    public void Low()
    {

        QualitySettings.SetQualityLevel(1);

        CloseSettingsPanel();
    }
    public void Medium()
    {

        QualitySettings.SetQualityLevel(2);

        CloseSettingsPanel();
    }
    public void High()
    {

        QualitySettings.SetQualityLevel(3);

        CloseSettingsPanel();
    }

    public void Restart()
    {
        SetUIPanels(UIState.Restart);
        Time.timeScale = 1f;
    }

    public void Quit()
    {
        Application.Quit();
    }
}
