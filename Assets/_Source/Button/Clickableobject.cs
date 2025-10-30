using System;
using _Source.Tutorial;
using UnityEngine;
using UnityEngine.EventSystems;

namespace _Source.Button
{
    [RequireComponent(typeof(Rigidbody))]
    public class Clickableobject : MonoBehaviour, IPointerClickHandler
    {
        [SerializeField] private GameObject car;
        [SerializeField] private Transform point;
        [SerializeField] private float speed = 80f;
        [SerializeField] private CarsHighlight highlight;
        
        private bool _clicked;
        private Rigidbody _rb;
        
        public void OnPointerClick(PointerEventData eventData)
        {
            _clicked = true;
        }

        private void Start()
        {
            _rb = GetComponent<Rigidbody>();
        }
        
        void FixedUpdate()
        {
            if (_clicked)
            {
                Vector3 newPosition = new Vector3(_rb.position.x, point.position.y, Mathf.MoveTowards(_rb.position.z, point.position.z, speed * Time.deltaTime));
                _rb.MovePosition(newPosition);
                highlight.StopAnimation();
            }

            if (Mathf.Approximately(car.transform.position.z, point.position.z))
            {
                _rb.constraints = RigidbodyConstraints.FreezeAll;
            }
        }
    }
}
