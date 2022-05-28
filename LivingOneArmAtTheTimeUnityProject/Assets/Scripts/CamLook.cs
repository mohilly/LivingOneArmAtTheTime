using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamLook : MonoBehaviour
{

    public float mouseSensitivity = 100f;

    float xRotation = 0f;
    float yRotation = 0f;

    public float smoothSpeed = 0.125f;
    Quaternion targetRotation;

    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -45f, 45f);

        yRotation += mouseX;
        yRotation = Mathf.Clamp(yRotation, -45f, 45f);

        targetRotation = Quaternion.Euler(xRotation, yRotation, 0f);
        transform.localRotation = Quaternion.Lerp(transform.localRotation, targetRotation, smoothSpeed);
    }
}
