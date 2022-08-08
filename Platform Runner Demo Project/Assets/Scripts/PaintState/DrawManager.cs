using UnityEngine;

public class DrawManager : MonoBehaviour
{

    public GameObject drawBrush;
    public LayerMask layerMask;

    GameObject _theTrail;
    Plane _planeObj;
    Vector3 _startPos;
    void Awake()
    {
        _planeObj = new Plane(Camera.main.transform.forward * -1, this.transform.position);
    }


/*
 * mouse position X :  -8 --- +8 
 * mouse position Y : +28 --- 0 
 * z = 119
 */
    void Update()
    {

        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began || Input.GetMouseButtonDown(0)) 
        {
            _theTrail = (GameObject)Instantiate(drawBrush, this.transform.position, Quaternion.identity);

            Ray mouseRay = Camera.main.ScreenPointToRay(Input.mousePosition);
            float _dis;
            if (_planeObj.Raycast(mouseRay,out _dis)) 
            {
                _startPos = mouseRay.GetPoint(_dis);
                float x = Mathf.Clamp(_startPos.x, -8f, 8f);
                float y = Mathf.Clamp(_startPos.y, 0, 28f);
                _startPos = new Vector3(x, y, 119);
            }
        }
        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Moved || Input.GetMouseButton(0)) 
        {
            Ray mouseRay = Camera.main.ScreenPointToRay(Input.mousePosition);
            float _dis;
            if (_planeObj.Raycast(mouseRay, out _dis)) 
            {
                _theTrail.transform.position = mouseRay.GetPoint(_dis);
                float x =Mathf.Clamp(_theTrail.transform.position.x, -8f, 8f);
                float y =Mathf.Clamp(_theTrail.transform.position.y, 0, 28f);
                _theTrail.transform.position = new Vector3(x, y, 119);
                
                Debug.Log(mouseRay.GetPoint(_dis));
            }
        }
    }
}
