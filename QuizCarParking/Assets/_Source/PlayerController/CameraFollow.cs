using System;
using UnityEngine;

namespace _Source.PlayerController
{
    public class CameraFollow : MonoBehaviour
    {
        [SerializeField] private GameObject player;
        
        private Vector3 _offset;

        private void Update()
        {
            Vector3 camPos = gameObject.transform.position;
            Vector3 playerPos = player.transform.position;
            _offset = playerPos - camPos;
            gameObject.transform.position = playerPos + _offset;
        }
    }
}
