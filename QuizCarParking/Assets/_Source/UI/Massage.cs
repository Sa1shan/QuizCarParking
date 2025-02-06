using UnityEngine;

namespace _Source.UI
{
    public class Massage : MonoBehaviour
    {
        [SerializeField] private GameObject massage;
        void Start()
        {
            massage.SetActive(true);
        }
        public void OnClick()
        {
            massage.SetActive(false);
        }
    }
}
