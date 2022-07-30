using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public enum UIState { Gameplay, Win, Lose, Settings, Begin, Pause };

    [Header("Other Reference")]
    [SerializeField] private MousePainter mousePainter;
    [Space(10)]
    public Image paintingBar;
    public GameObject sortingPanel;
    public Text sortingText;
    public Text paintEnergyText;
    [Header("Panels")]
    [SerializeField] private GameObject gameplayPanel;
    [SerializeField] private GameObject winPanel;
    [SerializeField] private GameObject pausePanel;
    public GameObject beginPanel;

    private GameManager _gameManager;


    private void Start()
    {
        _gameManager = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameManager>();

        if (mousePainter != null)
            paintingBar.fillAmount = mousePainter.PaintEnergy / 100f;
    }

    private void Update()
    {
        if (mousePainter.IsClick && mousePainter != null) // While we painting the wall the energy decreases. (Visually)
            paintingBar.fillAmount = _gameManager.paintEnergy / 100f;
    }

    public void SetUIPanels(UIState state)
    {
        switch (state)
        {
            case UIState.Gameplay:
                gameplayPanel.SetActive(true);
                winPanel.SetActive(false);
                beginPanel.SetActive(false);
                pausePanel.SetActive(false);
                break;

            case UIState.Win:
                winPanel.SetActive(true);
                gameplayPanel.SetActive(false);
                beginPanel.SetActive(false);
                pausePanel.SetActive(false);
                break;

            case UIState.Begin:
                winPanel.SetActive(false);
                gameplayPanel.SetActive(false);
                beginPanel.SetActive(true);
                pausePanel.SetActive(false);
                break;

            case UIState.Pause:
                winPanel.SetActive(false);
                gameplayPanel.SetActive(false);
                beginPanel.SetActive(false);
                pausePanel.SetActive(true);
                break;

            case UIState.Lose:
                break;

            case UIState.Settings:
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

    public void Restart()
    {
        _gameManager.RestartCharacter();
    }

    public void Quit()
    {
        Application.Quit();
    }
}
