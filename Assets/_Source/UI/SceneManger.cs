using UnityEngine;
using UnityEngine.SceneManagement;
namespace _Source.UI
{
    public class SceneManger : MonoBehaviour
    {
        [SerializeField] private int SceneNumber;
        public void OnClick()
        {
            SceneManager.LoadScene(SceneNumber);
            Time.timeScale = 1f;
        }
    }
}
