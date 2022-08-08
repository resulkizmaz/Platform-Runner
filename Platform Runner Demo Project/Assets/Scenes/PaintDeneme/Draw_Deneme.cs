using UnityEngine;

public class Draw_Deneme : MonoBehaviour
{
    public Camera mainCamera;
    public GameObject paintBrush;
    
    LineRenderer _currentLineRenderer;

    Vector2 _lastPos;

    private void Update()
    {
        Draw();
    }

    void Draw()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
            CreateBrush();


        if (Input.GetKey(KeyCode.Mouse0))
        {
            Vector2 mousePos = mainCamera.ScreenToWorldPoint(Input.mousePosition);

            if (mousePos!=_lastPos)
            {
                AddPoint(mousePos);
                _lastPos = mousePos;
            }

        }
        else
        {
            _currentLineRenderer = null;
        }
    }

    void CreateBrush()
    {
        GameObject brushInstiate = Instantiate(paintBrush);
        _currentLineRenderer = brushInstiate.GetComponent<LineRenderer>();

        Vector2 mousePos = mainCamera.ScreenToWorldPoint(Input.mousePosition);

        _currentLineRenderer.SetPosition(0, mousePos);
        _currentLineRenderer.SetPosition(1, mousePos);
    }

    void AddPoint(Vector2 pointPos)
    {
        _currentLineRenderer.positionCount++;
        int positionIndex = _currentLineRenderer.positionCount - 1;
        _currentLineRenderer.SetPosition(positionIndex, pointPos);
    }

}
