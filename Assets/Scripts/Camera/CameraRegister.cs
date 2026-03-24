using System;
using UnityEngine;
using Unity.Cinemachine;
using UnityEngine.Rendering;

    public class CameraRegister : MonoBehaviour
    {
        // When new camera spawns onto scene, register it to the camera switcher
        private void OnEnable()
        {
            CameraSwitcher.Register(GetComponent<CinemachineCamera>());
        }

        // When camera is destroyed, unregister it from the camera switcher
        private void OnDisable()
        {
            CameraSwitcher.Unregister(GetComponent<CinemachineCamera>());
        }
    }
