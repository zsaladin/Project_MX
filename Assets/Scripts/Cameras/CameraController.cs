using UnityEngine;
using System.Collections.Generic;
using System.Linq;

namespace MX
{
    public class CameraController : MonoBehaviour
    {
        public float _sensitivity = 1f;
        public float _height = 13.7f;

        protected Vector3? _mouseDownPosition;

        public BattleActor PossessedActor { get; set; }

        protected virtual void Update()
        {
            if (PossessedActor == null)
            {
                CatchMouseDown();
                CatchMouseUp();

                if (_mouseDownPosition == null) return;

                MoveCamera();
            }
            else
            {
                Vector3 targetPos = PossessedActor.transform.position - PossessedActor.transform.forward * 3 + Vector3.up * 3;
                transform.position = Vector3.MoveTowards(transform.position, targetPos, 20 * Time.deltaTime);

                Quaternion original = transform.rotation;
                transform.LookAt(PossessedActor.transform);
                transform.rotation = Quaternion.RotateTowards(original, transform.rotation, 20 * Time.deltaTime);
                Debug.Log(GetComponent<Camera>().projectionMatrix);
            }
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
}