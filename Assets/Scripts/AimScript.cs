using UnityEngine;
using System.Collections;

public class AimScript : MonoBehaviour
{

    float mouseX;
    float mouseY;
    Quaternion rotationSpeed;

    [Header("Gun Options")]
    public float aimSpeed = 6.5f;
    public float moveSpeed = 15.0f;

    [Header("Gun Positions")]
    public Vector3 defaultPosition;
    public Vector3 zoomPosition;

    [Header("Camera")]
    public Camera gunCamera;

    [Header("Camera Options")]
    public float fovSpeed = 15.0f;
    public float zoomFov = 30.0f;
    public float defaultFov = 60.0f;

    [Header("Audio")]
    public AudioSource aimSound;
    bool soundHasPlayed = false;

    void Update()
    {
        if (Input.GetMouseButton(1))
        {
            transform.localPosition = Vector3.Lerp(transform.localPosition,
                                                   zoomPosition, Time.deltaTime * moveSpeed);
            gunCamera.fieldOfView = Mathf.Lerp(gunCamera.fieldOfView,
                                               zoomFov, fovSpeed * Time.deltaTime);
            if (!soundHasPlayed)
            {
                // aimSound.Play();
                soundHasPlayed = true;
            }
        }
        else
        {
            transform.localPosition = Vector3.Lerp(transform.localPosition,
                                                   defaultPosition, Time.deltaTime * moveSpeed);
            gunCamera.fieldOfView = Mathf.Lerp(gunCamera.fieldOfView,
                                               defaultFov, fovSpeed * Time.deltaTime);

        }

        soundHasPlayed = false;
        mouseX = Input.GetAxis("Mouse X");
        mouseY = Input.GetAxis("Mouse Y");

        rotationSpeed = Quaternion.Euler(-mouseY, mouseX, 0);

        transform.localRotation = Quaternion.Slerp
            (transform.localRotation, rotationSpeed, aimSpeed * Time.deltaTime);
    }
}