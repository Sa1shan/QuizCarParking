using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace _Source.PlayerController
{
    public class PlayerController : MonoBehaviour
    {
        [Header("Movement Settings")]
        public float accelerationForce = 3000f;
        public float maxSpeed = 30f;
        public float turnTorque = 5000f;
        public float dragFactor = 0.98f;
        public float sidewaysFriction = 0.85f;
        public float turnSmoothing = 5f;
        public float minVelocityThreshold = 0.1f; 
        
        private Rigidbody _rb;

        void Start()
        {
            _rb = GetComponent<Rigidbody>();
            _rb.centerOfMass = new Vector3(0, -0.5f, 0);
        }

        void FixedUpdate()
        {
            HandleMovement();
            HandleSteering();
            ApplyDrag();
            ApplySidewaysFriction();
            StabilizeIdleState();
        }

        void HandleMovement()
        {
            float moveInput = Input.GetAxis("Vertical");
            
            if (moveInput > 0)
            {
                MoveForward(moveInput);
            }
            else if (moveInput < 0)
            {
                MoveBackward(moveInput);
            }
            else
            {
                ApplyBraking();
            }
        }
        
        void MoveForward(float moveInput)
        {
            if (_rb.velocity.magnitude < maxSpeed)
            {
                _rb.AddForce(transform.forward * moveInput * accelerationForce * Time.fixedDeltaTime, ForceMode.Acceleration);
            }
        }
        
        void MoveBackward(float moveInput)
        {
            if (_rb.velocity.magnitude < maxSpeed)
            {
                _rb.AddForce(-transform.forward * Mathf.Abs(moveInput) * accelerationForce * Time.fixedDeltaTime, ForceMode.Acceleration);
            }
        }
        
        void HandleSteering()
        {
            float turnInput = Input.GetAxis("Horizontal");
            float directionMultiplier = Vector3.Dot(_rb.velocity, transform.forward) >= 0 ? 1f : -1f;
            
            if (Mathf.Abs(turnInput) > 0.1f && _rb.velocity.magnitude > 1f)
            {
                float turnStrength = Mathf.Clamp(_rb.velocity.magnitude / maxSpeed, 0.2f, 1f);
                Vector3 turnForce = Vector3.up * turnInput * turnTorque * turnStrength * Time.fixedDeltaTime * directionMultiplier;
                _rb.angularVelocity = Vector3.Lerp(_rb.angularVelocity, turnForce, Time.fixedDeltaTime * turnSmoothing);
            }
        }

        void ApplyDrag()
        {
            _rb.velocity *= dragFactor;
        }

        void ApplySidewaysFriction()
        {
            Vector3 localVelocity = transform.InverseTransformDirection(_rb.velocity);
            localVelocity.x *= sidewaysFriction;
            _rb.velocity = transform.TransformDirection(localVelocity);
        }

        void ApplyBraking()
        {
            _rb.velocity *= 0.95f;
        }

        void StabilizeIdleState()
        {
            if (_rb.velocity.magnitude < minVelocityThreshold)
            {
                _rb.velocity = Vector3.zero;
                _rb.angularVelocity = Vector3.zero;
            }
        }
    }
}
