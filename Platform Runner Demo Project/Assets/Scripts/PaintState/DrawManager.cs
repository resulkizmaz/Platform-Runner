using UnityEngine;

public class DrawManager : MonoBehaviour
{

    public GameObject drawBrush;
    [SerializeField] LayerMask layerMask;
    
    GameObject _theTrail;
    Camera _camera;
    Vector3 _startPos;

    private void Start()
    {
        _camera = Camera.main;
    }
    

    void Update()
    {
        Painting();
    }


    /* 
     * mouse position X : -8 --- +8 
     * mouse position Y :  0 --- +27.5 
     * z = 119
     */
    private void Painting()
    {
        Ray ray = _camera.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, 150f, layerMask) && (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began || Input.GetMouseButtonDown(0))) 
        {
            if (hit.collider.name == "PaintablePlane") 
            {
                _theTrail = (GameObject)Instantiate(drawBrush, hit.point, Quaternion.identity);
                _startPos = ray.GetPoint(hit.distance);
            }
        }


        if (Physics.Raycast(ray, out hit, 150f, layerMask) && (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Moved || Input.GetMouseButton(0))) 
        {
            if (hit.collider.name == "PaintablePlane")
                _theTrail.transform.position = ray.GetPoint(hit.distance - 0.001f);
                
        }
    }
}
