using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(PlayerInput))]
[RequireComponent(typeof(Animator))]

public class PlayerController : MonoBehaviour, IBulletTarget
{
    public Vector2 LastDirection { get; private set; } = Vector2.up;
    public Vector2 PlayerPosition { get => transform.position; }

    [SerializeField] public float moveSpeed = 5f;
    [SerializeField] private float deathDelay = 1;
    [SerializeField] private float shootingCooldownSeconds = 1;
    [SerializeField] private float staminaAmount = 5;
    [SerializeField] private float minStaminaToMove = 2;
    [SerializeField] private float slowMoTimeScale = 0;
    
    [Range(0.0f, 1.0f)]
    [SerializeField] private float startSlowDownAtPercentage = 0.1f;

    private Rigidbody2D rb;
    private PlayerInput playerInput;
    private Animator anim;
    private UIStaminaSlider uiStaminaSlider;
    private BulletCreatorController bulletCreatorController;

    private Vector2 moveAmount;
    private Coroutine cooldown;
    private bool isCharacterTryingToMove = false;
    private bool isCharacterAbleToMove = true;
    private float staminaLeft;
    private bool hasCharacterStartedMoving = false;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        playerInput = GetComponent<PlayerInput>();
        anim = GetComponent<Animator>();
        uiStaminaSlider = GetComponentInChildren<UIStaminaSlider>();
        TryGetComponent(out bulletCreatorController);

        staminaLeft = staminaAmount;
        ObjectMovements.timeScale = 0;

        playerInput.actions["Move"].canceled += OnMovementInputCanceled;
        playerInput.actions["Move"].started += OnMovementInputStarted;
    }
    private void FixedUpdate()
    {
        if (isCharacterTryingToMove)
        {
            if (isCharacterAbleToMove)
            {
                if (hasCharacterStartedMoving == false)
                {
                    StartCharacterMovement();
                }

                MoveCharacter();
            }
            else
            {
                RechargeStamina();
            }
        }
        else
        {
            RechargeStamina();
        }

        uiStaminaSlider.SetSliderPercentage(staminaLeft/staminaAmount);
    }

    private void StartCharacterMovement()
    {
        hasCharacterStartedMoving = true;
        AudioPlayer.Instance.PlaySong(SongType.TimeFreezeSong, true);
    }

    private void MoveCharacter()
    {
        MoveCharacter(moveAmount);

        // Slow player down before stopping
        if (staminaLeft < staminaAmount * startSlowDownAtPercentage)
        {
            MoveCharacter(staminaLeft / staminaAmount / startSlowDownAtPercentage * moveAmount);
        }
        else
        {
            MoveCharacter(moveAmount);
        }

        staminaLeft -= Time.deltaTime;
        ObjectMovements.timeScale = slowMoTimeScale;

        if (staminaLeft <= 0)
        {
            staminaLeft = 0;
            DisableCharacterMovement();
        }
    }

    private void DisableCharacterMovement()
    {
        isCharacterAbleToMove = false;
        hasCharacterStartedMoving = false;
        StopCharacter();
        AudioPlayer.Instance.PlaySong(SongType.TimeMovingSong, true);
    }

    private void RechargeStamina()
    {
        ObjectMovements.timeScale = 1;
        staminaLeft += Time.deltaTime;

        if (staminaLeft > staminaAmount)
        {
            staminaLeft = staminaAmount;
        }

        if (staminaLeft >= minStaminaToMove)
        {
            isCharacterAbleToMove = true;
        }
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        // read the value for the "move" action each event call
        moveAmount = context.ReadValue<Vector2>();

        if (moveAmount.Equals(Vector2.zero)) { return; }

        LastDirection = moveAmount;

        anim.SetFloat("LastDirX", LastDirection.x);
        anim.SetFloat("LastDirY", LastDirection.y);
    }

    private void OnMovementInputStarted(InputAction.CallbackContext context)
    {
        isCharacterTryingToMove = true;
    }

    private void OnMovementInputCanceled(InputAction.CallbackContext context)
    {
        isCharacterTryingToMove = false;
        
        // Need for the character to stop after stamina runs out
        MoveCharacter(moveAmount);
        AudioPlayer.Instance.PlaySong(SongType.TimeMovingSong, true);
    }

    public void Fire()
    {
        if (bulletCreatorController == null || cooldown != null) { return; }

        cooldown = StartCoroutine(ShootingCooldown());
        bulletCreatorController.FireAtDirection(gameObject.transform.position, LastDirection);
    }

    public void Hit()
    {
        DisableCharacterMovement();
        AudioPlayer.Instance.PlayUninterruptableSFX(SFXType.PlayerDeath);
        anim.SetTrigger("Death");
        SceneLoader.Instance.ReloadCurrentScene(deathDelay);
        playerInput.actions["Move"].canceled -= OnMovementInputCanceled;
        playerInput.actions["Move"].started -= OnMovementInputStarted;
    }
    private void MoveCharacter(Vector2 direction)
    {
        rb.velocity = moveSpeed * direction;
        anim.SetFloat("Velocity", rb.velocity.magnitude);
    }

    private void StopCharacter()
    {
        rb.velocity = Vector2.zero;
        anim.SetFloat("Velocity", rb.velocity.magnitude);
    }

    private IEnumerator ShootingCooldown()
    {
        yield return new WaitForSeconds(shootingCooldownSeconds);
        cooldown = null;
    }
}
