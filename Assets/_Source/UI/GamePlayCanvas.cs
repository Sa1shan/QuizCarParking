using UnityEngine;

namespace _Source.UI
{
    public class GamePlayCanvas : MonoBehaviour
    {
        [SerializeField] private UnityEngine.Camera camera;
        [SerializeField] private Canvas canvas;

        void Update()
        {
            if (camera.gameObject.activeSelf)
            {
                canvas.gameObject.SetActive(false);
            }
            else
            {
                canvas.gameObject.SetActive(true);
            }
        }
    }
}
