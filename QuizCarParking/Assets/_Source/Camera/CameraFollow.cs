using UnityEngine;

namespace _Source.Camera
{
    public class CameraFollow : MonoBehaviour
    {
        [SerializeField] private Transform target;  // Машина (объект, за которым следим)
        [SerializeField] private float smoothSpeed = 5f;  // Скорость сглаживания (опционально)

        private void LateUpdate()
        {
            // Текущая позиция камеры
            Vector3 currentPosition = transform.position;

            // Новая позиция камеры с X как у машины, а Y и Z сохраняем
            Vector3 targetPosition = new Vector3(target.position.x - 34f, currentPosition.y, currentPosition.z);

            // Плавное движение камеры (опционально)
            transform.position = Vector3.Lerp(currentPosition, targetPosition, smoothSpeed * Time.deltaTime);
        }
    }
}
