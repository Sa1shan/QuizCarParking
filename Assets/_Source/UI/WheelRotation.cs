using System;
using UnityEngine;
using UnityEngine.UI;

namespace _Source.UI
{
    public class WheelRotation : MonoBehaviour
    {
        [SerializeField] private float maxRotationAngle = 45f; 
        private RectTransform _rectTransform;
        private float _screenCenterX;

        private void Awake()
        {
            _rectTransform = GetComponent<RectTransform>();
            _screenCenterX = Screen.width / 2f;
        }

        private void Update()
        {
            float mouseX = Input.mousePosition.x;
            
            float targetTurnInput = (mouseX - _screenCenterX) / _screenCenterX;
            
            targetTurnInput = Mathf.Clamp(targetTurnInput, -1f, 1f);
            
            float rotationZ = targetTurnInput * maxRotationAngle;
            
            _rectTransform.rotation = Quaternion.Euler(0f, 0f, -rotationZ);
        }
    }
}
