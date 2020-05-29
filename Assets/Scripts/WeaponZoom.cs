using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponZoom : MonoBehaviour
{
    [SerializeField] Camera cam;
    [SerializeField] float zoomedInFov = 30f;
    [SerializeField] float zoomedOutFov = 60f;
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
        }
        else
        {
            zoomedInToggle = false;
            cam.fieldOfView = zoomedOutFov;
        }    
    }
}
