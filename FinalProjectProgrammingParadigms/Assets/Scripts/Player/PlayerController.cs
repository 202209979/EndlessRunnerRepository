using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float movementSpeed;
    [SerializeField] private float gravity = 20f;
    [SerializeField] private float laneDistance = 2.1f;
    [SerializeField] private float jumpValue = 15f;
    [SerializeField] private float slideDuration = 1f;

    private CharacterController characterController;
    private PlayerAnimations playerAnimations;
    private float verticalPosition;
    private int currentLane = 1;
    private float controllerRadius;
    private float controllerHeight;
    private Vector3 controllerCenter;
    private float slideTimer;
    private bool isSliding = false;

    public bool IsJumping { get; private set; }

    void Start()
    {
        characterController = GetComponent<CharacterController>();
        playerAnimations = GetComponent<PlayerAnimations>();
        controllerHeight = characterController.height;
        controllerRadius = characterController.radius;
        controllerCenter = characterController.center;

        Animator animator = GetComponent<Animator>();
        if (animator != null)
        {
            animator.applyRootMotion = false;
        }

    }

    void Update()
    {
        if (GameManager.Instance.CurrentState == GameStates.Start || GameManager.Instance.CurrentState == GameStates.GameOver)
        {
            return;
        }
        VerticalMovement();
        LaneMovement();
        HandleSlide();
        MoveCharacter();
    }

    private void MoveCharacter()
    {
        float targetX = (currentLane - 1) * laneDistance;
        Vector3 newPosition = new Vector3(targetX - transform.position.x, verticalPosition, movementSpeed);
        characterController.Move(newPosition * Time.deltaTime);
        Vector3 finalPosition = characterController.transform.position;
        finalPosition.x = targetX;
        characterController.transform.position = finalPosition;

        if (characterController.isGrounded && !isSliding && !IsJumping)
        {
            playerAnimations.ShowAnimation("Run");
        }
    }

    private void VerticalMovement()
    {
        if (characterController.isGrounded)
        {
            IsJumping = false;
            verticalPosition = 0f;

            if (Input.GetKey(KeyCode.W))
            {
                verticalPosition = jumpValue;
                IsJumping = true;
                playerAnimations.ShowAnimation("Jump");

                if (isSliding)
                {
                    EndSlide();
                }
            }
        }
        else
        {
            if (Input.GetKey(KeyCode.S))
            {
                verticalPosition -= jumpValue;
                StartSlide();
            }
        }
        verticalPosition -= gravity * Time.deltaTime;
    }

    private void LaneMovement()
    {
        if (Input.GetKeyDown(KeyCode.A) && currentLane > 0)
        {
            currentLane--;
        }

        if (Input.GetKeyDown(KeyCode.D) && currentLane < 2)
        {
            currentLane++;
        }
    }

    private void HandleSlide()
    {
        if (Input.GetKey(KeyCode.S) && !isSliding && characterController.isGrounded)
        {
            StartSlide();
        }

        if (isSliding)
        {
            slideTimer -= Time.deltaTime;
            if (slideTimer <= 0)
            {
                EndSlide();
            }
        }
    }

    private void StartSlide()
    {
        verticalPosition = 0f;
        isSliding = true;
        slideTimer = slideDuration;
        characterController.height = 0.3f;
        characterController.radius = 0.3f;
        characterController.center = new Vector3(0f, 0.4f, 0f);
        playerAnimations.ShowAnimation("Crawl");
    }

    private void EndSlide()
    {
        isSliding = false;
        characterController.height = controllerHeight;
        characterController.radius = controllerRadius;
        characterController.center = controllerCenter;
        playerAnimations.ShowAnimation("Run");
    }

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if (hit.collider.CompareTag("Obstacle"))
        {
            if (GameManager.Instance.CurrentState == GameStates.GameOver)
            {
                return;
            }
            playerAnimations.ShowAnimation("Dead");
            GameManager.Instance.ChangeState(GameStates.GameOver);
        }

    }
}

