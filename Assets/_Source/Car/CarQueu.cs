using System;
using _Source.Button;
using UnityEngine;

namespace _Source.Car
{
    public class CarQueu : MonoBehaviour
    {
        [SerializeField] private GameObject car;
        [SerializeField] private Transform carTransform;
        
        private Clickableobject _clickableObject;

        private void Start()
        {
            _clickableObject = GetComponent<Clickableobject>();
        }

        void Update()
        {
            if (car.transform.position == carTransform.position)
            {
                _clickableObject.enabled = true;
            }
            else
            {
                _clickableObject.enabled = false;
            }
        }
        
    }
}