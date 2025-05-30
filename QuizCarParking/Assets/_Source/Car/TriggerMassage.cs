using System;
using UnityEngine;
using UnityEngine.UI;

namespace _Source.Car
{
    public class TriggerMassage : MonoBehaviour
    {
        [SerializeField] private Image massage;
        [SerializeField] private new GameObject camera;
        
        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("MainCar"))
            {
                massage.gameObject.SetActive(true);
                Debug.Log("Enter");
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.CompareTag("MainCar"))
            {
                massage.gameObject.SetActive(false);
            }
        }

        void Update()
        {
            if (camera.gameObject.activeSelf)
            {
                massage.gameObject.SetActive(false);
            }
        }
    }
}
