using System;
using UnityEngine;

namespace _Source.PlayerController
{
    public interface ITransmission
    {
        void PerformMovement(float moveInput);
    }

    // Классы состояний трансмиссии

    public class DriveTransmission : ITransmission
    {
        private PlayerController _controller;

        public DriveTransmission(PlayerController controller)
        {
            _controller = controller;
        }

        public void PerformMovement(float moveInput)
        {
            if (moveInput > 0)
            {
                _controller.MoveForward(moveInput); // Едем вперед
            }
            else if (moveInput < 0)
            {
                _controller.ApplyBraking(); // Тормозим при нажатии S
            }
        }
    }

    public class ReverseTransmission : ITransmission
    {
        private PlayerController _controller;

        public ReverseTransmission(PlayerController controller)
        {
            _controller = controller;
        }

        public void PerformMovement(float moveInput)
        {
            if (moveInput > 0)
            {
                _controller.MoveBackward(moveInput); // Едем вперед
            }
            else if (moveInput < 0)
            {
                _controller.ApplyBraking(); // Тормозим при нажатии S
            }
        }
    }

    public class NeutralTransmission : ITransmission
    {
        private PlayerController _controller;

        public NeutralTransmission(PlayerController controller)
        {
            _controller = controller;
        }

        public void PerformMovement(float moveInput)
        {
            _controller.HandleNeutral();
        }
    }

    public class ParkingTransmission : ITransmission
    {
        private PlayerController _controller;

        public ParkingTransmission(PlayerController controller)
        {
            _controller = controller;
        }

        public void PerformMovement(float moveInput)
        {
            _controller.HandleParking();
        }
    }
}