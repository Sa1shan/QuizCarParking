using System;
using UnityEngine;
using UnityEngine.EventSystems;

namespace _Source.Button
{
    [RequireComponent(typeof(Rigidbody))]
    public class Clickableobject : MonoBehaviour, IPointerClickHandler
    {
        [SerializeField] private GameObject car;
        [SerializeField] private Transform point;
        [SerializeField] private float speed = 1f;
        
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
            }
        }
    }
}
