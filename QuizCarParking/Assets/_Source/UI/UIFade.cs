using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using System.Collections.Generic;

namespace _Source.UI
{
    public class UIFade : MonoBehaviour
    {
        [SerializeField] private Image firstImage;
        [SerializeField] private List<Image> images;
        [SerializeField] private UnityEngine.Camera cam;

        private void Start()
        {
            // Сначала делаем все изображения из списка прозрачными и активными
            foreach (var img in images)
            {
                img.gameObject.SetActive(false);
            }
        }

        private void Update()
        {
            if (!firstImage.gameObject.activeSelf)
            {
                foreach (var img in images)
                {
                    img.gameObject.SetActive(true);
                }
            }

            if (cam.gameObject.activeSelf)
            {
                foreach (var image in images)
                {
                    image.gameObject.SetActive(false);
                }
            }
        }
    }
}
