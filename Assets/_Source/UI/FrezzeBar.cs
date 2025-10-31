using System;
using UnityEngine;
using UnityEngine.UI;

namespace _Source.UI
{
    public class FrezzeBar : MonoBehaviour
    {
        [SerializeField] private Image image;
        [SerializeField] private GameObject camera;

        private void Start()
        {
            image.gameObject.SetActive(false);
        }
        void Update()
        {
            if (camera.gameObject.activeSelf)
            {
                image.gameObject.SetActive(true);
            }
        }
    }
}
