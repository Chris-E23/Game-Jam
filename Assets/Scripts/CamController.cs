using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CorruptElementary
{
    public class CamController : MonoBehaviour
    {
        public float mouseSens;
        private float _xRot;
        private float _yRot;

        public Transform orientation;

        void Update()
        {
            float mouseX = Input.GetAxisRaw("Mouse X") * Time.deltaTime * mouseSens;
            float mouseY = Input.GetAxisRaw("Mouse Y") * Time.deltaTime * mouseSens;

            _yRot += mouseX;
            _xRot -= mouseY;
            _xRot = Mathf.Clamp(_xRot, -90f, 90f);

            transform.rotation = Quaternion.Euler(_xRot, _yRot, 0);
            orientation.rotation = Quaternion.Euler(0, _yRot, 0);
        }
    }
}
