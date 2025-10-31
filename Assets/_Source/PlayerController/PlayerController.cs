using System;
using UnityEngine;

namespace _Source.PlayerController
{
    public class PlayerController : MonoBehaviour
    {
        public Rigidbody _rb;
        private ITransmission _currentTransmission;
        
        public float maxSpeed = 100f;
        public float accelerationForce = 10f;
        public float dragFactor = 0.99f;
        public float sidewaysFriction = 0.5f;
        public float minVelocityThreshold = 0.1f;
        public float turnTorque = 10f;
        public float turnSmoothing = 5f;
        private float _turnDisableSpeedThreshold = 0.1f;

        public event Action<ITransmission> OnTransmissionChanged;
        
        private float _turnSpeed = 300f;
        public float turnSensitivity = 100f;
        private float _screenCenterX;
        private float _currentTurnInput;

        
        void Start()
        {
            _rb = GetComponent<Rigidbody>();
            _rb.centerOfMass = new Vector3(0, -0.5f, 0);
            SetTransmissionState(new ParkingTransmission(this)); 
            _screenCenterX = Screen.width / 2f;

        }

        void Update()
        {
            SetTransmission();
        }

        void FixedUpdate()
        {
            HandleMovement();
            HandleSteering();
            ApplyDrag();
            ApplySidewaysFriction();
            StabilizeIdleState();
        }
        
        private void SetTransmission()
        {
            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                SetTransmissionState(new ParkingTransmission(this));
            }
            if (Input.GetKeyDown(KeyCode.Alpha2))
            {
                SetTransmissionState(new ReverseTransmission(this));
            }
            if (Input.GetKeyDown(KeyCode.Alpha3))
            {
                SetTransmissionState(new NeutralTransmission(this));
            }
            if (Input.GetKeyDown(KeyCode.Alpha4))
            {
                SetTransmissionState(new DriveTransmission(this));
            }
        }
        
        private void SetTransmissionState(ITransmission transmission)
        {
            _currentTransmission = transmission;
            OnTransmissionChanged?.Invoke(_currentTransmission); 
        }
        
        private void HandleMovement()
        {
            float moveInput = Input.GetAxis("Vertical");
            _currentTransmission.PerformMovement(moveInput);
        }
        
        public void MoveForward(float moveInput)
        {
            if (_rb.velocity.magnitude < maxSpeed)
            {
                _rb.AddForce(transform.forward * moveInput * accelerationForce * Time.fixedDeltaTime, ForceMode.Acceleration);
            }
        }
        
        public void MoveBackward(float moveInput)
        {
            if (_rb.velocity.magnitude < maxSpeed)
            {
                _rb.AddForce(-transform.forward * Mathf.Abs(moveInput) * accelerationForce * Time.fixedDeltaTime, ForceMode.Acceleration);
            }
        }
        
        public void HandleNeutral()
        {
            if (_rb.velocity.magnitude > 0)
            {
                _rb.velocity = _rb.velocity.normalized * 0.5f;
            }
        }
        
        public void ApplyBraking()
        {
            _rb.velocity *= 0.95f;
        }
        
        public void HandleParking()
        {
           _rb.velocity = Vector3.zero; 
           _rb.angularVelocity = Vector3.zero;
        }
        
        void HandleSteering()
        {
            float mouseX = Input.mousePosition.x;
            float targetTurnInput = (mouseX - _screenCenterX) / _screenCenterX;
           
            _currentTurnInput = Mathf.Lerp(_currentTurnInput, targetTurnInput, Time.deltaTime * _turnSpeed * 50f);

            float clampedTurnInput = Mathf.Clamp(_currentTurnInput, -1f, 1f);
            float directionMultiplier = Vector3.Dot(_rb.velocity, transform.forward) >= 0 ? 1f : -1f;

            float speedSqr = _rb.velocity.sqrMagnitude;  
            float thresholdSqr = _turnDisableSpeedThreshold * _turnDisableSpeedThreshold;
            
            if ((Input.GetKey(KeyCode.S) || !Input.GetKey(KeyCode.W)) && speedSqr < thresholdSqr)
            {
                _currentTurnInput = 0f;
                _rb.angularVelocity = Vector3.zero;  
                return;
            }
            
            if (Mathf.Abs(clampedTurnInput) > 0.05f && _rb.velocity.magnitude > 1f)
            {
                float turnStrength = Mathf.Clamp(_rb.velocity.magnitude / maxSpeed, 0.7f, 1f);
                Vector3 turnForce = Vector3.up * clampedTurnInput * turnTorque * turnStrength * turnSensitivity * Time.fixedDeltaTime * directionMultiplier;

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


// void HandleSteering()
// {
//     float mouseX = Input.mousePosition.x;
//     float targetTurnInput = (mouseX - _screenCenterX) / _screenCenterX;  // От -1 до 1
//
//    
//     _currentTurnInput = Mathf.Lerp(_currentTurnInput, targetTurnInput, Time.deltaTime * _turnSpeed * 50f);
//
//     float clampedTurnInput = Mathf.Clamp(_currentTurnInput, -1f, 1f);
//     float directionMultiplier = Vector3.Dot(_rb.velocity, transform.forward) >= 0 ? 1f : -1f;
//
//     float speed = _rb.velocity.magnitude;
//
//     if (Input.GetKey(KeyCode.S) || !Input.GetKey(KeyCode.W))
//     {
//         _currentTurnInput = 0f;
//         _rb.angularVelocity = Vector3.zero;
//         return;
//     }
//
//     
//     if (Mathf.Abs(clampedTurnInput) > 0.05f && speed > 1f)
//     {
//         float turnStrength = Mathf.Clamp(speed / maxSpeed, 0.7f, 1f);
//         Vector3 turnForce = Vector3.up * clampedTurnInput * turnTorque * turnStrength * turnSensitivity * Time.fixedDeltaTime * directionMultiplier;
//
//         _rb.angularVelocity = Vector3.Lerp(_rb.angularVelocity, turnForce, Time.fixedDeltaTime * turnSmoothing);
//     }
// }
