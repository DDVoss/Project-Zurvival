using System.Collections.Generic;
using Unity.Cinemachine;
using UnityEngine;

public static class CameraSwitcher
{
    
    static List<CinemachineCamera> cameras = new List<CinemachineCamera>();

    public static CinemachineCamera ActiveCamera = null;

    public static bool IsActive(CinemachineCamera camera)
    {
        return camera == ActiveCamera;
    }

    public static void SwitchCamera(CinemachineCamera camera)
    {
        // Set the priority of the new active camera to 10
        camera.Priority = 10;
        ActiveCamera = camera;

        // For each camera which is not the active camera, set priority to 0
        foreach (CinemachineCamera c in cameras)
        {
            if (c != camera && c.Priority != 0)
            {
                c.Priority = 0;
            }
        }
    }
    
    public static void Register(CinemachineCamera camera)
    {
        cameras.Add(camera);
        Debug.Log("Camera registered: " + camera);
    }
    
    public static void Unregister(CinemachineCamera camera)
    {
        cameras.Remove(camera);
        Debug.Log("Camera unregistered: " + camera);
    }
}