using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public class CameraController : MonoBehaviour 
{
    public float _sensitivity = 1f;
    protected Vector3? _mouseDownPosition;

    protected virtual void Update()
    {
        CatchMouseDown();
        CatchMouseUp();

        if (_mouseDownPosition == null) return;

        MoveCamera();
    }

    void CatchMouseDown()
    {
        if (Input.GetMouseButtonDown(0))
        {
            _mouseDownPosition = Input.mousePosition;
        }
    }

    void CatchMouseUp()
    {
        if (Input.GetMouseButtonUp(0))
        {
            _mouseDownPosition = null;
        }
    }

    void MoveCamera()
    {
        Vector3 diff = Input.mousePosition - _mouseDownPosition.Value;
        diff.z = diff.y;
        diff.y = 0;
        transform.position -= diff * Time.deltaTime * _sensitivity;
        _mouseDownPosition = Input.mousePosition;
    }
}
