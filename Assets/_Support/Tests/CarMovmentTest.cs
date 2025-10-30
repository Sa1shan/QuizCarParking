using System.Collections;
using _Source.PlayerController;
using NUnit.Framework;
using UnityEngine;
using Object = UnityEngine.Object;
using UnityEngine.TestTools;

namespace Tests
{
    public class CarMovementTests
    {
        private GameObject _car;
        private PlayerController _controller;
        private Rigidbody _rb;

        [SetUp]
        public void SetUp()
        {
            _car = new GameObject("TestCar");
            _rb = _car.AddComponent<Rigidbody>();
            _controller = _car.AddComponent<PlayerController>();
            
            _rb.useGravity = false;
            _rb.drag = 0f;
            _rb.angularDrag = 0f;
            
            _controller.GetType().GetMethod("Awake")?.Invoke(_controller, null);
            _controller.GetType().GetMethod("Start")?.Invoke(_controller, null);
            
            var rbField = typeof(PlayerController).GetField("_rb", 
                System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
            if (rbField != null)
            {
                rbField.SetValue(_controller, _rb);
            }
        }

        [TearDown]
        public void TearDown()
        {
            if (_car != null)
            {
                Object.DestroyImmediate(_car);
            }
        }

        [UnityTest]
        public IEnumerator CarMovesForward_WhenMoveForwardCalled()
        {
            Vector3 startPos = _car.transform.position;
            
            for (int i = 0; i < 10; i++)
            {
                _controller.MoveForward(1f);
                yield return new WaitForFixedUpdate();
            }

            Vector3 endPos = _car.transform.position;
            
            Assert.Greater(endPos.z, startPos.z, 
                "Машина не двигается вперёд при вызове MoveForward().");
        }

        [UnityTest]
        public IEnumerator CarStops_WhenHandleParkingCalled()
        {
            for (int i = 0; i < 5; i++)
            {
                _controller.MoveForward(1f);
                yield return new WaitForFixedUpdate();
            }
            
            _controller.HandleParking();
            yield return new WaitForFixedUpdate();
            
            Assert.AreEqual(0f, _rb.velocity.magnitude, 0.001f, 
                "Машина не остановилась после HandleParking().");
            Assert.AreEqual(0f, _rb.angularVelocity.magnitude, 0.001f, 
                "Машина всё ещё вращается после HandleParking().");
        }

        [UnityTest]
        public IEnumerator CarMovesBackward_WhenMoveBackwardCalled()
        {
            Vector3 startPos = _car.transform.position;

            for (int i = 0; i < 10; i++)
            {
                _controller.MoveBackward(1f);
                yield return new WaitForFixedUpdate();
            }

            Vector3 endPos = _car.transform.position;

            Assert.Less(endPos.z, startPos.z, 
                "Машина не двигается назад при вызове MoveBackward().");
        }
    }
}