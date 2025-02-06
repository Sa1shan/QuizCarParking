using TMPro;
using UnityEngine;
public class PlayerLose : MonoBehaviour
{
    [SerializeField] private GameObject nextLevelMassage;
    
    private void OnTriggerStay(Collider other)
    {
        if (IsFullyInside(other))
        {
            Time.timeScale = 0;
            Debug.Log("Объект полностью внутри триггера");
            nextLevelMassage.SetActive(true);
        }
    }

    private bool IsFullyInside(Collider other)
    {
        Collider triggerCollider = GetComponent<Collider>();
        return triggerCollider.bounds.Contains(other.bounds.min) && triggerCollider.bounds.Contains(other.bounds.max);
    }
}
