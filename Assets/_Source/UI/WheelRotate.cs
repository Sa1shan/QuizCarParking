using System;
using UnityEngine;

namespace _Source.UI
{
    public class WheelRotate : MonoBehaviour
    {
        public float turnSpeed = 5f; 
        public float turnSmoothing = 5f;

        private float screenCenterX;
        private float currentTurnInput;

        void Start()
        {
            screenCenterX = Screen.width * 0.5f;
        }

        void Update()
        {
            HandleSteering();
        }

        void HandleSteering()
        {
            float mouseX = Input.mousePosition.x;
            float targetTurnInput = (mouseX - screenCenterX) / screenCenterX; 
            
            currentTurnInput = Mathf.Lerp(currentTurnInput, targetTurnInput, Time.deltaTime * turnSmoothing);
            
            float clampedTurnInput = Mathf.Clamp(currentTurnInput, -1f, 1f);
            
            float rotationAmount = clampedTurnInput * turnSpeed * Time.deltaTime;
            
            transform.rotation *= Quaternion.Euler(0, 0, -rotationAmount);
        }
    }
}
