using UnityEngine;

namespace _Source.Camera
{
    public class CameraFollow : MonoBehaviour
    {
        [SerializeField] private Transform target; 
        [SerializeField] private float smoothSpeed = 5f;  

        private void LateUpdate()
        {
            Vector3 currentPosition = transform.position;
            
            Vector3 targetPosition = new Vector3(target.position.x - 34f, currentPosition.y, currentPosition.z);
            
            transform.position = Vector3.Lerp(currentPosition, targetPosition, smoothSpeed * Time.deltaTime);
        }
    }
}
