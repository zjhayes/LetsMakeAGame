using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Core.Interfaces;
using TMPro;

[RequireComponent(typeof(CharacterController))]
public class PlayerMovement : MonoBehaviour
{
    [SerializeField]
    private Transform camera;
    [SerializeField]
    private float walkSpeed = 3f;
    [SerializeField]
    private float runSpeed = 6f;
    [SerializeField]
    private float turnSmoothTime = 0.1f;
    [SerializeField]
    Animator animator; // TODO: Move animator to own controller.
    private CharacterController controller;
    private IInput input;
    private ITime time;
    private float turnSmoothVelocity;
    
    void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    private void OnEnable()
    {
        var inputManager = GameObject.Find("InputManager");
        input = inputManager.GetComponent<IInput>();
        var timeManager = GameObject.Find("TimeManager");
        time = timeManager.GetComponent<ITime>();
    }

    void Update()
    {
        float horizontal = input.GetAxis(InputConstants.HORIZONTAL);
        float vertical = input.GetAxis(InputConstants.VERTICAL);
        Vector3 direction = new Vector3(horizontal, 0f, vertical).normalized;

        float speed = 0;
        if(direction.magnitude >= 0.1f)
        {
            speed = (input.GetAxis(InputConstants.RUN) >= 0.1f) ? runSpeed : walkSpeed;
            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + camera.eulerAngles.y;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);
            transform.rotation = Quaternion.Euler(0f, angle, 0f);
            Vector3 moveDirection = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
            controller.Move(moveDirection.normalized * speed * time.DeltaTime);
        }
        float smoothTime = .1f;
        float speedPercent = speed / runSpeed;
        animator.SetFloat("speedPercent", speedPercent, smoothTime, time.DeltaTime);
    }
}
