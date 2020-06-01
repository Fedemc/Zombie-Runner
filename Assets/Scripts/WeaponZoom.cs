using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;

public class WeaponZoom : MonoBehaviour
{
    [SerializeField] Camera cam;
    [SerializeField] float zoomedInFov = 30f;
    [SerializeField] float zoomedOutFov = 60f;
    [SerializeField] float zoomedOutSensitivity= 2f;
    [SerializeField] float zoomedInSensitivity = 0.5f;
    [SerializeField] RigidbodyFirstPersonController fpsController;

    bool zoomedInToggle = false;
    void Start()
    {
        
    }

    
    void Update()
    {
        if(Input.GetMouseButtonDown(1))
        {
            HandleZoom();
        }
        
    }

    private void HandleZoom()
    {
        if(!zoomedInToggle)
        {
            zoomedInToggle = true;
            cam.fieldOfView = zoomedInFov;
            fpsController.mouseLook.XSensitivity = zoomedInSensitivity;
            fpsController.mouseLook.YSensitivity = zoomedInSensitivity;
        }
        else
        {
            zoomedInToggle = false;
            cam.fieldOfView = zoomedOutFov;
            fpsController.mouseLook.XSensitivity = zoomedOutSensitivity;
            fpsController.mouseLook.YSensitivity = zoomedOutSensitivity;
        }    
    }
}
