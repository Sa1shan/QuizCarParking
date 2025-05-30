using UnityEngine;
using UnityEngine.UI;

namespace _Source.UI
{

    public class SeconMassageAnim : MonoBehaviour
    {
        [Header("Настройки анимации Second Massage")]
        [SerializeField] private Image firstImage;
        [SerializeField] private Image image; // Ссылка на Image
        [SerializeField] private float duration = 1f; // Длительность анимации
        [SerializeField] private Vector3 startPosition; // Начальная позиция вне экрана
        [SerializeField] private Vector3 endPosition; // Конечная позиция

        private bool _isAnimating = false;
        private float _elapsedTime = 0f;
        private bool _hasPlayedSecondMessage = false;

        private void Update()
        {
            if (!_hasPlayedSecondMessage && !firstImage.gameObject.activeSelf)
            {
                _hasPlayedSecondMessage = true;
                StartAnimation();
            }

            if (Input.GetKeyDown(KeyCode.T))
            {
                image.gameObject.SetActive(false);
            }

            if (_isAnimating)
            {
                AnimateImage();
            }
        }

        private void StartAnimation()
        {
            image.gameObject.SetActive(true);
            image.rectTransform.anchoredPosition = startPosition;
            _elapsedTime = 0f;
            _isAnimating = true;
        }

        private void AnimateImage()
        {
            _elapsedTime += Time.deltaTime;
            float t = Mathf.Clamp01(_elapsedTime / duration); // прогресс от 0 до 1
            image.rectTransform.anchoredPosition = Vector3.Lerp(startPosition, endPosition, t);

            if (t >= 1f)
            {
                _isAnimating = false;
                Debug.Log("Анимация завершена");
            }
        }
    }
}
