using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class CharacterRigid : MonoBehaviour
{
    public float walkingSpeed = 7.5f;
    public float runningSpeed = 7.5f;
    public float jumpSpeed = 8.0f;
    float charge = 0.0f;
    public float maxCharge = 3.0f;
    public float chargeRate = 0.01f;
    public float bounceModifier = 1;
    bool holding = false;
    public Camera playerCamera;
    public float lookSpeedBase;
    public float lookSpeed = 2.0f;
    public float lookXLimit = 45.0f;
    bool grounded;

    Rigidbody rb;
    PhysicMaterial mat;
    bool bounce;
    float rotationX = 0;

    [HideInInspector]
    public bool canMove = true;
    [HideInInspector]
    public bool canLook = true;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        mat = GetComponent<CapsuleCollider>().material;

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        //lookSpeed = SeneManagement.sensitivity;
        //lookSpeedBase = lookSpeed;

        StartCoroutine(CameraShakeCheck());
    }
    void Update()
    {
        canMove = !holding;
        if (Input.GetKeyDown(KeyCode.R))
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene
                (UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex);
        }
        Effects();

        Vector3 forward = transform.TransformDirection(Vector3.forward);
        Vector3 right = transform.TransformDirection(Vector3.right);

        bool isRunning = Input.GetKey(KeyCode.LeftShift);
        float curSpeedZ = canMove ? (isRunning ? runningSpeed : walkingSpeed)
            * Input.GetAxis("Vertical") : 0;
        float curSpeedX = canMove ? (isRunning ? runningSpeed : walkingSpeed)
            * Input.GetAxis("Horizontal") : 0;

        if (Input.GetButtonDown("Jump") && canMove && isGrounded())
        {
            StartCoroutine(JumpCheck());
        }

        if (canLook)
        {
            rotationX += -Input.GetAxis("Mouse Y") * lookSpeed;
            rotationX = Mathf.Clamp(rotationX, -lookXLimit, lookXLimit);
            playerCamera.transform.localRotation = Quaternion.Euler(rotationX, 0, 0);
            transform.rotation *= Quaternion.Euler(0, Input.GetAxis("Mouse X") * lookSpeed, 0);
        }

        Vector3 move = transform.right * curSpeedX + transform.forward * curSpeedZ;
        if (!holding && isGrounded())
            rb.velocity = move  * Time.deltaTime;
        if(!isGrounded())
        {
            mat.bounciness = bounceModifier;
        }
        else
        {
            mat.bounciness = 0;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        bounce = isGrounded() ? false : true;
    }

    IEnumerator JumpCheck()
    {
        holding = true;
        shakeCam = true;
        if (Input.GetButtonUp("Jump") || charge >= maxCharge)
        {
            rb.AddForce(new Vector3(0, jumpSpeed * charge, 0), ForceMode.Impulse);
            holding = false;
            shakeCam = false;
            yield return new WaitUntil(() => isGrounded() != true);
            StartCoroutine(PushForwards());
        }
        yield return new WaitForSeconds(0.001f);
        if (charge < maxCharge)
            charge += chargeRate;
        if (magnitude < maxCharge / 4)
            magnitude += chargeRate / 20;
        if (holding) StartCoroutine(JumpCheck());
    }

    IEnumerator PushForwards()
    {
        StartCoroutine(Cooldown());
        while (!isGrounded() && !bounce)
        {
            rb.velocity = new Vector3(transform.forward.x * walkingSpeed * Time.deltaTime, rb.velocity.y,
                transform.forward.z * walkingSpeed * Time.deltaTime) ;
            yield return new WaitForEndOfFrame();
        }
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

    bool isGrounded()
    {
        return Physics.Raycast(transform.position, -Vector3.up, GetComponent<Collider>().bounds.extents.y + 0.01f);
    }
}
