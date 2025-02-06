using System;
using UnityEngine;

namespace _Source.UI
{
    public class WheelRotate : MonoBehaviour
    {
        public float turnSpeed = 5f; // Скорость поворота
        public float turnSmoothing = 5f; // Плавность поворота

        private float screenCenterX;
        private float currentTurnInput;

        void Start()
        {
            screenCenterX = Screen.width * 0.5f; // Находим центр экрана по X
        }

        void Update()
        {
            HandleSteering();
        }

        void HandleSteering()
        {
            float mouseX = Input.mousePosition.x;
            float targetTurnInput = (mouseX - screenCenterX) / screenCenterX; // Получаем значение от -1 до 1

            // Плавно интерполируем входной поворот
            currentTurnInput = Mathf.Lerp(currentTurnInput, targetTurnInput, Time.deltaTime * turnSmoothing);

            // Ограничиваем диапазон значений
            float clampedTurnInput = Mathf.Clamp(currentTurnInput, -1f, 1f);

            // Вычисляем угол поворота
            float rotationAmount = clampedTurnInput * turnSpeed * Time.deltaTime;

            // Применяем поворот вокруг оси Y
            transform.rotation *= Quaternion.Euler(0, 0, -rotationAmount);
        }
    }
}
