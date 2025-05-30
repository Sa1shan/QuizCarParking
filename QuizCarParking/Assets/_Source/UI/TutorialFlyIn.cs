using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

namespace _Source.UI
{
    public class TutorialFlyIn : MonoBehaviour
    {
        [Header("Настройки анимации First Massage")] 
        [SerializeField] private Image image; // Ссылка на Image
        [SerializeField] private float duration = 1f; // Длительность анимации
        [SerializeField] private Vector3 startPosition; // Начальная позиция вне экрана
        [SerializeField] private Vector3 endPosition; // Конечная позиция
        
        private Tween _tween;

        void Start()
        { 
            image.gameObject.SetActive(true);
        }

        void Update()
        {
            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                FirstMassgeFlyOut();
            }
        }
        
        void FirstMassgeFlyOut()
        {
            if (_tween == null || !_tween.IsPlaying())
            {
                _tween = image.rectTransform.DOAnchorPos(startPosition, duration)
                    .SetEase(Ease.InBack).OnComplete(() => image.gameObject.SetActive(false));
            }
        }
    }
}