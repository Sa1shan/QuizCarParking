using System;
using UnityEngine;
using UnityEngine.UI;

namespace _Source.UI
{
    public class WheelRotation : MonoBehaviour
    {
        [SerializeField] private float maxRotationAngle = 45f; // Максимальный угол поворота колеса
        private RectTransform _rectTransform;
        private float _screenCenterX;

        private void Awake()
        {
            _rectTransform = GetComponent<RectTransform>();
            _screenCenterX = Screen.width / 2f;
        }

        private void Update()
        {
            // Получаем позицию мыши по X
            float mouseX = Input.mousePosition.x;

            // Вычисляем targetTurnInput от -1 до 1
            float targetTurnInput = (mouseX - _screenCenterX) / _screenCenterX;

            // Ограничиваем targetTurnInput на всякий случай
            targetTurnInput = Mathf.Clamp(targetTurnInput, -1f, 1f);

            // Вычисляем угол поворота (например, maxRotationAngle = 45 градусов)
            float rotationZ = targetTurnInput * maxRotationAngle;

            // Применяем вращение к RectTransform
            _rectTransform.rotation = Quaternion.Euler(0f, 0f, -rotationZ);
        }
    }
}
