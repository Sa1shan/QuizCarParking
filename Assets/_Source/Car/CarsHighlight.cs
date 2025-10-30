using UnityEngine;
using DG.Tweening;
namespace _Source.Tutorial
{
    public class CarsHighlight : MonoBehaviour
    {
        [SerializeField] private MeshRenderer meshRenderer;
        [SerializeField] private Color highlightColor = Color.yellow;
        [SerializeField] private float duration = 1f;

        private Material _material;
        private Color _originalColor;
        private Tween _colorTween;

        private void Start()
        {
            if (meshRenderer == null)
            {
                meshRenderer = GetComponent<MeshRenderer>();
            }

            if (meshRenderer != null)
            {
                _material = meshRenderer.material; // Используем копию материала, чтобы менять только этот объект
                _originalColor = _material.color;

                _colorTween = _material.DOColor(highlightColor, "_Color", duration)
                    .SetLoops(-1, LoopType.Yoyo)
                    .SetEase(Ease.InOutSine)
                    .OnKill(() =>
                    {
                        _material.color = _originalColor;
                    });
                
            }
        }

        internal void StopAnimation()
        {
            if (_colorTween != null && _colorTween.IsActive())
            {
                _colorTween.Kill();
            }
        }
    }
}
