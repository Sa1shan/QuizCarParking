using System;
using System.Collections.Generic;
using _Source.PlayerController;
using UnityEngine;
using UnityEngine.UI;

namespace _Source.UI
{
    public class TransmisionManager : MonoBehaviour
    {
        [SerializeField] private List<Image> images;

        void Start()
        {
            images[0].color = Color.green;
        }
        
        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                images[0].color = Color.green;
                foreach (var img in images)
                {
                    if (img != images[0]) 
                    {
                        img.color = Color.black;
                    }
                }
            }

            if (Input.GetKeyDown(KeyCode.Alpha2))
            {
                images[1].color = Color.green;
                foreach (var img in images)
                {
                    if (img != images[1]) 
                    {
                        img.color = Color.black;
                    }
                }
            }
            if (Input.GetKeyDown(KeyCode.Alpha3))
            {
                images[2].color = Color.green;
                foreach (var img in images)
                {
                    if (img != images[2]) 
                    {
                        img.color = Color.black;
                    }
                }
            }
            if (Input.GetKeyDown(KeyCode.Alpha4))
            {
                images[3].color = Color.green;
                foreach (var img in images)
                {
                    if (img != images[3]) 
                    {
                        img.color = Color.black;
                    }
                }
            }
        }
    }
}
