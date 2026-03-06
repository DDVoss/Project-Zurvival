using System;
using UnityEngine;
using Unity.Cinemachine;
using UnityEngine.Rendering;

    public class CameraRegister : MonoBehaviour
    {
        private void OnEnable()
        {
            CameraSwitcher.Register(GetComponent<CinemachineCamera>());
        }

        private void OnDisable()
        {
            CameraSwitcher.Unregister(GetComponent<CinemachineCamera>());
        }
    }
