using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Object dzieki temu ma charactercontroller
[RequireComponent(typeof(CharacterController))]


public class PlayerMovement : MonoBehaviour
{
    public float PlayerWalkingSpeed = 5f;
    public float PlayerRunningSpeed = 15f;
    public float JumpStrenght = 20f;
    public float VerticalRotationLimit = 80f;

    float ForwardMovement;
    float SidewaysMovement;
    float VerticalRotation = 0;
    float VerticalVelosity;

    CharacterController cc;

    private void Awake()
    {
        cc = GetComponent<CharacterController>();
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void Update()
    {
        //Obracanie sie
        float HorizontalRotation = Input.GetAxis("Mouse X");
        transform.Rotate(0, HorizontalRotation, 0);

        //Rozgladanie sie
        VerticalRotation -= Input.GetAxis("Mouse Y");
        VerticalRotation = Mathf.Clamp(VerticalRotation, -VerticalRotationLimit, VerticalRotationLimit);
        Camera.main.transform.localRotation = Quaternion.Euler(VerticalRotation, 0, 0);

        //Ruch graczem
        if (cc.isGrounded)
        {
            ForwardMovement = Input.GetAxis("Vertical") * PlayerWalkingSpeed;
            SidewaysMovement = Input.GetAxis("Horizontal") * PlayerWalkingSpeed;

            if (Input.GetKey(KeyCode.LeftShift)) //sprawdzamy czy gracz chce sprint
            {
                ForwardMovement = Input.GetAxis("Vertical") * PlayerRunningSpeed;
                SidewaysMovement = Input.GetAxis("Horizontal") * PlayerRunningSpeed;
            }
        }

        VerticalVelosity += Physics.gravity.y * Time.deltaTime;

        if (Input.GetButton("Jump") && cc.isGrounded) //sprawdzamy czy gracz jest uziemiony
        {
            VerticalVelosity = JumpStrenght;
        }

        Vector3 PlayerMovement = new Vector3(SidewaysMovement, VerticalVelosity, ForwardMovement);

        cc.Move(transform.rotation * PlayerMovement * Time.deltaTime);
    }


}
