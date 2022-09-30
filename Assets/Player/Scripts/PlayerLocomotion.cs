using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerLocomotion : MonoBehaviour
{

    CharacterController characterController;
    Transform playerContainer, cameraContainer;

    public float speed = 6.0f;
    public float jumpspeed = 10f;
    public float mouseSensitivity = 2f;
    public float gravity = 20.0f;
    public float lookUpCLamp = -30f;
    public float lookDownClamp = 60;

    private Vector3 moveDirection = Vector3.zero;
    float rotateX, rotateY;


    void Start()
    {
        Cursor.visible = false;
        characterController = GetComponent<CharacterController>();
      SetCurrentCamera();
    }

    // Update is called once per frame
    void Update()
    {

        Locomotion();
        RotateAndLook();
    }

    void SetCurrentCamera()
    {
        playerContainer = gameObject.transform.Find("Container3P");
        cameraContainer = playerContainer.transform.Find("Camera3PContainer");
    }

    void Locomotion()
    {
        if(characterController.isGrounded)
        {
            moveDirection = new Vector3(Input.GetAxis("Horizontal"), 0f, Input.GetAxis("Vertical"));
            moveDirection = transform.TransformDirection(moveDirection);
            moveDirection *= speed;

            //ToDo Jumping/Crouching
        }

        moveDirection.y -= gravity * Time.deltaTime;

        characterController.Move(moveDirection * Time.deltaTime);
    
    }

   void RotateAndLook()
    {
        rotateX = Input.GetAxis("Mouse X") * mouseSensitivity;
        rotateY -= Input.GetAxis("Mouse Y") * mouseSensitivity * mouseSensitivity;

        rotateY = Mathf.Clamp(rotateY, lookUpCLamp, lookDownClamp);
        transform.Rotate(0f, rotateX, 0f);

        cameraContainer.transform.localRotation = Quaternion.Euler(rotateY, 0f, 0f);
    }
}