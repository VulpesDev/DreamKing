using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

[RequireComponent(typeof(CharacterController))]

public class Character : MonoBehaviour
{
    public float walkingSpeed = 7.5f;
    public float runningSpeed = 7.5f;
    public float jumpSpeed = 8.0f;
    float charge = 0.0f;
    public float maxCharge = 3.0f;
    public float chargeRate = 0.01f;
    bool holding = false;
    public float gravity = 20.0f;
    public Camera playerCamera;
    public float lookSpeedBase;
    public static float lookSpeed = 2.0f;
    public float lookXLimit = 45.0f;
    public bool grounded;

    CharacterController characterController;
    Vector3 moveDirection = Vector3.zero;
    float rotationX = 0;

    [HideInInspector]
    public bool canMove = true;
    [HideInInspector]
    public bool canLook = true;

    void Start()
    {
        //Application.targetFrameRate = 60;
        characterController = GetComponent<CharacterController>();

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        lookSpeedBase = lookSpeed;

        StartCoroutine(CameraShakeCheck());
    }
    void Update()
    {
        grounded = characterController.isGrounded;
        if (grounded) bounced = false;
        if (Input.GetKeyDown(KeyCode.R))
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene
                (UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex);
        }

        Effects();
        Vector3 forward = transform.TransformDirection(Vector3.forward);
        Vector3 right = transform.TransformDirection(Vector3.right);

        bool isRunning = Input.GetKey(KeyCode.LeftShift);
        float curSpeedX = canMove ? (isRunning ? runningSpeed : walkingSpeed)
            * Input.GetAxis("Vertical") : 0;
        float curSpeedY = canMove ? (isRunning ? runningSpeed : walkingSpeed)
            * Input.GetAxis("Horizontal") : 0;
        float movementDirectionY = moveDirection.y;
        moveDirection = (forward * curSpeedX) + (right * curSpeedY);

        if (Input.GetButtonDown("Jump") && canMove && characterController.isGrounded)
        {
            StartCoroutine(JumpCheck());
        }
        else
        {
            moveDirection.y = movementDirectionY;
        }

        if (canLook)
        {
            rotationX += -Input.GetAxis("Mouse Y") * lookSpeed;
            rotationX = Mathf.Clamp(rotationX, -lookXLimit, lookXLimit);
            playerCamera.transform.localRotation = Quaternion.Euler(rotationX, 0, 0);
            transform.rotation *= Quaternion.Euler(0, Input.GetAxis("Mouse X") * lookSpeed, 0);
        }
        if (!characterController.isGrounded)
        {
            moveDirection.y -= gravity * Time.deltaTime;
            characterController.Move(new Vector3(0, moveDirection.y, 0) * Time.deltaTime);
        }
        if (!holding && characterController.isGrounded)
            characterController.Move(moveDirection * Time.deltaTime);
    }

    bool bounced = false;
    Vector3 wallDir;

    private void OnControllerColliderHit(ControllerColliderHit col)
    {
        StartCoroutine(Bounce(col));
    }
    IEnumerator Bounce(ControllerColliderHit col)
    {
        yield return new WaitForEndOfFrame();
        if (!characterController.isGrounded)
        {
            Sounds.PlaySound("Bounce", 1f);
            wallDir =
            new Vector3(col.point.x - transform.position.x,
            (col.point.y - transform.position.y), col.point.z - transform.position.z);
            wallDir.Normalize();
            if (col.point.y > transform.position.y)
                moveDirection.y *= -.5f;
            bounced = true;
        }
    }

    IEnumerator JumpCheck()
    {
        holding = true;
        shakeCam = true;
        if (Input.GetButtonUp("Jump") || charge >= maxCharge)
        {
            Sounds.PlaySound("Jump", 0.5f);
            moveDirection.y = Mathf.Clamp(jumpSpeed * charge, 0 , jumpSpeed);
            holding = false;
            shakeCam = false;
            yield return new WaitUntil(() => characterController.isGrounded != true);
            StartCoroutine(PushForwards());
        }
        yield return new WaitForSeconds(0.001f);
        if (charge < maxCharge)
            charge += chargeRate * Time.deltaTime;
        if (magnitude < maxCharge / 4)
            magnitude += chargeRate / 20 * Time.deltaTime;
        if (holding) StartCoroutine(JumpCheck());
    }

    IEnumerator PushForwards()
    {
        StartCoroutine(Cooldown());
        while (!characterController.isGrounded)
        {
            characterController.Move( (bounced ? -wallDir : transform.TransformDirection(Vector3.forward))
                * walkingSpeed * Time.deltaTime);
            yield return new WaitForFixedUpdate();
        }
        bounced = false;
    }
    IEnumerator Cooldown()
    {
        while (magnitude > 0.0f)
        {
            magnitude -= 0.6f;
            if (magnitude < 0.0f) magnitude = 0.0f;
            yield return new WaitForSeconds(0.01f);
        }

        while (charge > 0.0f)
        {
            charge -= 0.4f; if (charge < 0.0f) charge = 0.0f;
            yield return new WaitForSeconds(0.01f);
        }
    }

    void Effects()
    {
        Volume vol = GameObject.Find("Volume").GetComponent<Volume>();

        if (vol.profile.TryGet(out Vignette vignette2))
        {
            Vignette vignette = vignette2;
            vignette.intensity.Override(charge / 3f);
        }

    }
    float magnitude = 0.0f;
    bool shakeCam;
    IEnumerator CameraShakeCheck()
    {
        GameObject cam = Camera.main.gameObject;
        Vector3 originalPosition = cam.transform.localPosition;
        while (shakeCam)
        {
            float strenght = magnitude;
            cam.transform.localPosition = originalPosition + Random.insideUnitSphere * strenght;
            yield return null;
        }
        cam.transform.localPosition = originalPosition;
        yield return new WaitForSeconds(0.1f);
        StartCoroutine(CameraShakeCheck());
    }
}