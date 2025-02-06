using System;
using UnityEngine;

namespace _Source.PlayerController
{
    public class PlayerController : MonoBehaviour
    {
        public Rigidbody _rb;
        private ITransmission _currentTransmission;

        // Настройки
        public float maxSpeed = 100f;
        public float accelerationForce = 10f;
        public float dragFactor = 0.99f;
        public float sidewaysFriction = 0.5f;
        public float minVelocityThreshold = 0.1f;
        public float turnTorque = 10f;
        public float turnSmoothing = 5f;

        public event Action<ITransmission> OnTransmissionChanged;
        
        private float turnSpeed = 3f;
        public float turnSensitivity = 1f;
        private float screenCenterX;
        private float currentTurnInput;

        
        void Start()
        {
            _rb = GetComponent<Rigidbody>();
            _rb.centerOfMass = new Vector3(0, -0.5f, 0);
            SetTransmissionState(new ParkingTransmission(this));  // Начальное состояние трансмиссии
            screenCenterX = Screen.width / 2f;

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

        // Смена состояния трансмиссии
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

        // Устанавливаем новое состояние трансмиссии
        private void SetTransmissionState(ITransmission transmission)
        {
            _currentTransmission = transmission;
            OnTransmissionChanged?.Invoke(_currentTransmission);  // Можно подписаться на это событие
        }

        // Обработка движения в зависимости от трансмиссии
        private void HandleMovement()
        {
            float moveInput = Input.GetAxis("Vertical");
            _currentTransmission.PerformMovement(moveInput);
        }

        // Управление движением вперед
        public void MoveForward(float moveInput)
        {
            if (_rb.velocity.magnitude < maxSpeed)
            {
                _rb.AddForce(transform.forward * moveInput * accelerationForce * Time.fixedDeltaTime, ForceMode.Acceleration);
            }
        }

        // Управление движением назад
        public void MoveBackward(float moveInput)
        {
            if (_rb.velocity.magnitude < maxSpeed)
            {
                _rb.AddForce(-transform.forward * Mathf.Abs(moveInput) * accelerationForce * Time.fixedDeltaTime, ForceMode.Acceleration);
            }
        }

        // Нейтральное положение (замедление)
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
           _rb.velocity = Vector3.zero;  // Останавливаем машину при парковке
           _rb.angularVelocity = Vector3.zero;
        }
        
        // Управление рулевым механизмом
        void HandleSteering()
        {
            float mouseX = Input.mousePosition.x;
            float targetTurnInput = (mouseX - screenCenterX) / screenCenterX; // От -1 до 1
                
            // Плавно меняем поворот, но теперь через turnSpeed
            currentTurnInput = Mathf.Lerp(currentTurnInput, targetTurnInput, Time.deltaTime * turnSpeed);
                
            // Ограничиваем диапазон значений от -1 до 1
            float clampedTurnInput = Mathf.Clamp(currentTurnInput, -1f, 1f);
                
            float directionMultiplier = Vector3.Dot(_rb.velocity, transform.forward) >= 0 ? 1f : -1f;
                
            if (Mathf.Abs(clampedTurnInput) > 0.05f && _rb.velocity.magnitude > 1f)
            {
                float turnStrength = Mathf.Clamp(_rb.velocity.magnitude / maxSpeed, 0.2f, 1f);
                Vector3 turnForce = Vector3.up * clampedTurnInput * turnTorque * turnStrength * turnSensitivity * Time.fixedDeltaTime * directionMultiplier;
                
                _rb.angularVelocity = Vector3.Lerp(_rb.angularVelocity, turnForce, Time.fixedDeltaTime * turnSmoothing);
            }
        }


        // Применение тормозного усилия
        void ApplyDrag()
        {
            _rb.velocity *= dragFactor;
        }

        // Боковое трение
        void ApplySidewaysFriction()
        {
            Vector3 localVelocity = transform.InverseTransformDirection(_rb.velocity);
            localVelocity.x *= sidewaysFriction;
            _rb.velocity = transform.TransformDirection(localVelocity);
        }

        // Стабилизация автомобиля на холостом ходу
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
