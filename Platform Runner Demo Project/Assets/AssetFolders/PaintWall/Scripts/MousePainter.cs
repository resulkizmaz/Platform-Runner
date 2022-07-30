using UnityEngine;

public class MousePainter : MonoBehaviour
{
    public Camera cam;
    [Space]
    public bool mouseSingleClick;
    [Space]
    public Color paintColor;

    public float radius = 1;
    public float strength = 1;
    public float hardness = 1;
    [SerializeField] private float energyAmount = 10f;

    public float PaintEnergy { get { return paintEnergy; } set { paintEnergy = value; } }
    public bool IsClick { get { return isClick; } set { isClick = value; } }

    private GameManager _gameManager;

    private float paintEnergy = 100f;
    private bool isClick = true;

    private void Awake()
    {
        _gameManager = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameManager>();
    }

    void Update()
    {

        bool click;
        click = mouseSingleClick ? Input.GetMouseButtonDown(0) : Input.GetMouseButton(0);

        if (click && isClick)
        {
            Vector3 position = Input.mousePosition;
            Ray ray = cam.ScreenPointToRay(position);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, 100.0f))
            {
                Debug.DrawRay(ray.origin, hit.point - ray.origin, Color.red);
                transform.position = hit.point;
                Paintable p = hit.collider.GetComponent<Paintable>();
                if (p != null)
                {
                    PaintManager.instance.paint(p, hit.point, radius, hardness, strength, paintColor);
                    SetPaintEnergy(energyAmount);
                }
            }
        }

    }

    private void SetPaintEnergy(float decrementAmount)
    {
        int energy = (int)_gameManager.paintEnergy;
        _gameManager.uIManager.paintEnergyText.text = energy.ToString();

        if (_gameManager.paintEnergy > 0f)
        {
            _gameManager.paintEnergy -= decrementAmount * Time.deltaTime;
        }
        else
        {
            isClick = false;
            _gameManager.SetUI(UIManager.UIState.Win);
        }

    }

}
