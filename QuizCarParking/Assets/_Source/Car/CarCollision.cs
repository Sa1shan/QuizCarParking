using UnityEngine;

namespace _Source.Car
{
    public class CarCollision : MonoBehaviour
    {
        [SerializeField] private GameObject crushMassage;
        void OnCollisionEnter(Collision collision)
        {
            if (collision.gameObject.CompareTag("MainCar"))
            {
                Debug.Log("Player collision");
                crushMassage.SetActive(true);
                Time.timeScale = 0f;
            }
        }
    }
}
