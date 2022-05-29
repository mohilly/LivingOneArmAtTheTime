using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamLook : MonoBehaviour
{
    float mouseX, mouseY;
    public float mouseSensitivity = 250f;

    float xRotation = 0f;
    float yRotation = 0f;

    public float smoothSpeed = 0.125f;
    Quaternion targetRotation;
 
    public Vector3 camOffset = new Vector3(0f, 1.75f, -1f);

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        // PART FOR MOUSE MOVEMENT
        mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, - 45f, 45f);
      
        yRotation += mouseX;                           
        yRotation = Mathf.Clamp(yRotation, - 45f, 45f);
      
        targetRotation = Quaternion.Euler(xRotation, yRotation, 0f);
        transform.localRotation = Quaternion.Lerp(transform.localRotation, targetRotation, smoothSpeed);
        
        transform.localPosition = camOffset;
    }
}
