using UnityEngine;

namespace _Source.Camera
{
    public class CamerSwithc : MonoBehaviour
    {
        [SerializeField] private GameObject twoDCamera;
        [SerializeField] private GameObject threeDCamera;
        
        private bool _isCamActive = true;
        void Start()
        {
            CamSetActive(true, false);
        }
        
        void Update()
        {
            if (Input.GetKeyDown(KeyCode.T))
            {
                _isCamActive = !_isCamActive;
                CamSetActive(_isCamActive, !_isCamActive);
            }
        }

        void CamSetActive(bool twoDCam, bool threeDCam)
        {
            twoDCamera.SetActive(twoDCam);
            threeDCamera.SetActive(threeDCam);
        }
    }
}
