using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PempekMovement : MonoBehaviour
{
    public float moveSpeed = 10f;
    public float verticalSpeed = 5f;
    public float rotationSpeed = 5f;
    public float smoothDamp = 0.3f;

    private Rigidbody rb;
    private float targetVerticalSpeed = 0f;
    private float currentVerticalSpeed = 0f;
    private float verticalVelocity = 0f;
    public Animator propellerAnimator;

    [Header("Audio Settings")]
    public AudioSource interactAudio;
    public PauseMenuOption pauseMenu;

    [Header("Stamina Settings")]
    public float maxStamina = 100f;
    public float staminaDrainRate = 20f;
    public float staminaRegenRate = 10f;
    public Image staminaBar; 

    private float currentStamina; 

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        Time.timeScale = 1;
        currentStamina = maxStamina;
        if (staminaBar != null)
        {
            staminaBar.fillAmount = 1f;
        }
    }

    void FixedUpdate()
    {
        Vector3 movement = Vector3.zero;
        bool isMoving = false;

        bool isBoosting = Input.GetKey(KeyCode.LeftShift) && currentStamina > 0;

        if (Input.GetKey(KeyCode.W))
        {
            float speedMultiplier = isBoosting ? 2f : 1f;
            movement += transform.forward * moveSpeed * speedMultiplier * Time.deltaTime;
            isMoving = true;

            if (isBoosting)
            {
                currentStamina -= staminaDrainRate * Time.deltaTime;
                if (currentStamina < 0) currentStamina = 0;
            }
        }

        if (Input.GetKey(KeyCode.A))
        {
            movement -= transform.right * moveSpeed * Time.deltaTime;
            isMoving = true;
        }
        else if (Input.GetKey(KeyCode.D))
        {
            movement += transform.right * moveSpeed * Time.deltaTime;
            isMoving = true;
        }

        if (Input.GetKey(KeyCode.Space))
        {
            targetVerticalSpeed = verticalSpeed;
            isMoving = true;
        }
        else if (Input.GetKey(KeyCode.S))
        {
            targetVerticalSpeed = -verticalSpeed;
            isMoving = true;
        }
        else
        {
            targetVerticalSpeed = 0f;
        }

        currentVerticalSpeed = Mathf.SmoothDamp(currentVerticalSpeed, targetVerticalSpeed, ref verticalVelocity, smoothDamp);
        Vector3 verticalMovement = transform.up * currentVerticalSpeed * Time.deltaTime;

        rb.MovePosition(rb.position + movement + verticalMovement);

        if (movement != Vector3.zero)
        {
            Quaternion targetRotation = Quaternion.LookRotation(movement);
            rb.rotation = Quaternion.Slerp(rb.rotation, targetRotation, rotationSpeed * Time.deltaTime);
        }

        if (!isBoosting && currentStamina < maxStamina)
        {
            currentStamina += staminaRegenRate * Time.deltaTime;
            if (currentStamina > maxStamina) currentStamina = maxStamina;
        }

        if (staminaBar != null)
        {
            staminaBar.fillAmount = currentStamina / maxStamina;
        }

        propellerAnimator.SetBool("IsMoving", isMoving);

        if (Input.GetKeyDown(KeyCode.F))
        {
            PlayInteractAudio();
        }
    }

    private void PlayInteractAudio()
    {
        if (interactAudio != null && pauseMenu != null)
        {
            float volume = pauseMenu.soundSlider.value;
            interactAudio.volume = volume;
            interactAudio.Play();
            Debug.Log("Interacted! Audio played with volume: " + volume);
        }
    }
}